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
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.HideContent(ELFThings.ContentSections.BLUE_SECTION);
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            DBAccess my_access = null;
            string log_cat = "SubmitButton_Click";

            string name = null;
            string email = null;
            string feedback = null;
            int log_id;

            try
            {
                my_access = new DBAccess(this);

                string type = "Comment";
                string responseEmailTemplate = "FeedbackResponse";
                string reply_email_body;

                name = ProjectUtils.WebUtils.PreventXSS(Name.Text);
                email = ProjectUtils.WebUtils.PreventXSS(Email.Text);
                feedback = ProjectUtils.WebUtils.PreventXSS(Feedback.Text);

                HttpRequest request = HttpContext.Current.Request;

                Dictionary<string, string> replacements = new Dictionary<string, string>();
                replacements.Add("{**site_url**}", request.Url.AbsolutePath);
                replacements.Add("{**request_time**}", DateTime.Now.ToString());
                replacements.Add("{**name**}", name);
                replacements.Add("{**email**}", email);
                replacements.Add("{**feedback**}", feedback);

                if (FeedbackType.SelectedValue == "Support")
                {
                    if (String.IsNullOrEmpty(email))
                    {
                        Master.SetErrorMessage("You must provide an email address for a support request, so that "
                            + "we can respond when we have a solution to your problem.  Thank you.");
                        return;
                    }
                    type = "Support Request";
                    responseEmailTemplate = "SupportRequestResponse";
                }

                if (FeedbackType.SelectedValue == "Question")
                {
                    if (String.IsNullOrEmpty(email))
                    {
                        Master.SetErrorMessage("You must provide an email address for a question, so that "
                            + "we can respond when we have an answer.  If you want to provide feedback that does "
                            + "require a response, please use the 'Comment' options.  Thank you.");
                        return;
                    }
                    type = "Question";
                }

                replacements.Add("{**feedback_type**}", type);

                string sql, err_msg;

                sql = DBAccess.FormatSQL("INSERT INTO SupportRequests ( email, name, request, type ) "
                    + "OUTPUT inserted.id, inserted.created_utc "
                    + "VALUES ( {0}, {1}, {2}, {3} )",
                    email, name, feedback, type); 

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    throw new Exception("DBAccess reported error: " + err_msg);
                }

                string request_id = null;

                if(reader.Read())
                {
                    request_id = DBAccess.SafeString(reader, "id");
                }

                if (request_id == null)
                {
                    throw new Exception("INSERT of SupportRequest returned invalid or null request_id.");
                }

                replacements.Add("{**request_id**}", request_id.ToString());

                reply_email_body = ProjectUtils.WebUtils.GetHTMLFromTemplate("FeedbackReceived", replacements);

                log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_INFO, log_cat,
                    "Sending Feedback Received for: Email: {0} Name: {1}, Feedback: {2}", email, name, feedback);

                if (!ProjectUtils.SendEmail.SendSupportEmail(null, ProjectUtils.Constants.SUPPORT_EMAIL, null, null,
                    "ELF " + type + " Received", reply_email_body))
                {
                    throw new ExceptionEx("Unable to send 'Feedback Received' email to {0}", ProjectUtils.Constants.SUPPORT_EMAIL);
                }

                if (!String.IsNullOrEmpty(email))
                {
                    reply_email_body = ProjectUtils.WebUtils.GetHTMLFromTemplate(responseEmailTemplate, replacements);

                    if (!ProjectUtils.SendEmail.SendSupportEmail(ProjectUtils.Constants.SUPPORT_EMAIL,
                        email, null, null, "ELF " + type + " Received", reply_email_body))
                    {
                        throw new ExceptionEx("Unable to send feedback response email to user email: '{0}'", email);
                    }

                    my_access.LogMessage(DBAccess.LOG_LEVEL_INFO, log_cat,
                        "Sent an feedback response Email to: {0} Name: {1}, Feedback: {2}", email, name, feedback);
                }

                Master.SetNotificationMessage("Your feedback has been sent.  Thank you.");
                FeedbackPanel.Visible = false;
            }
            catch (Exception ex)
            {
                log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "Caught an exception while trying to send Feedback Response Email to: {0} Name: {1}, "
                    + "Feedback: {2}. Exception: {3} {4}", email, name, feedback, ex.Message, ex.StackTrace);

                Master.SetErrorMessage("An error occured while trying to process your feedback. "
                    + "Please email {1} with the details of your feedback and error code: "
                    + "{0}, to make sure that your feedback is received.", log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
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
