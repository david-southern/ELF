using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;
using System.Data.Common;

namespace ProjectUtils
{
    [Serializable]
    public class LoginException : Exception
    {
        public LoginException(string formatMessage, params object[] formatParams)
            : base(formatParams.Length > 0 ? String.Format(formatMessage, formatParams) : formatMessage)
        {
        }
    }

    public static class LoginUtils
    {
        public static void RequireLogin()
        {
            if (HttpContext.Current == null)
            {
                throw new InvalidOperationException("No HttpContext found");
            }

            string loginUrl = ConfigurationManager.AppSettings["LoginURL"];

            if (String.IsNullOrEmpty(loginUrl))
            {
                throw new InvalidOperationException("AppSetting LoginURL is not found");
            }

            if (!IsLoggedIn())
            {
                HttpContext.Current.Session[Constants.SESSION_CMON_BACK] = HttpContext.Current.Request.Url.AbsoluteUri;
                HttpContext.Current.Response.Redirect(loginUrl);
            }
        }

        public static bool Login(string username, string password)
        {
            DBAccess my_access = null;
            string log_cat = "Login";

            try
            {
                if (HttpContext.Current == null)
                {
                    return false;
                }

                my_access = new DBAccess("LoginUtils");

                if (username == null || String.IsNullOrEmpty(username.Trim()))
                {
                    throw new LoginException("You must provide a username.");
                }

                username = username.Trim();

                HttpContext.Current.Session.Remove(ProjectUtils.Constants.SESSION_AUTHORIZATION);
                HttpContext.Current.Session.Remove(ProjectUtils.Constants.SESSION_USERNAME);
                HttpContext.Current.Session.Remove(ProjectUtils.Constants.SESSION_USER_ID);

                password = ProjectUtils.Utils.SimplePasswordHash(password);

                string sql, err_msg;

                sql = String.Format("SELECT id FROM UserAccounts WHERE username = {0} AND password = {1}",
                    DBAccess.FormatSQLValue(username),
                    DBAccess.FormatSQLValue(password)
                    );

                object user_id_obj = my_access.GetScalar(sql, out err_msg);

                if (err_msg != null)
                {
                    string message = String.Format(
                        "DB Error while looking for UserAccounts record for username: {0}, password: {1}.  Error: {2}",
                        username, password, err_msg);
                    my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat, message);
                    throw new Exception(message);
                }

                if (user_id_obj == null || user_id_obj == DBNull.Value)
                {
                    return false;
                }

                string user_id = user_id_obj.ToString();

                HttpContext.Current.Session[ProjectUtils.Constants.SESSION_AUTHORIZATION] = user_id;
                HttpContext.Current.Session[ProjectUtils.Constants.SESSION_USER_ID] = user_id;
                HttpContext.Current.Session[ProjectUtils.Constants.SESSION_USERNAME] = username;

                UsageAudit.LogUsage(UsageAudit.EventType.UserLogin, user_id, null);

                return true;
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }
        }

        public static void Logout()
        {
            if (HttpContext.Current == null || HttpContext.Current.Session[ProjectUtils.Constants.SESSION_AUTHORIZATION] == null)
            {
                return;
            }

            string user_id = (string)HttpContext.Current.Session[ProjectUtils.Constants.SESSION_USER_ID];
            HttpContext.Current.Session.Remove(ProjectUtils.Constants.SESSION_AUTHORIZATION);
            HttpContext.Current.Session.Remove(ProjectUtils.Constants.SESSION_USER_ID);
            HttpContext.Current.Session.Remove(ProjectUtils.Constants.SESSION_USERNAME);

            UsageAudit.LogUsage(UsageAudit.EventType.UserLogout, user_id, null);
        }

        public static bool IsLoggedIn()
        {
            if (HttpContext.Current == null || HttpContext.Current.Session[ProjectUtils.Constants.SESSION_AUTHORIZATION] == null)
            {
                return false;
            }

            return true;
        }

        private static string ValidatePassword(string clearPassword)
        {
            if (clearPassword.Length <= 6)
            {
                throw new LoginException("Your password must be more than 6 characters long.");
            }

            if (clearPassword.Length >= 42)
            {
                throw new LoginException("Your password must be less than 42 characters long.");
            }

            return ProjectUtils.Utils.SimplePasswordHash(clearPassword);
        }

