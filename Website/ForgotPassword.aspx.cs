using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectUtils;

namespace Website
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        public static string Url()
        {
            return "~/ForgotPassword.aspx";
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Master.HideContent(ELFThings.ContentSections.BLUE_SECTION);

            if (ProjectUtils.LoginUtils.IsLoggedIn())
            {
                Response.Redirect(Login.Url());
            }
        }

        protected void ResendButton_Click(object sender, EventArgs e)
        {
            string errorMessage;

            if (LocalUtils.SendPasswordEmail(ResendEmail.Text, out errorMessage))
            {
                Master.SetNotificationMessage("We have sent an email with password reset instructions to {0}.  "
                    + "If you do not receive the email within a few minutes, please check your spam folder.  "
                    + "If you still cannot find the email, please contact {1} for further assistance.",
                    ResendEmail.Text, Constants.SUPPORT_EMAIL);
                ForgottenPanel.Visible = false;
            }
            else
            {
                Master.SetErrorMessage(errorMessage);
            }
        }
    }
}
