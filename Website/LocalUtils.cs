using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectUtils;
using System.Data.Common;

namespace Website
{
    public static class LocalUtils
    {
        public static bool HasPTARegistration()
        {
            DBAccess my_access = null;
            string log_cat = "HasPTARegistration";
            string user_id = null;

            LoginUtils.RequireLogin();

            try
            {
                user_id = (string)HttpContext.Current.Session[Constants.SESSION_USER_ID];

                my_access = new DBAccess("LocalUtils");

                string sql, err_msg;

                sql = DBAccess.FormatSQL("SELECT id FROM PTARegistration WHERE user_id = {0}", user_id);

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB Error while selecting PTARegistration record for user_id: {0}, Error: {1}",
                        user_id, err_msg);
                    return false;
                }

                string id = null;

                if (reader.Read())
                {
                    id = DBAccess.SafeString(reader, "id");
                }

                return id != null;
            }
            catch (Exception ex)
            {
                int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "An exception occured while selecting PTA registration for user_id: {0}, Exception: {1} {2}", user_id, ex.Message, ex.StackTrace);
                return false;
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }
        }

        public static bool SendPurchaseEmail(string activationCode, string email, out string errorMessage)
        {
            errorMessage = null;

            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Email may not be null in SendPurchaseEmail");
            }

            if (HttpContext.Current == null)
            {
                throw new InvalidOperationException("No HttpContext found");
            }

            DBAccess my_access = null;
            string log_cat = "SendPurchaseEmail";

            try
            {
                my_access = new DBAccess("LocalUtils");

                string sql, err_msg;

                sql = String.Format("SELECT p.activation_code, pi.item_desc, pi.item_code, p.hardware_key, pi.item_url "
                    + "FROM Purchases p JOIN PurchaseItems pi ON (p.item_id = pi.id) "
                    + "JOIN UserAccounts ua on (p.user_id = ua.id) "
                    + "WHERE ua.username = {0} AND p.paid = 1 AND pi.cart_order BETWEEN 0 and 9999",
                    DBAccess.FormatSQLValue(email));

                if (activationCode != null)
                {
                    sql += " AND p.activation_code = " + DBAccess.FormatSQLValue(activationCode);
                }

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    throw new ExceptionEx("DB Error while trying to read purchases for actCode: {0}, email: {1}.  Error: {2}",
                        activationCode, email, err_msg);
                }

                string lessons_body = "";
                string games_body = "";
                int gameCodeCount = 0;

                Dictionary<string, Pair> purchasedLessons = new Dictionary<string, Pair>();

                while (reader.Read())
                {
                    string actCode = (string)reader["activation_code"];
                    string itemCode = (string)reader["item_code"];
                    string itemDesc = (string)reader["item_desc"];
                    object itemUrl_obj = reader["item_url"];
                    object hardwareKey = reader["hardware_key"];

                    string itemUrl = null;
                    if (itemUrl_obj != null && itemUrl_obj != DBNull.Value)
                    {
                        itemUrl = (string)itemUrl_obj;
                    }

                    if (itemCode == ProjectUtils.Constants.PURCHASE_GAMES)
                    {
                        games_body += String.Format("<br>&nbsp;&nbsp;&nbsp;{0}", actCode);

                        if (hardwareKey != null && hardwareKey != DBNull.Value)
                        {
                            games_body += "&nbsp;&nbsp;* This activation code has already been used";
                        }

                        gameCodeCount++;
                    }
                    else
                    {
                        purchasedLessons[itemCode] = new Pair(itemDesc, itemUrl);
                    }
                }

                string payload = "";

                if (purchasedLessons.Count < 1 && gameCodeCount < 1)
                {
                    payload = String.Format("Someone requested that a re-send of purchased downloadable items from the "
                        + "ELF Spelling/Writing program be sent to this email address, however we have no "
                        + "record of any purchases associated with this account.<br><br>"
                        + "If you did not request this email, then please disregard it.  If you did request this "
                        + "email, please check and see if your ELF purchases were sent to another one of your "
                        + "email addresses?<br><br>"
                        + "If you are sure that ELF purchases were associated with this email address, please contact "
                        + "{0} for further help.",
                        Constants.SUPPORT_EMAIL);
                }
                else
                {
                    List<string> lessonCodes = new List<string>(purchasedLessons.Keys);

                    lessonCodes.Sort();

                    string lessons_body_plain_url = "";

                    foreach (string lessonCode in lessonCodes)
                    {
                        string itemDesc = (string)purchasedLessons[lessonCode].first;
                        string itemUrl = (string)purchasedLessons[lessonCode].second;

                        lessons_body += String.Format(
                            "<br><br><a href='https://www.e-l-fun.com{1}'>{0}</a>", itemDesc, itemUrl);

                        lessons_body_plain_url += String.Format(
                            "<br><br>{0}: https://www.e-l-fun.com{1}", itemDesc, itemUrl);
                    }

                    if (lessons_body.Length > 0)
                    {
                        payload += "You can download your ELF Spelling Program Lessons here:" + lessons_body;
                        payload += "<br><br>Note: some email programs will not allow downloads directly from this email.  "
                            + "If you encounter any difficulties downloading the lessons, please copy the URL shown here "
                            + "and paste it directly into your browser to download the lessons:" + lessons_body_plain_url;
                    }

                    if (games_body.Length > 0)
                    {
                        if (payload.Length > 0)
                        {
                            payload += "<br><br><br>";
                        }

                        payload += String.Format("Your activation code{0} for the ELF Phonics Games {1}:<br>{2}",
                            gameCodeCount > 1 ? "s" : "",
                            gameCodeCount > 1 ? "are" : "is",
                            games_body);

                        payload += String.Format("<br><br>You can download the trial version of the ELF Phonics Games "
                            + "<a href='https://www.e-l-fun.com/installer/ELF.exe'>here</a> if you have not already "
                            + "installed the games.  When the trial version asks for an activation code, "
                            + "enter {0} shown above.",
                            gameCodeCount > 1 ? " one of the codes" : " the code");
                    }
                }

                Dictionary<string, string> replacements = new Dictionary<string, string>();
                replacements.Add("{**purchase_payload**}", payload);
                replacements.Add("{**site_url**}", HttpContext.Current.Request.Url.AbsolutePath);

                string email_body = ProjectUtils.WebUtils.GetHTMLFromTemplate("PurchaseActivationCodes", replacements);

                if (!ProjectUtils.SendEmail.SendSupportEmail(ProjectUtils.Constants.SUPPORT_EMAIL, email, null, null,
                    "ELF Program Purchases", email_body))
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "Unable to send Purchase Results Email to: {0} ActivationCode: {1}", email, activationCode);

                    errorMessage = String.Format("We were unable to send your purchase delivery email to: {0}.  "
                        + "Please contact {2} with this error code: {1}.", email, log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
                    return false;
                }

                my_access.LogMessage(DBAccess.LOG_LEVEL_INFO, log_cat,
                    "Sent a purchase results Email to: {0} ActivationCode: {1}", email, activationCode);

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

        public static bool SendPasswordEmail(string email, out string errorMessage)
        {
            errorMessage = null;

            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Email may not be null in SendPasswordEmail");
            }

            if (HttpContext.Current == null)
            {
                throw new InvalidOperationException("No HttpContext found");
            }

            DBAccess my_access = null;
            string log_cat = "SendPasswordEmail";

            try
            {
                my_access = new DBAccess("LocalUtils");

                string sql, err_msg;

                sql = DBAccess.FormatSQL("SELECT id FROM UserAccounts WHERE username = {0}", email);

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    throw new ExceptionEx("DB Error while trying to lookup account info for: {0}.  Error: {1}",
                        email, err_msg);
                }

                string user_id = null;

                while (reader.Read())
                {
                    user_id = DBAccess.SafeString(reader, "id");
                }

                if (user_id == null)
                {
                    errorMessage = "We do not have any user account registered with that email address.";
                    return false;
                }

                Dictionary<string, string> replacements = new Dictionary<string, string>();
                replacements.Add("{**user_id**}", user_id);
                replacements.Add("{**site_url**}", HttpContext.Current.Request.Url.AbsolutePath);

                string email_body = ProjectUtils.WebUtils.GetHTMLFromTemplate("ForgottenEmail", replacements);

                if (!ProjectUtils.SendEmail.SendSupportEmail(ProjectUtils.Constants.SUPPORT_EMAIL, email, null, null,
                    "ELF Password Reset Instructions", email_body))
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "Unable to send Password Reset Email to: {0}", email);

                    errorMessage = String.Format("We were unable to send your password reset email to: {0}.  "
                        + "Please contact {2} with this error code: {1}.", email, log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
                    return false;
                }

                my_access.LogMessage(DBAccess.LOG_LEVEL_INFO, log_cat,
                    "Sent a password reset email to: {0}", email);

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
    }
}
