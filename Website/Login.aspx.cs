using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectUtils;

namespace Website
{
    public partial class Login : System.Web.UI.Page
    {
        public static string Url()
        {
            return "~/Login.aspx";
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Master.HideContent(ELFThings.ContentSections.BLUE_SECTION);

            if (ProjectUtils.LoginUtils.IsLoggedIn())
            {
                LoginPanel.Visible = false;
                MyAccountPanel.Visible = true;

                UpdateEmail.Text = (string)Session[Constants.SESSION_USERNAME];
                ConfirmUpdateEmail.Text = (string)Session[Constants.SESSION_USERNAME];

                string allowEmailString = LoginUtils.GetProfileString(Constants.PROFILE_ALLOW_MARKETING);
                UpdateAllowEmail.Checked = allowEmailString == "True";
            }
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            try
            {
                ProjectUtils.LoginUtils.CreateUser(RegisterEmail.Text, RegisterPassword.Text);

                if (ProjectUtils.LoginUtils.Login(RegisterEmail.Text, RegisterPassword.Text))
                {
                    LoginUtils.SetProfileString(Constants.PROFILE_ALLOW_MARKETING, AllowEmail.Checked.ToString());

                    string returnUrl = (string)Session[Constants.SESSION_CMON_BACK];

                    Session.Remove(Constants.SESSION_CMON_BACK);

                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        Response.Redirect(returnUrl);
                    }

                    return;
                }
            }
            catch (LoginException lex)
            {
                Master.SetErrorMessage(lex.Message);
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if (ProjectUtils.LoginUtils.Login(LoginEmail.Text, LoginPassword.Text))
            {
                string returnUrl = (string)Session[Constants.SESSION_CMON_BACK];

                Session.Remove(Constants.SESSION_CMON_BACK);

                if (String.IsNullOrEmpty(returnUrl) && LocalUtils.HasPTARegistration())
                {
                    returnUrl = PTOProgram.Url();
                }

                if (!String.IsNullOrEmpty(returnUrl))
                {
                    Response.Redirect(returnUrl);
                }

                return;
            }

            Master.SetErrorMessage("Email or Password is incorrect.");
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            if (UpdatePassword.Text != ConfirmUpdatePassword.Text)
            {
                PasswordInvalid.Text = "* Confirm Password does not match Password<br>";
                return;
            }

            DBAccess my_access = null;
            string log_cat = "UpdateButton_Click";
            string user_id = null;

            LoginUtils.RequireLogin();

            try
            {
                my_access = new DBAccess(this);

                user_id = (string)Session[Constants.SESSION_USER_ID];

                LoginUtils.UpdateUser(user_id, UpdateEmail.Text, UpdatePassword.Text);

                LoginUtils.SetProfileString(Constants.PROFILE_ALLOW_MARKETING, UpdateAllowEmail.Checked.ToString());
            }
            catch (LoginException lex)
            {
                Master.SetErrorMessage(lex.Message);
            }
            catch (Exception ex)
            {
                int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "An exception occured while updating account settings for user_id: {0}, Exception: {1} {2}", user_id, ex.Message, ex.StackTrace);
                Master.SetErrorMessage("An error occured while trying to update your account settings. "
                    + "  Please contact {1} with this error id: {0}", log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }
        }

        protected void ResendButton_Click(object sender, EventArgs e)
        {
            LoginUtils.RequireLogin();

            string email = (string)Session[Constants.SESSION_USERNAME];

            string errorMessage;

            if (LocalUtils.SendPurchaseEmail(null, email, out errorMessage))
            {
                Master.SetNotificationMessage("We have sent an email with your purchase information to {0}.  "
                    + "If you do not receive the email within a few minutes, please check your spam folder.  "
                    + "If you still cannot find the email, please contact {1} for further assistance."
                    , email, Constants.SUPPORT_EMAIL);
            }
            else
            {
                Master.SetErrorMessage(errorMessage);
            }
        }
    }
}
