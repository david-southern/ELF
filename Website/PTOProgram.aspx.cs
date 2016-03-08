using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectUtils;
using System.Data.Common;

namespace Website
{
    public partial class PTOProgram : System.Web.UI.Page
    {
        public static string Url()
        {
            return "~/PTOProgram.aspx";
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            LoginUtils.RequireLogin();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.HideContent(ELFThings.ContentSections.BLUE_SECTION);

            if (!LocalUtils.HasPTARegistration())
            {
                Response.Redirect(PTORegister.Url());
            }

            UpdateProgramStatus();
        }

        private void UpdateProgramStatus()
        {
            DBAccess my_access = null;
            string log_cat = "UpdateProgramStatus";
            string user_id = null;

            try
            {
                my_access = new DBAccess(this);

                string sql, err_msg;

                user_id = (string)Session[Constants.SESSION_USER_ID];

                sql = String.Format("SELECT org_code, promotion_expire_date FROM PTARegistration WHERE user_id = {0}",
                    DBAccess.FormatSQLValue(user_id)
                    );

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB error while looking up PTARegistration for user_id: {0}, Error: {1}", user_id, err_msg);
                    Master.SetErrorMessage("An error occured while looking up your registration.  Please contact "
                        + "{0} with error code: {1} for further help.", Constants.SUPPORT_EMAIL, log_id);
                    return;
                }

                object org_code_obj = null;
                object exp_date_obj = null;

                if (reader.Read())
                {
                    org_code_obj = reader["org_code"];
                    exp_date_obj = reader["promotion_expire_date"];
                }

                if (org_code_obj == null || org_code_obj == DBNull.Value)
                {
                    SalesStatusPanel.Visible = false;
                    StartProgramPanel.Visible = true;
                    ProgramStatus.Text = "Not Started";
                }
                else
                {
                    SalesStatusPanel.Visible = true;
                    StartProgramPanel.Visible = false;

                    DateTime expDate = (DateTime)exp_date_obj;

                    ProgramStatus.Text = DateTime.UtcNow < expDate ? "In Progress" : "Finished";

                    OrgCode.Text = (string)org_code_obj;
                    ExpDate.Text = expDate.ToString("MMMM dd, yyyy");

                    GetSalesTotal(my_access, (string)org_code_obj);
                }
            }
            catch (Exception ex)
            {
                int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "Caught exception while updating program status for user_id: {0}, Exception: {1} {2}",
                    user_id, ex.Message, ex.StackTrace);
                Master.SetErrorMessage("An error occured while updating your program status.  Please contact "
                    + "{0} with error code: {1} for further help.", Constants.SUPPORT_EMAIL, log_id);
                return;
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }
        }

        private void GetSalesTotal(DBAccess my_access, string orgCode)
        {
            string log_cat = "GetSalesTotal";

            ProgramSalesTotal.Text = "$0.00";

            string sql, err_msg;

            sql = String.Format("SELECT SUM(amount_paid) as total_sales FROM Purchases WHERE coupon LIKE '%{0}%' AND paid = 1", orgCode);

            object totalSalesObj = my_access.GetScalar(sql, out err_msg);

            if (err_msg != null)
            {
                int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "DB error while looking up total sales for orgCode: {0}, Error: {1}", orgCode, err_msg);
                Master.SetErrorMessage("An error occured while looking up your registration, please "
                    + "contact {0} with error code: {1} for further help.", Constants.SUPPORT_EMAIL, log_id);
                return;
            }

            decimal totalSales = 0;

            if (totalSalesObj != null && totalSalesObj != DBNull.Value)
            {
                totalSales = (decimal)totalSalesObj;
            }

            ProgramSalesTotal.Text = String.Format("{0:C2}", totalSales);
            FundraiserSalesTotal.Text = String.Format("{0:C2}", totalSales / 4);
        }

        protected void StartProgramButton_Click(object sender, EventArgs e)
        {
            DBAccess my_access = null;
            string log_cat = "StartProgramButton_Click";
            string user_id = null;

            try
            {
                my_access = new DBAccess(this);

                string sql, err_msg;

                user_id = (string)Session[Constants.SESSION_USER_ID];

                sql = String.Format("SELECT org_code, promotion_expire_date FROM PTARegistration WHERE user_id = {0}",
                    DBAccess.FormatSQLValue(user_id));

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB error while looking up PTARegistration for user_id: {0}, Error: {1}", user_id, err_msg);
                    Master.SetErrorMessage("An error occured while looking up your registration.  Please contact "
                        + "{0} with error code: {1} for further help.", Constants.SUPPORT_EMAIL, log_id);
                    return;
                }

                object org_code_obj = null;
                object exp_date_obj = null;

                if (reader.Read())
                {
                    org_code_obj = reader["org_code"];
                    exp_date_obj = reader["promotion_expire_date"];
                }

                if (org_code_obj != null && org_code_obj != DBNull.Value)
                {
                    Master.SetErrorMessage("You already have an Organization code assigned.");
                    return;
                }

                string orgCode = Utils.GenerateUnambiguousSerialNumber();
                DateTime expDate = DateTime.UtcNow.AddDays(91);

                sql = String.Format(
                    "UPDATE PTARegistration SET org_code = {0}, promotion_expire_date = {1} WHERE user_id = {2}",
                    DBAccess.FormatSQLValue(orgCode),
                    DBAccess.FormatSQLValue(expDate),
                    DBAccess.FormatSQLValue(user_id)
                    );

                my_access.ExecuteNonQuery(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB error while creating OrgCode for user_id: {0}, OrgCode: {1}, Error: {1}",
                        user_id, orgCode, err_msg);
                    Master.SetErrorMessage("An error occured while creating your Organization Code.  Please contact "
                        + "{0} with error code: {1} for further help.", Constants.SUPPORT_EMAIL, log_id);
                    return;
                }

                UpdateProgramStatus();
            }
            catch (Exception ex)
            {
                int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "Caught exception while creating OrgCode for user_id: {0}, Exception: {1} {2}",
                    user_id, ex.Message, ex.StackTrace);
                Master.SetErrorMessage("An error occured while creating your Organization Code.  Please contact "
                    + "{0} with error code: {1} for further help.", Constants.SUPPORT_EMAIL, log_id);
                return;
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }
        }

        protected void UpdateInfoButton_Click(object sender, EventArgs e)
        {
        }
    }
}
