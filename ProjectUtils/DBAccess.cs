using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;

using System.Threading;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.Common;

namespace ProjectUtils
{
    public partial class DBAccess
    {
        /// <summary>
        /// The DBAccess class provides access to and helper functions for the
        /// database.  All calls to the database should be done through this
        /// class, as all failover/redundnacy/scalability enhancements will make use of this
        /// infrastructure.
        /// </summary>
        private SqlConnection my_connection = null;
        private string owning_name = null;
        private SqlDataReader close_reader = null;
        private SqlTransaction my_transaction = null;

        public static readonly string DEFAULT_CONNECTION_NAME = "ELF";
        private static readonly string SQL_LOG_CAT = "SQLLogs";
        private static readonly int LOG_LEVEL_SQL = 90;

        private string connection_name;
        private string my_connection_string = null;

        public string ConnectionString { get { return my_connection_string; } }

        public bool allow_log_sql = true;

        static DBAccess()
        {
            Utils.LogEventLog("DBAccess static constructor starting", EventLogEntryType.Information, 99, 99);
        }

        /// <summary>
        /// A simple constructor to obtain a connection to the database.
        /// </summary>
        /// <param name="owner">Pass in a reference to the containing class, or a string 
        /// indicating the name of the owning code(for static methods).  
        /// This is used for error/log messages.</param>
        public DBAccess(Object owner)
            : this(owner, DEFAULT_CONNECTION_NAME)
        {
        }

        /// <summary>
        /// Obtain a connection to the a specified database.
        /// </summary>
        /// <param name="owner">Pass in a reference to the containing class, or a string 
        /// indicating the name of the owning code(for static methods).  
        /// This is used for error/log messages.</param>
        /// <param name="connection_name">The name of the connection string specified in the 
        /// app.config/web.config file.
        /// </param>
        public DBAccess(Object owner, string connection_name)
        {
            if (owner == null)
            {
                throw new ArgumentException("DBAccess must be constructed with a valid owner");
            }

            Type owning_type = owner.GetType();

            if (owning_type == typeof(string))
            {
                owning_name = (string)owner;
            }
            else
            {
                if (!owning_type.IsClass)
                {
                    throw new ArgumentException("DBAccess must be constructed with an Class type owner");
                }

                owning_name = owning_type.Name;
            }

            if (connection_name == null || connection_name.Length < 1)
            {
                connection_name = DEFAULT_CONNECTION_NAME;
            }

            this.connection_name = connection_name;
            my_connection_string = GetConnectionString(connection_name);

            my_connection = new SqlConnection(my_connection_string);

            my_connection.Open();

            StackTrace callStack = new StackTrace();
        }

