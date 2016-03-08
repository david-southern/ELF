using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Net;
using System.Data.Common;

namespace ProjectUtils
{
    public partial class DBAccess
    {
        public static string GetConnectionString(string connection_name)
        {
            if (ConfigurationManager.ConnectionStrings != null
                && ConfigurationManager.ConnectionStrings[connection_name] != null)
            {
                return ConfigurationManager.ConnectionStrings[connection_name].ConnectionString;
            }

            throw new ExceptionEx(
              "Error, DBAccess ConnectionString was not specified using connection name '{0}'",
              connection_name);
        }

        /// <summary>
        /// A helper method that formats a DateTime into a string representation of the date that is
        /// valid to be used in SQL passed to the DB
        /// </summary>
        public static string FormatDBDateTime(DateTime date_time)
        {
            return date_time.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// A helper method that formats a DateTime into a string representation of the date that is
        /// valid to be used in SQL passed to the DB.  The formatted time include milliseconds.
        /// </summary>
        public static string FormatDBDateTimeHiRes(DateTime date_time)
        {
            return date_time.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        /// A helper method that prevents SQL injection assuming that the input string represents
        /// an integer value.
        /// </summary>
        public static string PreventIntSQLInjection(string input)
        {
            long value = Utils.SafeLong(input, 0);
            return value.ToString();
        }

        /// <summary>
        /// A helper method that prevents SQL injection assuming that the input string represents
        /// an floating-point value.
        /// </summary>
        public static string PreventFloatSQLInjection(string input)
        {
            double value = Utils.SafeDouble(input, 0);
            return value.ToString();
        }

        /// <summary>
        /// A helper method that prevents SQL injection assuming that the input string represents
        /// an string value.
        /// </summary>
        public static string PreventStringSQLInjection(string input)
        {
            string value = Utils.SafeString(input);
            value = value.Replace("'", "''");
            return value;
        }

        /// <summary>
        /// Converts the input string to one that can safely be used in SQL statements.
        /// The resulting string will already be quoted, so do not add quotes yourself.
        /// </summary>
        public static string EscapeSQLString(string input)
        {
            string value = Utils.SafeString(input);
            value = value.Replace("'", "''");
            return "'" + value + "'";
        }

        /// <summary>
        /// Attempts to format the input object in the correct form to be used in SQL.  Any null
        /// input returns NULL.  For value types, an input of the type's MaxValues is considered to
        /// be a NULL as well.  In addition, strings and dates are quoted, and strings are also 
        /// escaped so that single quotes won't mess up the SQL.
        /// </summary>
        /// <param name="thingy"></param>
        /// <returns></returns>
        public static string FormatSQLValue(object thingy)
        {
            if (thingy == null || Utils.IsMaxValue(thingy))
            {
                return "NULL";
            }

            if (thingy is string)
            {
                return EscapeSQLString((string)thingy);
            }

            // Check the more common value types first
            if (thingy is int || thingy is float || thingy is double || thingy is long)
            {
                return thingy.ToString();
            }

            if (thingy is DateTime)
            {
                return "'" + FormatDBDateTime((DateTime)thingy) + "'";
            }

            if (thingy is bool)
            {
                return ((bool)thingy) ? "1" : "0";
            }

            // Check the less common value types 
            if (thingy is short || thingy is sbyte || thingy is byte
                || thingy is uint || thingy is ushort || thingy is ulong
                || thingy is decimal)
            {
                return thingy.ToString();
            }

            if (thingy is char)
            {
                return EscapeSQLString(Convert.ToString(thingy));
            }

            throw new ExceptionEx("Unexpected type {0} in FormatSQLValue", thingy.GetType().ToString());
        }

        /// <summary>
        /// Applies FormatSQLValue to all input SQLValues objects, and then uses the converted
        /// parameter objects to format the input SQLFormatString to return completed SQL.
        /// </summary>
        /// <param name="thingy"></param>
        /// <returns></returns>
        public static string FormatSQL(string SQLFormatString, params object[] SQLValues)
        {
            for (int convertIndex = 0; convertIndex < SQLValues.Length; convertIndex++)
            {
                SQLValues[convertIndex] = FormatSQLValue(SQLValues[convertIndex]);
            }

            return String.Format(SQLFormatString, SQLValues);
        }

        /// <summary>
        /// Given a reader and a column name, attempt to return a string value for the column.  
        /// DBNull.Value will be converted to a simple null, and any non-string objects will be
        /// rendered using ToString() method
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="col_name"></param>
        /// <returns></returns>
        public static string SafeString(DbDataReader reader, string col_name)
        {
            if (reader[col_name] == null || reader[col_name] == DBNull.Value)
            {
                return null;
            }

            return reader[col_name].ToString();
        }

        /// <summary>
        /// Tests the input parameter to see if it is either NULL or DBNull.Value.
        /// </summary>
        /// <param name="thingy"></param>
        /// <returns></returns>
        public static bool IsNull(object thingy)
        {
            return thingy == null || thingy == DBNull.Value;
        }

    }
}
