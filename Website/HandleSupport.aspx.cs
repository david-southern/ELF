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
    public partial class HandleSupport : System.Web.UI.Page
    {
        public static string Url()
        {
            return "~/HandleSupport.aspx";
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            LoginUtils.RequireLogin();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Master.HideContent(ELFThings.ContentSections.BLUE_SECTION);

            if (LoginUtils.GetProfileString(Constants.PROFILE_ADMIN_TAG) != Constants.PROFILE_ADMIN_TAG)
            {
                Master.SetErrorMessage("You must be logged in as an admin to view this page.");
                return;
            }

            DBAccess my_access = null;
            string log_cat = "Page_PreRender";
            string request_id = null;

            try
            {
                my_access = new DBAccess(this);

                string sql, err_msg;

                request_id = Request.QueryString["request_id"];

                Guid requestId;

                try
                {
                    requestId = new Guid(request_id);
                    request_id = requestId.ToString();
                }
                catch (Exception)
                {
                    Master.SetErrorMessage("Invalid/No account reset token was found");
                    return;
                }

                sql = DBAccess.FormatSQL("SELECT id, email, name, request, response, type FROM SupportRequests WHERE id = {0}", request_id);

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    throw new ExceptionEx("DB Error while trying to lookup support request info for: {0}.  Error: {1}",
                        request_id, err_msg);
                }

                request_id = null;
                string email = null;
                string name = null;
                string request = null;
                string response = null;
                string type = null;

                while (reader.Read())
                {
                    request_id = DBAccess.SafeString(reader, "id");
                    email = DBAccess.SafeString(reader, "email");
                    name = DBAccess.SafeString(reader, "name");
                    request = DBAccess.SafeString(reader, "request");
                    response = DBAccess.SafeString(reader, "response");
                    type = DBAccess.SafeString(reader, "type");
                }

                if (request_id == null)
                {
                    Master.SetErrorMessage("Support request ID {0} did not match any user account",
                        Request.QueryString["request_id"]);
                    return;
                }

                ViewState[Constants.VIEWSTATE_FEEDBACK_ID] = request_id;
                ViewState[Constants.VIEWSTATE_FEEDBACK_TYPE] = type;

                FeedbackMessage.Text = request;
                FeedbackName.Text = name;
                FeedbackType.Text = type;
                FeedbackResponse.Text = response;

                if (String.IsNullOrEmpty(email))
                {
                    FeedbackEmail.Text = "** No email supplied, cannot send a response **";
                    NoEmailLabel.Text = "** No email supplied, cannot send a response **";
                    SendButton.Enabled = false;
                }
                else
                {
                    FeedbackEmail.Text = email;
                }
            }
            catch (Exception ex)
            {
                int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "Caught exception while rendering support request for request_id: {0}, Exception: {1} {2}",
                    request_id, ex.Message, ex.StackTrace);
                Master.SetErrorMessage("An error occured while rending support request.  Please "
                    + "contact David with error code: {0} for further help.", log_id);
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

        protected void SendButton_Click(object sender, EventArgs e)
        {
            DBAccess my_access = null;
            string log_cat = "SendButton_Click";
            string request_id = null;

            try
            {
                my_access = new DBAccess(this);

                string sql, err_msg;

                request_id = ViewState[Constants.VIEWSTATE_FEEDBACK_ID].ToString();
                string response = DateTime.Now.ToString("yyyy-MM-dd HH:mm") + " - " + FeedbackResponse.Text;

                sql = DBAccess.FormatSQL("UPDATE SupportRequests SET response = {0} WHERE id = {1}",
                    response, request_id);

                my_access.ExecuteNonQuery(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB error while updating SupportRequest {0}.  Error: {1}", request_id, err_msg);

                    Master.SetErrorMessage("DB Error while updating SupportRequest {0}.  Contact David with error code: {1}",
                        request_id, log_id);

                    return;
                }

                Dictionary<string, string> replacements = new Dictionary<string, string>();

                replacements.Add("{**feedback_type**}", ViewState[Constants.VIEWSTATE_FEEDBACK_TYPE].ToString());
                replacements.Add("{**response**}", response.Replace("\r\n", "<br>"));
                replacements.Add("{**site_url**}", HttpContext.Current.Request.Url.AbsolutePath);

                string email_body = ProjectUtils.WebUtils.GetHTMLFromTemplate("SupportResponse", replacements);

                if (!ProjectUtils.SendEmail.SendSupportEmail(ProjectUtils.Constants.SUPPORT_EMAIL, FeedbackEmail.Text, null, null,
                    "ELF Support Response", email_body))
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "Unable to send Password Reset Email to: {0}", FeedbackEmail.Text);

                    Master.SetErrorMessage("We were unable to send a support response email to: {0}.  "
                        + "Please contact David with this error code: {0}.", FeedbackEmail.Text, log_id);
                    return;
                }

                my_access.LogMessage(DBAccess.LOG_LEVEL_INFO, log_cat,
                    "Sent a support response email to: {0}", FeedbackEmail.Text);

                Master.SetNotificationMessage("Response sent");
                ResponsePanel.Visible = false;
            }
            catch (Exception ex)
            {
                int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "Caught exception while responding to support request for request_id: {0}, Exception: {1} {2}",
                    request_id, ex.Message, ex.StackTrace);
                Master.SetErrorMessage("An error occured while responding to support request.  Please "
                    + "contact David with error code: {0} for further help.", log_id);
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

        protected void MarkHandledButton_Click(object sender, EventArgs e)
        {
            DBAccess my_access = null;
            string log_cat = "MarkHandledButton_Click";
            string request_id = null;

            try
            {
                my_access = new DBAccess(this);

                string sql, err_msg;

                request_id = ViewState[Constants.VIEWSTATE_FEEDBACK_ID].ToString();
                string response = DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "Request marked as handled. - " + FeedbackResponse.Text;

                sql = DBAccess.FormatSQL("UPDATE SupportRequests SET response = {0} WHERE id = {1}",
                    response, request_id);

                my_access.ExecuteNonQuery(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB error while updating SupportRequest {0}.  Error: {1}", request_id, err_msg);

                    Master.SetErrorMessage("DB Error while updating SupportRequest {0}.  Contact David with error code: {1}",
                        request_id, log_id);

                    return;
                }

                my_access.LogMessage(DBAccess.LOG_LEVEL_INFO, log_cat,
                    "Marked support request {0} as handled", request_id);

                Master.SetNotificationMessage("Response sent");
                ResponsePanel.Visible = false;
            }
            catch (Exception ex)
            {
                int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "Caught exception while marking support request as handled for request_id: {0}, Exception: {1} {2}",
                    request_id, ex.Message, ex.StackTrace);
                Master.SetErrorMessage("An error occured while marking support request.  Please "
                    + "contact David with error code: {0} for further help.", log_id);
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