        ~DBAccess()
        {
            try
            {
                if (my_connection != null && !is_logger_dbaccess)
                {
                    // Apparently it is strictly illegal to call methods on other objects in a Finalizer.  I am 
                    // going to risk it here, though, as the reason not to call other objects is that "they might
                    // be being finalized themselves".  Since the LogEventLog is a static method, the only time
                    // this should be possible is if the whole app is shutting down, in which case I don't care
                    // if this call throws an exception, etc.
                    Utils.LogEventLog(String.Format("DBAccess({0}) was not closed", owning_name), EventLogEntryType.Warning, 44, 44);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Closes the connection to the DB, and frees the connection back to .Net's connection
        /// pool.  For performance reasons, it is important that this method be called when the
        /// DBAccess instance is no longer needed.  The destuctor will close an open connection,
        /// however the destructor is not called very often, and so pooled connection threads will
        /// be held open if this method is not called.
        /// </summary>
        public void CloseConnection()
        {
            // Utils.LogEventLog(String.Format("Delete dbaccess for head ({0})", owning_name), EventLogEntryType.Warning, 44, 44);

            if (my_transaction != null)
            {
                string err_msg;
                RollbackTransaction(out err_msg);
            }

            if (close_reader != null && !close_reader.IsClosed)
            {
                close_reader.Close();
            }
            close_reader = null;

            if (my_connection != null && my_connection.State == ConnectionState.Open)
            {
                my_connection.Close();
            }
            my_connection = null;

            if (logger_connection != null)
            {
                logger_connection.CloseConnection();
                logger_connection = null;
            }
        }

        /// <summary>
        /// Begin transaction on current connection.
        /// </summary>
        /// <param name="err_msg">return message if error occurs.</param>
        public void BeginTransaction(out string err_msg)
        {
            err_msg = null;

            if (my_transaction != null)
            {
                err_msg = "Cannot BeginTransaction(), DBAccess is already in transaction";
                return;
            }

            my_transaction = my_connection.BeginTransaction();
        }

        public bool InTransaction()
        {
            return my_transaction != null;
        }

        public void CommitTransaction(out string err_msg)
        {
            err_msg = null;

            if (my_transaction == null)
            {
                err_msg = "Cannot CommitTransaction(), DBAccess is not in transaction";
                return;
            }

            my_transaction.Commit();
            my_transaction = null;
        }

        public void RollbackTransaction(out string err_msg)
        {
            err_msg = null;

            if (my_transaction == null)
            {
                err_msg = "Cannot RollbackTrans(), DBAccess is not in transaction";
                return;
            }

            my_transaction.Rollback();
            my_transaction = null;
        }

        public void BulkCopy(DbDataReader reader, string tableName, out string errMsg)
        {
            errMsg = null;
            SqlBulkCopy bulkCopy = new SqlBulkCopy(my_connection);
            bulkCopy.DestinationTableName = tableName;
            try
            {
                bulkCopy.WriteToServer(reader);
            }
            catch (Exception ex)
            {
                errMsg = String.Format("Got exception in bulk copy: {0}", ex.Message);
            }
            finally
            {
                bulkCopy.Close();
            }
            return;
        }


        /// <summary>
        /// Excutes any SQL command that does not produce a result set, usually INSERT, 
        /// UPDATE, or DELETE.
        /// </summary>
        /// <param name="sql">The SQL string to execute</param>
        /// <returns>The number of rows affected by the command</returns>
        public int ExecuteNonQuery(string sql)
        {
            if (close_reader != null && !close_reader.IsClosed)
            {
                close_reader.Close();
                close_reader = null;
            }

            if (allow_log_sql)
            {
                LogMessage(DBAccess.LOG_LEVEL_SQL, SQL_LOG_CAT, "ExecuteNonQuery: {0}", sql);
            }

            SqlCommand command = new SqlCommand(sql, my_connection);
            command.CommandTimeout = 0;
            command.Transaction = my_transaction;
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Excutes any SQL command that does not produce a result set, usually INSERT, 
        /// UPDATE, or DELETE.  This method does not throw any exceptions, instead if an exception
        /// occurs, it is caught and returned in the err_msg out parameter.
        /// </summary>
        /// <param name="sql">The SQL string to execute</param>
        /// <param name="err_msg">null if the command was successful, otherwise contains the 
        /// exception message of any exception that was caught.</param>
        /// <returns>The number of rows affected by the command, or a -1 if an excepton was caught</returns>
        public int ExecuteNonQuery(string sql, out string err_msg)
        {
            err_msg = null;

            try
            {
                return ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                err_msg = String.Format("DBAccess({0}):ExecuteNonQuery() encountered an exception for SQL: <{1}>  Exception: {2}",
                    owning_name, Utils.ShortString(sql), ex.Message);
            }

            return -1;
        }

        /// <summary>
        /// Excutes a stored procedure that does not produce a result set.
        /// </summary>
        /// <param name="sp_name">The name of the stored procedure to call</param>
        /// <param name="sp_parameters">An arbitrary number of parameters as name/value pairs, 
        /// indicating the parameters to be passed to the stored procedure.  Currently only 
        /// INPUT parameters are supported.</param>
        /// <returns>The number of rows affected by the command</returns>
        public int ExecuteNonQuerySP(string sp_name, params object[] sp_parameters)
        {
            if (close_reader != null && !close_reader.IsClosed)
            {
                close_reader.Close();
                close_reader = null;
            }

            SqlCommand command = new SqlCommand(sp_name, my_connection);
            command.CommandTimeout = 0;
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = my_transaction;

            if (sp_parameters.Length % 3 != 0)
            {
                throw new ArgumentException(String.Format("DBAccess({0}):ExecuteNonQuerySP() "
                    + "received an invalid number of sp_parameters.  "
                    + "SP call: {1}",
                    owning_name), DumpSPParams(sp_name, sp_parameters));
            }

            for (int param_index = 0; param_index < sp_parameters.Length; param_index += 3)
            {
                SqlParameter this_param = new SqlParameter(
                    (string)sp_parameters[param_index],
                    (DbType)sp_parameters[param_index + 1]);
                this_param.Value = sp_parameters[param_index + 2];
                command.Parameters.Add(this_param);
            }

            if (allow_log_sql)
            {
                LogMessage(DBAccess.LOG_LEVEL_SQL, SQL_LOG_CAT,
                  "ExecuteNonQuerySP: {0}", DumpSPCall(sp_name, sp_parameters));
            }

            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Excutes a stored procedure that does not produce a result set.  This method does not 
        /// throw any exceptions, instead if an exception occurs, it is caught and returned in 
        /// the err_msg out parameter.
        /// </summary>
        /// <param name="sp_name">The name of the stored procedure to call</param>
        /// <param name="err_msg">null if the command was successful, otherwise contains the 
        /// exception message of any exception that was caught.</param>
        /// <param name="sp_parameters">An arbitrary number of parameters as name/value pairs, 
        /// indicating the parameters to be passed to the stored procedure.  Currently only 
        /// INPUT parameters are supported.</param>
        /// <returns>The number of rows affected by the command, or a -1 if an excepton 
        /// was caught</returns>
        public int ExecuteNonQuerySP(string sp_name, out string err_msg, params object[] sp_parameters)
        {
            err_msg = null;

            try
            {
                return ExecuteNonQuerySP(sp_name, sp_parameters);
            }
            catch (Exception ex)
            {
                err_msg = String.Format("DBAccess({0}):ExecuteNonQuerySP() encountered an exception while calling stored procedure {1}  Exception: {2}",
                    owning_name, DumpSPCall(sp_name, sp_parameters), ex.Message);
            }

            return -1;
        }

        /// <summary>
        /// Executes the indicated SQL, which should produce a result set, and returns only the 
        /// value of the first column of the first row of the result set.
        /// </summary>
        /// <param name="sql">The SQL string to execute</param>
        /// <returns>The value of the first column of the first row of the result set</returns>
        public object GetScalar(string sql)
        {
            if (close_reader != null && !close_reader.IsClosed)
            {
                close_reader.Close();
                close_reader = null;
            }

            if (allow_log_sql)
            {
                LogMessage(DBAccess.LOG_LEVEL_SQL, SQL_LOG_CAT, "GetScalar: {0}", sql);
            }

            SqlCommand command = new SqlCommand(sql, my_connection);
            command.CommandTimeout = 0;
            command.Transaction = my_transaction;
            return command.ExecuteScalar();
        }

        /// <summary>
        /// Executes the indicated SQL, which should produce a result set, and returns only the 
        /// value of the first column of the first row of the result set.  This method does not 
        /// throw any exceptions, instead if an exception occurs, it is caught and returned 
        /// in the err_msg out parameter.
        /// </summary>
        /// <param name="sql">The SQL string to execute</param>
        /// <param name="err_msg">null if the command was successful, otherwise contains the 
        /// exception message of any exception that was caught.</param>
        /// <returns>The value of the first column of the first row of the result set, or null 
        /// if an exception was caught</returns>
        public object GetScalar(string sql, out string err_msg)
        {
            err_msg = null;

            try
            {
                return GetScalar(sql);
            }
            catch (Exception ex)
            {
                err_msg = String.Format("DBAccess({0}):GetScalar() encountered an exception for SQL: <{1}>  Exception: {2}",
                    owning_name, Utils.ShortString(sql), ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Returns a DataTable containing the first result set returned by the input SQL.
        /// </summary>
        /// <param name="sql">The SQL string to execute</param>
        /// <returns>The first result set returned by the SQL</returns>
        public DataTable GetTable(string sql)
        {
            if (close_reader != null && !close_reader.IsClosed)
            {
                close_reader.Close();
                close_reader = null;
            }

            if (allow_log_sql)
            {
                LogMessage(DBAccess.LOG_LEVEL_SQL, SQL_LOG_CAT, "GetTable: {0}", sql);
            }

            SqlCommand command = new SqlCommand(sql, my_connection);
            command.CommandTimeout = 0;
            command.Transaction = my_transaction;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable retval = new DataTable();
            adapter.Fill(retval);
            return retval;
        }

        /// <summary>
        /// Returns a DataTable containing the first result set returned by the input SQL.
        /// This method does not throw any exceptions, instead if an exception occurs, it 
        /// is caught and returned in the err_msg out parameter.
        /// </summary>
        /// <param name="sql">The SQL string to execute</param>
        /// <param name="err_msg">null if the command was successful, otherwise contains the 
        /// exception message of any exception that was caught.</param>
        /// </summary>
        /// <returns>The first result set returned by the SQL, or null if an exception 
        /// was caught</returns>
        public DataTable GetTable(string sql, out string err_msg)
        {
            err_msg = null;

            try
            {
                return GetTable(sql);
            }
            catch (Exception ex)
            {
                err_msg = String.Format("DBAccess({0}):GetTable() encountered an exception for SQL: <{1}>  Exception: {2}",
                    owning_name, Utils.ShortString(sql), ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Returns a DbDataReader containing the first result set returned by the input SQL.
        /// </summary>
        /// <param name="sql">The SQL string to execute</param>
        /// </summary>
        /// <returns>The first result set returned by the SQL</returns>
        public DbDataReader GetReader(string sql)
        {
            if (close_reader != null && !close_reader.IsClosed)
            {
                close_reader.Close();
                close_reader = null;
            }

            if (allow_log_sql)
            {
                LogMessage(DBAccess.LOG_LEVEL_SQL, SQL_LOG_CAT, "GetReader: {0}", sql);
            }

            SqlCommand command = new SqlCommand(sql, my_connection);
            command.CommandTimeout = 0;
            command.Transaction = my_transaction;
            close_reader = command.ExecuteReader();
            return close_reader;
        }

        /// <summary>
        /// Returns a DbDataReader containing the first result set returned by the input SQL.
        /// This method does not throw any exceptions, instead if an exception occurs, it 
        /// is caught and returned in the err_msg out parameter.
        /// </summary>
        /// <param name="sql">The SQL string to execute</param>
        /// <param name="err_msg">null if the command was successful, otherwise contains the 
        /// exception message of any exception that was caught.</param>
        /// </summary>
        /// <returns>The first result set returned by the SQL, or null if an exception 
        /// was caught</returns>
        public DbDataReader GetReader(string sql, out string err_msg)
        {
            err_msg = null;

            try
            {
                return GetReader(sql);
            }
            catch (Exception ex)
            {
                err_msg = String.Format("DBAccess({0}):GetReader() encountered an exception for SQL: <{1}>  Exception: {2}",
                    owning_name, Utils.ShortString(sql), ex.Message);
            }

            return null;
        }

        private static string DumpSPParams(string sp_name, object[] sp_parameters)
        {
            string retval = sp_name + " ==>";

            foreach (object this_obj in sp_parameters)
            {
                retval += " [" + this_obj.ToString() + "]";
            }

            return retval;
        }

        private static string DumpSPCall(string sp_name, object[] sp_parameters)
        {
            if (sp_parameters.Length % 3 != 0)
            {
                return DumpSPParams(sp_name, sp_parameters);
            }

            string retval = sp_name + "(";

            for (int param_index = 0; param_index < sp_parameters.Length; param_index += 3)
            {
                if (param_index > 0)
                {
                    retval += ", ";
                }

                retval += String.Format("['{0}'({1}) ==> '{2}']",
                    sp_parameters[param_index],
                    sp_parameters[param_index + 1],
                    sp_parameters[param_index + 2]);
            }

            return retval;
        }

        /// <summary>
        /// Executes the indicated stored procedure, which should produce a result set, and 
        /// returns only the value of the first column of the first row of the result set.  
        /// </summary>
        /// <param name="sp_name">The name of the stored procedure to call</param>
        /// <param name="sp_parameters">An arbitrary number of parameters as name/value pairs, 
        /// indicating the parameters to be passed to the stored procedure.  Currently only 
        /// INPUT parameters are supported.</param>
        /// <returns>The value of the first column of the first row of the result set</returns>
        public object GetScalarSP(string sp_name, params object[] sp_parameters)
        {
            if (close_reader != null && !close_reader.IsClosed)
            {
                close_reader.Close();
                close_reader = null;
            }

            SqlCommand command = new SqlCommand(sp_name, my_connection);
            command.CommandTimeout = 0;
            command.Transaction = my_transaction;
            command.CommandType = CommandType.StoredProcedure;

            if (sp_parameters.Length % 3 != 0)
            {
                throw new ArgumentException(String.Format("DBAccess({0}):GetScalarSP() "
                    + "received an invalid number of sp_parameters.  "
                    + "SP call: {1}",
                    owning_name), DumpSPParams(sp_name, sp_parameters));
            }

            for (int param_index = 0; param_index < sp_parameters.Length; param_index += 3)
            {
                SqlParameter this_param = new SqlParameter(
                    (string)sp_parameters[param_index],
                    (DbType)sp_parameters[param_index + 1]);
                this_param.Value = sp_parameters[param_index + 2];
                command.Parameters.Add(this_param);
            }

            if (allow_log_sql)
            {
                LogMessage(DBAccess.LOG_LEVEL_SQL, SQL_LOG_CAT, "GetScalarSP: {0}",
                  DumpSPCall(sp_name, sp_parameters));
            }

            return command.ExecuteScalar();
        }

        /// <summary>
        /// Executes the indicated stored procedure, which should produce a result set, and 
        /// returns only the value of the first column of the first row of the result set.  
        /// This method does not throw any exceptions, instead if an exception occurs, it is 
        /// caught and returned in the err_msg out parameter.
        /// </summary>
        /// <param name="sp_name">The name of the stored procedure to call</param>
        /// <param name="err_msg">null if the command was successful, otherwise contains the 
        /// exception message of any exception that was caught.</param>
        /// <param name="sp_parameters">An arbitrary number of parameters as name/value pairs, 
        /// indicating the parameters to be passed to the stored procedure.  Currently only 
        /// INPUT parameters are supported.</param>
        /// <returns>The value of the first column of the first row of the result set, or null 
        /// if an exception was caught</returns>
        public object GetScalarSP(string sp_name, out string err_msg, params object[] sp_parameters)
        {
            err_msg = null;

            try
            {
                return GetScalarSP(sp_name, sp_parameters);
            }
            catch (Exception ex)
            {
                err_msg = String.Format("DBAccess({0}):GetScalarSP() encountered an exception while calling stored procedure {1}  Exception: {2}",
                    owning_name, DumpSPCall(sp_name, sp_parameters), ex.Message);
            }

            return -1;
        }

        /// <summary>
        /// Executes the indicated stored procedure, which should produce a result set, and 
        /// returns the first result set as a DataTable.
        /// </summary>
        /// <param name="sp_name">The name of the stored procedure to call</param>
        /// <param name="sp_parameters">An arbitrary number of parameters as name/value pairs, 
        /// indicating the parameters to be passed to the stored procedure.  Currently only 
        /// INPUT parameters are supported.</param>
        /// <returns>The first result set</returns>
        public DataTable GetTableSP(string sp_name, params object[] sp_parameters)
        {
            if (close_reader != null && !close_reader.IsClosed)
            {
                close_reader.Close();
                close_reader = null;
            }

            SqlCommand command = new SqlCommand(sp_name, my_connection);
            command.CommandTimeout = 0;
            command.Transaction = my_transaction;
            command.CommandType = CommandType.StoredProcedure;

            if (sp_parameters.Length % 3 != 0)
            {
                throw new ArgumentException(String.Format("DBAccess({0}):GetTableSP() "
                    + "received an invalid number of sp_parameters.  "
                    + "SP call: {1}",
                    owning_name), DumpSPParams(sp_name, sp_parameters));
            }

            for (int param_index = 0; param_index < sp_parameters.Length; param_index += 3)
            {
                SqlParameter this_param = new SqlParameter(
                    (string)sp_parameters[param_index],
                    (DbType)sp_parameters[param_index + 1]);
                this_param.Value = sp_parameters[param_index + 2];
                command.Parameters.Add(this_param);
            }

            if (allow_log_sql)
            {
                LogMessage(DBAccess.LOG_LEVEL_SQL, SQL_LOG_CAT, "GetTableSP: {0}", DumpSPCall(sp_name, sp_parameters));
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable retval = new DataTable();
            adapter.Fill(retval);
            return retval;
        }

        /// <summary>
        /// Executes the indicated stored procedure, which should produce a result set, and 
        /// returns the first result set as a DataTable.  This method does not throw any exceptions, 
        /// instead if an exception occurs, it is caught and returned in the err_msg out parameter.
        /// </summary>
        /// <param name="sp_name">The name of the stored procedure to call</param>
        /// <param name="err_msg">null if the command was successful, otherwise contains the 
        /// exception message of any exception that was caught.</param>
        /// <param name="sp_parameters">An arbitrary number of parameters as name/value pairs, 
        /// indicating the parameters to be passed to the stored procedure.  Currently only 
        /// INPUT parameters are supported.</param>
        /// <returns>The first result set, or null if an exception has occurred</returns>
        public DataTable GetTableSP(string sp_name, out string err_msg, params object[] sp_parameters)
        {
            err_msg = null;

            try
            {
                return GetTableSP(sp_name, sp_parameters);
            }
            catch (Exception ex)
            {
                err_msg = String.Format("DBAccess({0}):GetTableSP() encountered an exception while calling stored procedure {1}  Exception: {2}",
                    owning_name, DumpSPCall(sp_name, sp_parameters), ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Executes the indicated stored procedure, which should produce a result set, and 
        /// returns the first result set as a DbDataReader.
        /// </summary>
        /// <param name="sp_name">The name of the stored procedure to call</param>
        /// <param name="sp_parameters">An arbitrary number of parameters as name/value pairs, 
        /// indicating the parameters to be passed to the stored procedure.  Currently only 
        /// INPUT parameters are supported.</param>
        /// <returns>The first result set</returns>
        public DbDataReader GetReaderSP(string sp_name, params object[] sp_parameters)
        {
            if (close_reader != null && !close_reader.IsClosed)
            {
                close_reader.Close();
                close_reader = null;
            }

            SqlCommand command = new SqlCommand(sp_name, my_connection);
            command.CommandTimeout = 0;
            command.Transaction = my_transaction;
            command.CommandType = CommandType.StoredProcedure;

            if (sp_parameters.Length % 3 != 0)
            {
                throw new ArgumentException(String.Format("DBAccess({0}):GetReaderSP() "
                    + "received an invalid number of sp_parameters.  "
                    + "SP call: {1}",
                    owning_name), DumpSPParams(sp_name, sp_parameters));
            }

            for (int param_index = 0; param_index < sp_parameters.Length; param_index += 3)
            {
                SqlParameter this_param = new SqlParameter(
                    (string)sp_parameters[param_index],
                    (DbType)sp_parameters[param_index + 1]);
                this_param.Value = sp_parameters[param_index + 2];
                command.Parameters.Add(this_param);
            }

            if (allow_log_sql)
            {
                LogMessage(DBAccess.LOG_LEVEL_SQL, SQL_LOG_CAT, "GetReaderSP: {0}",
                  DumpSPCall(sp_name, sp_parameters));
            }

            close_reader = command.ExecuteReader();
            return close_reader;
        }

        /// <summary>
        /// Executes the indicated stored procedure, which should produce a result set, and 
        /// returns the first result set as a DbDataReader.  This method does not throw any exceptions, 
        /// instead if an exception occurs, it is caught and returned in the err_msg out parameter.
        /// </summary>
        /// <param name="sp_name">The name of the stored procedure to call</param>
        /// <param name="err_msg">null if the command was successful, otherwise contains the 
        /// exception message of any exception that was caught.</param>
        /// <param name="sp_parameters">An arbitrary number of parameters as name/value pairs, 
        /// indicating the parameters to be passed to the stored procedure.  Currently only 
        /// INPUT parameters are supported.</param>
        /// <returns>The first result set, or null if an exception has occurred</returns>
        public DbDataReader GetReaderSP(string sp_name, out string err_msg,
            params object[] sp_parameters)
        {
            err_msg = null;

            try
            {
                return GetReaderSP(sp_name, sp_parameters);
            }
            catch (Exception ex)
            {
                err_msg = String.Format("DBAccess({0}):GetReaderSP() encountered an exception while calling stored procedure {1}  Exception: {2}",
                    owning_name, DumpSPCall(sp_name, sp_parameters), ex.Message);
            }

            return null;
        }

        private int MAX_QUEUE_STRINGS = 1000;

        public int BulkSQLMaxCommandQueueSize { get { return MAX_QUEUE_STRINGS; } set { MAX_QUEUE_STRINGS = value < 1 ? 1 : value; } }

        private List<string> bulk_update_sql_queue = new List<string>();

        public int BulkSQLStatementCount { get { return bulk_update_sql_queue.Count; } }

        public void BulkSQLClearQueue()
        {
            bulk_update_sql_queue.Clear();
        }

        /// <summary>
        /// Stores an SQL command that does not produce a result set, usually INSERT, 
        /// UPDATE, or DELETE for future "bulk" updates.  Used for performance imporvements
        /// when the connection to to DB is slow.  Transaction boundaries are not respected,
        /// so if it is important that a particular SQL statement be executed, call
        /// BulkSQLFlushQueue().
        /// NOTE: Calling this function may cause the queued SQL statements to be executed
        /// if the maximum queue size is exceeded, so you still have to handle any
        /// exceptions that might be produced by executing SQL.
        /// </summary>
        /// <param name="sql">The SQL string to execute</param>
        /// <returns>The number of rows affected by the commands in the queue if the queue 
        /// was executed successfully, or a -1 if the queue was executed and encountered an 
        /// error, or a -2 if the queue was not executed at all</returns>
        public int BulkSQLAddSQL(string sql)
        {
            if (allow_log_sql)
            {
                LogMessage(DBAccess.LOG_LEVEL_SQL, SQL_LOG_CAT, "BulkSQLAddSQL: {0}", sql);
            }

            if (sql == null || sql.Length < 1)
            {
                return -2;
            }

            sql = sql.TrimEnd(" ;".ToCharArray());

            bulk_update_sql_queue.Add(sql);

            if (bulk_update_sql_queue.Count >= MAX_QUEUE_STRINGS)
            {
                try
                {
                    return BulkSQLFlushQueue();
                }
                catch (Exception ex)
                {
                    throw new ExceptionEx(
                      "DBAccess({0}):BulkSQLAddSQL() encountered an exception while flushing queue "
                      + "for SQL: <{1}>  Exception: {2}",
                      owning_name, Utils.ShortString(String.Join(";", bulk_update_sql_queue.ToArray())), ex.Message);
                }
            }

            return -2;
        }

        /// <summary>
        /// Stores an SQL command that does not produce a result set, usually INSERT, 
        /// UPDATE, or DELETE for future "bulk" updates.  Used for performance imporvements
        /// when the connection to to DB is slow.  Transaction boundaries are not respected,
        /// so if it is important that a particular SQL statement be executed, call
        /// BulkSQLFlushQueue().  This method does not throw any exceptions, instead if an exception
        /// occurs, it is caught and returned in the err_msg out parameter.
        /// NOTE: Calling this function may cause the queued SQL statements to be executed
        /// if the maximum queue size is exceeded, so you still have to handle any error
        /// messages that might be produced by executing SQL.
        /// </summary>
        /// <param name="sql">The SQL string to execute</param>
        /// <param name="err_msg">null if the command was successful, otherwise contains the 
        /// exception message of any exception that was caught.</param>
        /// <returns>The number of rows affected by the commands in the queue if the queue 
        /// was executed successfully, or a -1 if the queue was not executed, or a -2 if the 
        /// queue was executed and encountered an error</returns>
        public int BulkSQLAddSQL(string sql, out string err_msg)
        {
            err_msg = null;

            try
            {
                return BulkSQLAddSQL(sql);
            }
            catch (Exception ex)
            {
                err_msg = String.Format(
                  "DBAccess({0}):BulkSQLAddSQL() encountered an exception for SQL: <{1}>  Exception: {2}",
                  owning_name, Utils.ShortString(sql), ex.Message);
            }

            return -1;
        }

        /// <summary>
        /// Executes any SQL statements stored in the "bulk update" queue.
        /// </summary>
        /// <returns>The number of rows affected by the commands in the queue</returns>
        public int BulkSQLFlushQueue()
        {
            if (close_reader != null && !close_reader.IsClosed)
            {
                close_reader.Close();
                close_reader = null;
            }

            if (allow_log_sql)
            {
                LogMessage(DBAccess.LOG_LEVEL_SQL, SQL_LOG_CAT, "BulkSQLFlushQueue: Executing {0} sql statements",
                  bulk_update_sql_queue.Count);
            }

            string bulk_sql = String.Join(";", bulk_update_sql_queue.ToArray());
            bulk_update_sql_queue.Clear();

            if (String.IsNullOrEmpty(bulk_sql))
            {
                return 0;
            }

            SqlCommand command = new SqlCommand(bulk_sql, my_connection);
            command.CommandTimeout = 0;
            command.Transaction = my_transaction;
            int retval = command.ExecuteNonQuery();
            return retval;
        }

        /// <summary>
        /// Executes any SQL statements stored in the "bulk update" queue.
        /// This method does not throw any exceptions, instead if an exception
        /// occurs, it is caught and returned in the err_msg out parameter.
        /// </summary>
        /// <param name="err_msg">null if the command was successful, otherwise contains the 
        /// exception message of any exception that was caught.</param>
        public int BulkSQLFlushQueue(out string err_msg)
        {
            err_msg = null;

            try
            {
                return BulkSQLFlushQueue();
            }
            catch (Exception ex)
            {
                err_msg = String.Format(
                  "DBAccess({0}):BulkSQLFlushQueue() encountered an exception for SQL: <{1}>  Exception: {2}",
                  owning_name, Utils.ShortString(String.Join(";", bulk_update_sql_queue.ToArray())), ex.Message);
            }

            return -1;
        }
    }
}