        public static string CreateUser(string username, string clearPassword)
        {
            DBAccess my_access = null;
            string log_cat = "CreateUser";
            try
            {
                my_access = new DBAccess("LoginUtils");

                string sql, err_msg;

                sql = String.Format("SELECT id FROM UserAccounts WHERE username = {0}", DBAccess.FormatSQLValue(username));

                object user_id_obj = my_access.GetScalar(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB Error while looking for UserAccounts record for username: {0}, Error: {1}", username, err_msg);
                    throw new LoginException("An error occured while trying to create your account.  Please contact {0} "
                        + "with error code: {1}", Constants.SUPPORT_EMAIL, log_id);
                }

                if (user_id_obj != null && user_id_obj != DBNull.Value)
                {
                    throw new LoginException("There is already another user with that same username, please choose another.");
                }

                string password = ValidatePassword(clearPassword);

                sql = String.Format("INSERT INTO UserAccounts ( username, password ) OUTPUT inserted.id VALUES ( {0}, {1} )",
                    DBAccess.FormatSQLValue(username),
                    DBAccess.FormatSQLValue(password));

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB error while creating user account for username {0}: {1}", username, err_msg);
                    throw new LoginException("An error occured while trying to create your account. "
                        + "  Please contact {1} with this error id: {0}", log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
                }

                string user_id = null;

                if (reader.Read())
                {
                    user_id = DBAccess.SafeString(reader, "id");
                }

                if (user_id == null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "No user ID was returned when creating user account for username {0}: {1}", username, err_msg);
                    throw new LoginException("An error occured while trying to create your account. "
                        + "  Please contact {0} with this error id: {1}",
                        ProjectUtils.Constants.SUPPORT_EMAIL,
                        log_id);
                }

                return user_id;
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }

            }
        }

        public static void UpdateUser(string user_id, string username, string clearPassword)
        {
            DBAccess my_access = null;
            string log_cat = "UpdateUser";
            try
            {
                if (String.IsNullOrEmpty(user_id))
                {
                    throw new ArgumentNullException("user_id cannot be null");
                }

                my_access = new DBAccess("LoginUtils");

                string sql = "", err_msg;

                if (username != null)
                {
                    username = username.Trim();
                }
                else
                {
                    username = "";
                }

                if (clearPassword != null)
                {
                    clearPassword = clearPassword.Trim();
                }
                else
                {
                    clearPassword = "";
                }

                if (username.Length > 0)
                {
                    sql = "UPDATE UserAccounts SET username = " + DBAccess.FormatSQLValue(username);
                }

                if (clearPassword.Length > 0)
                {
                    string password = ValidatePassword(clearPassword);

                    if (sql.Length > 0)
                    {
                        sql += ", ";
                    }
                    else
                    {
                        sql = "UPDATE UserAccounts SET ";
                    }

                    sql += "password = " + DBAccess.FormatSQLValue(password);
                }

                if (sql.Length > 0)
                {
                    sql += " WHERE id = " + DBAccess.FormatSQLValue(user_id);

                    my_access.ExecuteNonQuery(sql, out err_msg);

                    if (err_msg != null)
                    {
                        int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                            "DB Error while updating UserAccounts record for user_id: {0}.  "
                            + "Error: {1}", user_id, err_msg);

                        throw new LoginException("An error occured while trying to update your login.  "
                            + "Please contact {0} with error code: {1}", ProjectUtils.Constants.SUPPORT_EMAIL, log_id);
                    }

                    if (username.Length > 0 && HttpContext.Current != null)
                    {
                        HttpContext.Current.Session[ProjectUtils.Constants.SESSION_USERNAME] = username;
                    }
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

        public static void SetProfileString(string profileKey, string profileValue)
        {
            DBAccess my_access = null;
            string log_cat = "SetProfileString";

            try
            {
                my_access = new DBAccess("LoginUtils");

                if (!IsLoggedIn())
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "SetProfileString called while user was not logged in.  Key: {0}, Value: {1}, Stack Trace: {2}",
                        profileKey, profileValue, Environment.StackTrace);
                    throw new LoginException("An error occured while accessing your account.  Please contact "
                        + "{1} with error code {2} for further help.", Constants.SUPPORT_EMAIL, log_id);
                }

                string user_id = (string)HttpContext.Current.Session[Constants.SESSION_USER_ID];

                string sql, err_msg;

                sql = DBAccess.FormatSQL("EXECUTE spELF_UpsertProfileValue {0}, {1}, {2}",
                    user_id, profileKey, profileValue);

                my_access.ExecuteNonQuery(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB Error while trying to set profile string.  UserId: {0}, Key: {1}, Value: {2}, Error: {3}",
                        user_id, profileKey, profileValue, err_msg);
                    throw new LoginException("An error occured while accessing your account.  Please contact "
                        + "{1} with error code {2} for further help.", Constants.SUPPORT_EMAIL, log_id);
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

        public static string GetProfileString(string profileKey)
        {
            DBAccess my_access = null;
            string log_cat = "GetProfileString";

            try
            {
                my_access = new DBAccess("LoginUtils");

                if (!IsLoggedIn())
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "GetProfileString called while user was not logged in.  Key: {0}, Stack Trace: {1}",
                        profileKey, Environment.StackTrace);
                    throw new LoginException("An error occured while accessing your account.  Please contact "
                        + "{1} with error code {2} for further help.", Constants.SUPPORT_EMAIL, log_id);
                }

                string user_id = (string)HttpContext.Current.Session[Constants.SESSION_USER_ID];

                string sql, err_msg;

                sql = DBAccess.FormatSQL(
                    "SELECT profile_value FROM UserProfileData WHERE user_id = {0} AND profile_key = {1}",
                    user_id, profileKey);

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB Error while trying to get profile string.  UserId: {0}, Key: {1}, Error: {2}",
                        user_id, profileKey, err_msg);
                    throw new LoginException("An error occured while accessing your account.  Please contact "
                        + "{1} with error code {2} for further help.", Constants.SUPPORT_EMAIL, log_id);
                }

                string result = null;

                if (reader.Read())
                {
                    result = DBAccess.SafeString(reader, "profile_value");
                }

                return result;
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
