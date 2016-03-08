using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectUtils;
using System.Threading;
using System.Data.Common;

namespace Website
{
    public partial class PTORegister : System.Web.UI.Page
    {
        public static string Url()
        {
            return "~/PTORegister.aspx";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.HideContent(ELFThings.ContentSections.BLUE_SECTION);

            if (LocalUtils.HasPTARegistration() && !IsPostBack)
            {
                PopulateUpdateAccountInfo();
            }
        }

        private void PopulateUpdateAccountInfo()
        {
            DBAccess my_access = null;
            string log_cat = "PopulateUpdateAccountInfo";
            string user_id = null;

            try
            {
                Register.Visible = false;
                Update.Visible = true;

                InstructionsLabel.Text = "Update your account information";
                InstructionsPanel.Visible = false;

                user_id = (string)Session[Constants.SESSION_USER_ID];

                my_access = new DBAccess(this);

                string sql, err_msg;

                sql = String.Format("SELECT "
                    + "school_name, contact_first_name, contact_last_name, contact_title, contact_addr1, "
                    + "contact_addr2, contact_city, contact_state, contact_zip, contact_phone "
                    + "FROM PTARegistration WHERE user_id = {0}",
                    DBAccess.FormatSQLValue(user_id));

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB Error while selecting PTARegistration record for user_id: {0}, Error: {1}",
                        user_id, err_msg);

                    Master.SetErrorMessage("An error occured while looking up your program data.  "
                        + "Please contact {0} with error code: {1}",
                        ProjectUtils.Constants.SUPPORT_EMAIL, log_id);
                    return;
                }

                if (reader.Read())
                {
                    SchoolName.Text = DBAccess.SafeString(reader, "school_name");
                    ContactFirstName.Text = DBAccess.SafeString(reader, "contact_first_name");
                    ContactLastName.Text = DBAccess.SafeString(reader, "contact_last_name");
                    ContactTitle.Text = DBAccess.SafeString(reader, "contact_title");
                    ContactAddrStreet.Text = DBAccess.SafeString(reader, "contact_addr1");
                    ContactAddrStreet2.Text = DBAccess.SafeString(reader, "contact_addr2");
                    ContactAddrCity.Text = DBAccess.SafeString(reader, "contact_city");
                    ContactAddrState.SelectedValue = DBAccess.SafeString(reader, "contact_state");
                    ContactAddrZip.Text = DBAccess.SafeString(reader, "contact_zip");
                    ContactPhone.Text = DBAccess.SafeString(reader, "contact_phone");
                }
            }
            catch (Exception ex)
            {
                int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "An exception occured while looking up PTA program data for user_id: {0}, Exception: {1} {2}", user_id, ex.Message, ex.StackTrace);
                Master.SetErrorMessage("An error occured while trying to look up your registration data. "
                    + "  Please contact {1} with this error id: {0}",
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

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            DBAccess my_access = null;
            string log_cat = "RegisterButton_Click";
            string user_id = null;

            try
            {
                user_id = (string)Session[Constants.SESSION_USER_ID];

                my_access = new DBAccess(this);

                string sql, err_msg;

                sql = DBAccess.FormatSQL("INSERT INTO PTARegistration "
                    + "( user_id, school_name, contact_first_name, contact_last_name, contact_title, contact_addr1, "
                    + "contact_addr2, contact_city, contact_state, contact_zip, contact_phone ) "
                    + " VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10} ) ",
                    user_id, SchoolName.Text, ContactFirstName.Text, ContactLastName.Text, ContactTitle.Text,
                    ContactAddrStreet.Text, ContactAddrStreet2.Text, ContactAddrCity.Text, ContactAddrState.Text,
                    ContactAddrZip.Text, ContactPhone.Text
                    );

                my_access.ExecuteNonQuery(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB Error while inserting PTARegistration record: {0}", err_msg);

                    Master.SetErrorMessage("An error occured while trying to create your registration.  "
                        + "Please contact {0} with error code: {1}", ProjectUtils.Constants.SUPPORT_EMAIL, log_id);
                    return;
                }

                Response.Redirect(PTOProgram.Url());
            }
            catch (LoginException lex)
            {
                Master.SetErrorMessage(lex.Message);
            }
            catch (Exception ex)
            {
                // The Response.Redirect above throws a ThreadAbortException, don't complain about that
                if (!(ex is ThreadAbortException))
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "An exception occured while registering PTA program for user_id: {0}, Exception: {1} {2}", user_id, ex.Message, ex.StackTrace);
                    Master.SetErrorMessage("An error occured while trying to process your registration. "
                        + "  Please contact {1} with this error id: {0}", log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
                }
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            DBAccess my_access = null;
            string log_cat = "UpdateButton_Click";
            string user_id = null;

            LoginUtils.RequireLogin();

            try
            {
                my_access = new DBAccess(this);

                string sql, err_msg;

                user_id = (string)Session[Constants.SESSION_USER_ID];

                sql = DBAccess.FormatSQL("UPDATE PTARegistration SET "
                    + "school_name = {1}, contact_first_name = {2}, contact_last_name = {3}, "
                    + "contact_title = {4}, contact_addr1 = {5}, contact_addr2 = {6}, contact_city = {7}, "
                    + "contact_state = {8}, contact_zip = {9}, contact_phone = {10} "
                    + "WHERE user_id = {0} ",
                    user_id, SchoolName.Text, ContactFirstName.Text, ContactLastName.Text, ContactTitle.Text,
                    ContactAddrStreet.Text, ContactAddrStreet2.Text, ContactAddrCity.Text, ContactAddrState.Text,
                    ContactAddrZip.Text, ContactPhone.Text
                    );

                my_access.ExecuteNonQuery(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB Error while updating PTARegistration record for user_id: {0}, Error: {1}",
                        user_id, err_msg);

                    Master.SetErrorMessage("An error occured while trying to update your registration.  "
                        + "Please contact {0} with error code: {1}", ProjectUtils.Constants.SUPPORT_EMAIL, log_id);
                    return;
                }

                Response.Redirect(PTOProgram.Url());
            }
            catch (LoginException lex)
            {
                Master.SetErrorMessage(lex.Message);
            }
            catch (Exception ex)
            {
                // The Response.Redirect above throws a ThreadAbortException, don't complain about that
                if (!(ex is ThreadAbortException))
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "An exception occured while updating PTA registration for user_id: {0}, Exception: {1} {2}", user_id, ex.Message, ex.StackTrace);
                    Master.SetErrorMessage("An error occured while trying to update your registration. "
                        + "  Please contact {1} with this error id: {0}", log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
                }
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
