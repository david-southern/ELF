using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ProjectUtils;
using System.Data.Common;

namespace Website
{
    public partial class PTOProgramFlyers : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            LoginUtils.RequireLogin();
        }

        private bool DO_CUSTOMIZED_FLYERS = false;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (DO_CUSTOMIZED_FLYERS)
            {
                GenerateCustomizedFlyer();
            }

            if (IsPostBack)
            {
                return;
            }

            DBAccess my_access = null;
            string log_cat = "Page_Load";
            string user_id = null;

            try
            {
                my_access = new DBAccess(this);

                Master.HideContent(ELFThings.ContentSections.BLUE_SECTION);

                string sql, err_msg;

                user_id = (string)Session[Constants.SESSION_USER_ID];

                sql = DBAccess.FormatSQL("SELECT flyers_requested, flyers_sent, flyers_sent_timestamp_utc "
                    + "FROM PTARegistration WHERE user_id = {0}", user_id);

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB Error while selecting Org Info for user_id: {0}, Error: {1}", user_id, err_msg);
                    Master.SetErrorMessage("An error occured while generating the promotional flyer page.  Please "
                        + "contact {0} with error code: {1} for further help.", Constants.SUPPORT_EMAIL, log_id);
                    return;
                }

                object flyersRequestedObj = null;
                object flyersSentObj = null;
                object flyersSentTimestampObj = null;

                if (reader.Read())
                {
                    flyersRequestedObj = reader["flyers_requested"];
                    flyersSentObj = reader["flyers_sent"];
                    flyersSentTimestampObj = reader["flyers_sent_timestamp_utc"];
                }

                int flyersRequested = 0, flyersSent = 0;
                DateTime flyersSentTimestamp;

                if (DBAccess.IsNull(flyersRequestedObj))
                {
                    OrderFlyersPanel.Visible = true;
                    FlyersSentPanel.Visible = false;
                    return;
                }

                flyersRequested = (int)flyersRequestedObj;

                if(!DBAccess.IsNull(flyersSentObj))
                {
                    flyersSent = (int)flyersSentObj;
                }

                OrderFlyersPanel.Visible = false;
                FlyersSentPanel.Visible = true;

                if (DBAccess.IsNull(flyersSentTimestampObj))
                {
                    Master.SetNotificationMessage("We have received your order for {0} promotional flyers.  "
                        + "We will send them out as soon as possible.", flyersRequested);
                    return;
                }

                flyersSentTimestamp = (DateTime)flyersSentTimestampObj;

                Master.SetNotificationMessage("We sent your order of {0} flyers on {1}",
                    flyersSent, flyersSentTimestamp.ToString("MMM dd, yyyy"));
            }
            catch (Exception ex)
            {
                int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "Caught exception while building Flyer page for user_id: {0}, Exception: {1} {2}",
                    user_id, ex.Message, ex.StackTrace);
                Master.SetErrorMessage("An error occured while generating the promotional flyer page.  Please "
                    + "contact {0} with error code: {1} for further help.", Constants.SUPPORT_EMAIL, log_id);
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

        protected void SendFlyersButtonClick(object sender, EventArgs e)
        {
            DBAccess my_access = null;
            string log_cat = "SendFlyersButtonClick";
            string user_id = null;

            int requestedFlyers = 0;

            try
            {
                my_access = new DBAccess(this);

                requestedFlyers = Utils.SafeInt(FlyerCount.Text, 0);

                if (requestedFlyers < 1)
                {
                    Master.SetErrorMessage("We did not recognize the number of flyers you requested.  Please enter "
                        + "the number of flyers you need for your parents.");
                    return;
                }

                Master.HideContent(ELFThings.ContentSections.BLUE_SECTION);

                string sql, err_msg;

                user_id = (string)Session[Constants.SESSION_USER_ID];

                sql = DBAccess.FormatSQL("UPDATE PTARegistration SET flyers_requested = {0}, "
                    + "flyers_sent = NULL, flyers_sent_timestamp_utc = NULL "
                    + "WHERE user_id = {1}", requestedFlyers, user_id);

                my_access.ExecuteNonQuery(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB Error while updating number of flyers requested for user_id: {0}, Error: {1}", user_id, err_msg);
                    Master.SetErrorMessage("An error occured while saving the number of flyers requested.  Please "
                        + "contact {0} with error code: {1} for further help.", Constants.SUPPORT_EMAIL, log_id);
                    return;
                }

                OrderFlyersPanel.Visible = false;
                FlyersSentPanel.Visible = true;

                string message = String.Format(
                    "We have recorded your order for {0} flyers.  We will send them as soon as possible.",
                    requestedFlyers);

                Master.SetNotificationMessage(message);
            }
            catch (Exception ex)
            {
                int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "Caught exception while recording flyer request for {3} flyers for user_id: {0}, Exception: {1} {2}",
                    user_id, ex.Message, ex.StackTrace, requestedFlyers);
                Master.SetErrorMessage("An error occured while saving your flyer request.  Please "
                    + "contact {0} with error code: {1} for further help.", Constants.SUPPORT_EMAIL, log_id);
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

        protected void OrderMoreButtonClick(object sender, EventArgs e)
        {
            OrderFlyersPanel.Visible = true;
            FlyersSentPanel.Visible = false;
        }

        private void GenerateCustomizedFlyer()
        {
            DBAccess my_access = new DBAccess(this);
            string log_cat = "GenerateCustomizedFlyer";
            string user_id = null;

            try
            {
                Master.HideContent(ELFThings.ContentSections.BLUE_SECTION);

                string sql, err_msg;

                user_id = (string)Session[Constants.SESSION_USER_ID];

                sql = String.Format(
                    "SELECT school_name, org_code, promotion_expire_date FROM PTARegistration WHERE user_id = {0}",
                    DBAccess.FormatSQLValue(user_id));

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB Error while selecting Org Info for user_id: {0}, Error: {1}", user_id, err_msg);
                    Master.SetErrorMessage("An error occured while generating your promotional flyer.  Please "
                        + "contact {0} with error code: {1} for further help.", Constants.SUPPORT_EMAIL, log_id);
                    return;
                }

                object orgName_obj = null;
                object orgCode_obj = null;
                object expDate_obj = null;

                if (reader.Read())
                {
                    orgName_obj = reader["school_name"];
                    orgCode_obj = reader["org_code"];
                    expDate_obj = reader["promotion_expire_date"];
                }

                if (orgName_obj == null || orgName_obj == DBNull.Value
                    || orgCode_obj == null || orgCode_obj == DBNull.Value
                    || expDate_obj == null || expDate_obj == DBNull.Value)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "user_id: {0} attempted to generate promotional flyers without a SchoolName, OrgCode, or ExpDate", user_id);
                    Master.SetErrorMessage("An error occured while generating your promotional flyer.  Please "
                        + "contact {0} with error code: {1} for further help.", Constants.SUPPORT_EMAIL, log_id);
                    return;
                }

                Dictionary<string, string> replacements = new Dictionary<string, string>();

                replacements.Add("OrgName", (string)orgName_obj);
                replacements.Add("OrgCode", (string)orgCode_obj);
                replacements.Add("ExpDate", ((DateTime)expDate_obj).ToString("MMMM dd, yyyy"));

                MemoryStream stream = PDFUtils.GeneratePDFForm("OrgFlyer", replacements);

                byte[] bytes = stream.ToArray();

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.OutputStream.Write(bytes, 0, bytes.Length);

                my_access.LogMessage(DBAccess.LOG_LEVEL_INFO, log_cat,
                    "Generated promotional flyers for user_id: {0}", user_id);
            }
            catch (Exception ex)
            {
                int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "Caught exception while generating Org Flyer for user_id: {0}, Exception: {1} {2}",
                    user_id, ex.Message, ex.StackTrace);
                Master.SetErrorMessage("An error occured while generating your promotional flyer.  Please "
                    + "contact {0} with error code: {1} for further help.", Constants.SUPPORT_EMAIL, log_id);
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
    }
}
