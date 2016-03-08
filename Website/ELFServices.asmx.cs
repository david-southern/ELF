using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using ProjectUtils;
using System.IO;
using System.Data.Common;

namespace Website
{
    /// <summary>
    /// Summary description for ELFServices
    /// </summary>
    [WebService(Namespace = "http://www.e-l-fun.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ELFServices : System.Web.Services.WebService
    {
        private static readonly string LICENSE_PROJECT_FILENAME = "~/licensing/ELF_IntelliLock.ilproj";

        [WebMethod]
        public string StartRegistration(string regEmail, bool allowMarketing, string hardwareID)
        {
            return "This is an older version of ELF Games.  We have identified a problem with this version that prevents "
                + "correct unlocking of the purchased games in some cases.  Please uninstall this version, and download "
                + "and reinstall the latest version of ELF Games from the http://www.e-l-fun.com website. We apologize "
                + "for the inconvenience.";
        }

        [WebMethod]
        public string FinishRegistration(string activationCode)
        {
            return "This is an older version of ELF Games.  We have identified a problem with this version that prevents "
                + "correct unlocking of the purchased games in some cases.  Please uninstall this version, and download "
                + "and reinstall the latest version of ELF Games from the http://www.e-l-fun.com website. We apologize "
                + "for the inconvenience.";
        }

        [WebMethod]
        public string FinishRegistrationRev001(string activationCode, string hardwareID, string clientInfo)
        {
            DBAccess my_access = null;
            string log_cat = "FinishRegistration";

            try
            {
                my_access = new DBAccess(this);

                my_access.LogMessage(DBAccess.LOG_LEVEL_INFO, log_cat, "Starting FinishRegistration for activation code: {0}", activationCode);

                if (activationCode == null || String.IsNullOrEmpty(activationCode.Trim()))
                {
                    return "No Activation Code was supplied";
                }

                activationCode = activationCode.Trim();

                if (hardwareID == null || String.IsNullOrEmpty(hardwareID.Trim()))
                {
                    return "No Hardware ID was supplied";
                }

                hardwareID = hardwareID.Trim();

                string sql, err_msg;

                sql = String.Format(
                      "SELECT p.activation_code, p.hardware_key, p.paid, p.id "
                    + "FROM Purchases p JOIN PurchaseItems pi ON (p.item_id = pi.id)"
                    + "WHERE p.activation_code = {0} AND pi.item_code = {1} AND pi.cart_order BETWEEN 0 and 9999",
                    DBAccess.FormatSQLValue(activationCode),
                    DBAccess.FormatSQLValue(ProjectUtils.Constants.PURCHASE_GAMES)
                    );

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "An error occured while checking activation code '{0}': {1}", activationCode, err_msg);
                    return String.Format("An error occured while trying to look up your Activation Code."
                        + "  Please contact {1} with this error id: {0}", log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
                }

                string actCode = null;
                object hardwareKey_obj = null;
                bool paid = false;
                string purchaseID = null;

                if (reader.Read())
                {
                    actCode = (string)reader["activation_code"];
                    hardwareKey_obj = reader["hardware_key"];
                    paid = (bool)reader["paid"];
                    purchaseID = DBAccess.SafeString(reader, "id");
                }

                if (actCode == null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "Activation code '{0}' was not found", activationCode);
                    return String.Format("We cannot find your registration.  "
                        + "Please contact {1} with this error id: {0}",
                        log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
                }

                if (!paid)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "Activation code '{0}' was found, but not paid", activationCode);
                    return String.Format("We cannot find your registration.  "
                        + "Please contact {1} with this error id: {0}",
                        log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
                }

                if (hardwareKey_obj != null && hardwareKey_obj != DBNull.Value && (string)hardwareKey_obj != hardwareID)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "Activation code '{0}' was attempted to unlock hardware ID: {1} "
                        + "but it has already been used to unlock hardware ID: {2}",
                        activationCode, hardwareID, hardwareKey_obj);

                    return String.Format("The activation key you specified has already been used to activate a different "
                        + "computer.  This may happen if you have changed computers or upgraded several components of "
                        + "your computer.  If you need to transfer this activation key to the new computer, please "
                        + "contact {2} with this information:\r\n\r\n  Activation Code: {0}\r\n"
                        + "  Hardware ID: {1}", activationCode, hardwareID, ProjectUtils.Constants.SUPPORT_EMAIL);
                }

                string full_project_filename = null;

                try
                {
                    full_project_filename = Server.MapPath(LICENSE_PROJECT_FILENAME);

                    IntelliLock.LicenseManager.ProjectFile myproject = new IntelliLock.LicenseManager.ProjectFile(full_project_filename);

                    myproject.Hardware_ID = hardwareID;

                    if (!myproject.LicenseInformation.ContainsKey("Version"))
                    {
                        myproject.LicenseInformation.Add("Version", ProjectUtils.Utils.GetProgramVersionString());
                    }

                    string license_file = Convert.ToBase64String(IntelliLock.LicenseManager.LicenseGenerator.CreateLicenseFile(myproject));

                    sql = String.Format("INSERT INTO CustMetrics ( client_info ) VALUES ( {0} )",
                        DBAccess.FormatSQLValue(clientInfo));

                    my_access.ExecuteNonQuery(sql, out err_msg);

                    if (err_msg != null)
                    {
                        int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                            "An error occured while storing Cust Metrics for activation code '{0}': {1}", activationCode, err_msg);
                    }

                    sql = String.Format("UPDATE Purchases SET "
                        + "license_file = {0}, hardware_key = {1} "
                        + "WHERE id = {2}",
                        DBAccess.FormatSQLValue(license_file),
                        DBAccess.FormatSQLValue(hardwareID),
                        DBAccess.FormatSQLValue(purchaseID)
                        );

                    my_access.ExecuteNonQuery(sql, out err_msg);

                    if (err_msg != null)
                    {
                        int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                            "An error occured while storing license_file for activation code '{0}': {1}", activationCode, err_msg);
                    }

                    my_access.LogMessage(DBAccess.LOG_LEVEL_INFO, log_cat, "Returning success and license file for Activation Code: {0}", activationCode);

                    return "Success:" + license_file;
                }
                catch (Exception ex)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "Caught exception while generating license file for Activation Code: {0}, Hardware Key: {1}.  "
                        + "Exception: {2} {3}",
                        activationCode, hardwareID, ex.Message, ex.StackTrace);
                    return String.Format("A problem occured while creating your license key.  "
                        + "Please contact {1} with this error id: {0}",
                        log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
                }
            }
            catch (Exception ex)
            {
                int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "Caught exception while finishing registration for Activation Code: {0}.  "
                    + "Exception: {1} {2}",
                    activationCode, ex.Message, ex.StackTrace);
                return String.Format("An unknown error occured while creating your license key.  "
                    + "Please contact {1} with this error id: {0}",
                    log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }
        }
    }
}
