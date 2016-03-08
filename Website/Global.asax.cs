using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using ProjectUtils;

namespace Website
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Don't do DB access for portfolio site
            return;

            DBAccess my_access = null;
            string log_cat = "Session_Start";

            try
            {
                string sql, err_msg;

                my_access = new DBAccess(this);

                string clientLanguages = Request.UserLanguages == null ?
        null : String.Join("|", Request.UserLanguages);

                sql = String.Format("INSERT INTO CustMetrics "
                    + " (url_referrer, browser, browser_id, browser_major_version, browser_minor_version, "
                    + "platform, user_languages, user_host_ip, user_agent) "
                    + " VALUES "
                    + " ({0}, {1}, {2}, {3}, {4}, "
                    + "{5}, {6}, {7}, {8}) ",
                    DBAccess.FormatSQLValue(Utils.SafeString(Request.UrlReferrer, "<unset>")),
                    DBAccess.FormatSQLValue(Utils.SafeString(Request.Browser.Browser, "<unset>")),
                    DBAccess.FormatSQLValue(Utils.SafeString(Request.Browser.Id, "<unset>")),
                    DBAccess.FormatSQLValue(Utils.SafeString(Request.Browser.MajorVersion, "<unset>")),
                    DBAccess.FormatSQLValue(Utils.SafeString(Request.Browser.MinorVersionString, "<unset>")),
                    DBAccess.FormatSQLValue(Utils.SafeString(Request.Browser.Platform, "<unset>")),
                    DBAccess.FormatSQLValue(Utils.SafeString(clientLanguages, "<unset>")),
                    DBAccess.FormatSQLValue(Utils.SafeString(Request.UserHostAddress, "<unset>")),
                    DBAccess.FormatSQLValue(Utils.SafeString(Request.UserAgent, "<unset>"))
                    );

                my_access.ExecuteNonQuery(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "Error while inserting customer metrics: {1}", err_msg);
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

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}