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
    public partial class ResetPassword : System.Web.UI.Page
    {
        public static string Url()
        {
            return "~/ResetPassword.aspx";
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Master.HideContent(ELFThings.ContentSections.BLUE_SECTION);

            if (ProjectUtils.LoginUtils.IsLoggedIn())
            {
                Response.Redirect(Login.Url());
            }

            DBAccess my_access = null;
            string log_cat = "Page_PreRender";

            try
            {
                my_access = new DBAccess(this);

                string sql, err_msg;

                string user_id = Request.QueryString["reset_id"];

                Guid userId;

                try
                {
                    userId = new Guid(user_id);
                    user_id = userId.ToString();
                }
                catch (Exception ex)
                {
                    Master.SetErrorMessage("Invalid/No account reset token was found");
                    return;
                }

                sql = DBAccess.FormatSQL("SELECT id FROM UserAccounts WHERE id = {0}", user_id);

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    throw new ExceptionEx("DB Error while trying to lookup account info for: {0}.  Error: {1}",
                        user_id, err_msg);
                }

                user_id = null;

                while (reader.Read())
                {
                    user_id = DBAccess.SafeString(reader, "id");
                }

                if (user_id == null)
                {
                    Master.SetErrorMessage("Account reset token did not match any user account");
                    return;
                }

                Session[Constants.SESSION_USER_ID] = user_id;
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }

        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {

            string user_id = (string)Session[Constants.SESSION_USER_ID];

            try
            {
                LoginUtils.UpdateUser(user_id, null, NewPassword.Text);
            }
            catch (LoginException lex)
            {
                Master.SetErrorMessage(lex.Message);
                return;
            }

            Master.SetNotificationMessage("Your password has been reset.");
            ResetPanel.Visible = false;
        }


    }
}
