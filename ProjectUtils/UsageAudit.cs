using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace ProjectUtils
{
    public static class UsageAudit
    {
        public enum EventType
        {
            UserLogin = 1,
            UserLogout,
        }

        public static void LogUsage(EventType eventType, string user_id, string context)
        {
            DBAccess my_access = null;
            string log_cat = "LogUsage";

            try
            {
                if(HttpContext.Current == null)
                {
                    return;
                }

                my_access = new DBAccess("UsageAudit");

                string sql, err_msg;

                sql = String.Format("INSERT INTO UsageAudit ( user_id, event_id, context ) VALUES ( {0}, {1}, {2} )",
                    DBAccess.FormatSQLValue(user_id),
                    DBAccess.FormatSQLValue((int)eventType),
                    DBAccess.FormatSQLValue(context)
                    );

                my_access.ExecuteNonQuery(sql, out err_msg);

                if (err_msg != null)
                {
                    my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat, 
                        "DB Error while inserting UsageAudit record for user_id: {0}, event: {1}, context: {2}.  Error: {3}",
                        user_id, eventType.ToString(), context, err_msg);
                    return;
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
