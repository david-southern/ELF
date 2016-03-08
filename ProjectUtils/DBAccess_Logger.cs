using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Threading;
using System.Net;
using System.Data.Common;

namespace ProjectUtils
{
    public partial class DBAccess
    {
        public static readonly int LOG_LEVEL_DEBUG = 70;
        public static readonly int LOG_LEVEL_INFO = 50;
        public static readonly int LOG_LEVEL_WARN = 30;
        public static readonly int LOG_LEVEL_ERROR = 10;
        public static readonly int LOG_LEVEL_FATAL = 0;

        private static readonly int DEFAULT_LOG_LEVEL = LOG_LEVEL_WARN;

        private static object mt_lock = new object();
        private static Dictionary<string, int> cached_log_levels = new Dictionary<string, int>();
        private static DateTime cache_refresh_time = DateTime.UtcNow;

        private bool is_logger_dbaccess = false;
        private DBAccess logger_connection = null;

        public int LogMessage(int log_level, string log_category,
            string message_format, params object[] message_objects)
        {
            if (is_logger_dbaccess)
            {
                return -2;
            }

            string log_sql, err_msg;

            try
            {
                if (logger_connection == null)
                {
                    logger_connection = new DBAccess(String.Format("DB Logger({0})", owning_name), connection_name);
                    logger_connection.is_logger_dbaccess = true;
                }

                lock (mt_lock)
                {
                    if (DateTime.UtcNow >= cache_refresh_time)
                    {
                        cached_log_levels = new Dictionary<string, int>();
                        cache_refresh_time = DateTime.UtcNow.AddMinutes(1);

                        string sql;

                        sql = "	SELECT COALESCE(log_category, '') as log_category, log_level FROM LogSettings;";

                        DbDataReader reader = logger_connection.GetReader(sql, out err_msg);

                        if (err_msg != null)
                        {
                            // Try and log an error message, just in case
                            string try_log_message = String.Format("Error trying to cache log levels: {0}", err_msg);
                            int try_message_thread_id = Thread.CurrentThread.ManagedThreadId;
                            string try_hostname = Dns.GetHostName();

                            log_sql = String.Format(@"
                                INSERT INTO LogMessages (
                                    log_level, log_owner, message_category, message_text, type_id, 
                                    thread_id, hostname
                                ) VALUES (
                                    {0}, {1}, {2}, {3}, {4}, 
                                    {5}, {6}
                                );
                                SELECT SCOPE_IDENTITY();",
                                DBAccess.FormatSQLValue(LOG_LEVEL_FATAL),
                                DBAccess.FormatSQLValue("DBAccess"),
                                DBAccess.FormatSQLValue("LogCache"),
                                DBAccess.FormatSQLValue(try_log_message),
                                DBAccess.FormatSQLValue(0),
                                DBAccess.FormatSQLValue(try_message_thread_id),
                                DBAccess.FormatSQLValue(try_hostname));

                            logger_connection.GetScalar(log_sql, out err_msg);
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                string log_cat = (string)reader["log_category"];
                                int cat_log_level = (int)reader["log_level"];

                                if (log_cat.Length == 0)
                                {
                                    log_cat = "*";
                                }

                                cached_log_levels.Add(log_cat, cat_log_level);
                            }
                        }
                    }
                }

                int target_log_level = DEFAULT_LOG_LEVEL;

                if (cached_log_levels.ContainsKey(log_category))
                {
                    target_log_level = cached_log_levels[log_category];
                }
                else if (cached_log_levels.ContainsKey("*"))
                {
                    target_log_level = cached_log_levels["*"];
                }

                if (log_level > target_log_level)
                {
                    return -1;
                }

                string log_message = String.Format(message_format, message_objects);
                int message_type_id = message_format.GetHashCode();
                int message_thread_id = Thread.CurrentThread.ManagedThreadId;
                string hostname = Dns.GetHostName();

                log_sql = String.Format(@"
                    INSERT INTO LogMessages (
                        log_level, log_owner, message_category, message_text, type_id, 
                        thread_id, hostname
                    ) VALUES (
                        {0}, {1}, {2}, {3}, {4}, 
                        {5}, {6}
                    );
                    SELECT SCOPE_IDENTITY();",
                    DBAccess.FormatSQLValue(log_level),
                    DBAccess.FormatSQLValue(owning_name),
                    DBAccess.FormatSQLValue(log_category),
                    DBAccess.FormatSQLValue(log_message),
                    DBAccess.FormatSQLValue(message_type_id),
                    DBAccess.FormatSQLValue(message_thread_id),
                    DBAccess.FormatSQLValue(hostname));

                object log_id_obj = logger_connection.GetScalar(log_sql, out err_msg);

                SendLogMail(log_level, log_message, message_type_id, message_thread_id, hostname);

                return Utils.SafeInt(log_id_obj, -1);
            }
            catch (Exception ex)
            {
                try
                {
                    string log_message = String.Format(message_format, message_objects);
                    int message_type_id = message_format.GetHashCode();
                    int message_thread_id = Thread.CurrentThread.ManagedThreadId;
                    string hostname = Dns.GetHostName();
                    log_message += "|message_type_id:" + message_type_id
                        + "|message_thread_id:" + message_thread_id + "|hostname:" + hostname
                        + "|exception:" + ex.Message + "|stack trace:" + ex.StackTrace;

                    SendLogMail(LOG_LEVEL_ERROR, log_message, message_type_id, message_thread_id, hostname);

                    Utils.LogEventLog("Got Error in logger logging message. ",
                        System.Diagnostics.EventLogEntryType.Error, 99, 99);
                }
                catch (Exception)
                {
                    // shoot
                }
            }

            return -1;
        }

        private void SendLogMail(int log_level, string log_message, int message_type_id,
            int message_thread_id, string hostname)
        {
            if (!cached_log_levels.ContainsKey("SendEmailOnLog"))
            {
                return;
            }
            int target_log_level = cached_log_levels["SendEmailOnLog"];
            if (log_level <= target_log_level)
            {
                string bodymessage = log_message += "|message_type_id:"
                    + message_type_id + "|message_thread_id:" + message_thread_id + "|hostname:" + hostname;
                SendEmail.SendSupportEmail(null, "admin@e-l-fun.com", null, null,
                    "ELF Server Error", bodymessage);
            }
        }
    }
}
