
namespace Smart.DBUtility
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.Data.OracleClient;
    using System.Configuration;


    /// <summary>
    /// Oracle 数据访问
    /// </summary>
    public abstract class OracleHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString = Smart.Security.Encrypter.Decrypt(ConfigurationManager.AppSettings["ConnectionStrings"], ConfigurationManager.AppSettings["EncryptKey"]);

        /// <summary>
        /// Execute a database query which does not include a select
        /// </summary>
        /// <param name="connectionString">Connection string to database</param>
        /// <param name="commandType">Command type either stored procedure or SQL</param>
        /// <param name="commandText">Acutall SQL Command</param>
        /// <param name="commandParameters">Parameters to bind to the command</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            // Create a new Oracle command
            OracleCommand cmd = new OracleCommand();

            //Create a connection
            using (OracleConnection connection = new OracleConnection(connectionString))
            {

                //Prepare the command
                PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);

                //Execute the command
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute an OracleCommand (that returns no resultset) against an existing database transaction 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
        /// </remarks>
        /// <param name="trans">an existing database transaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(OracleTransaction trans, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = trans.Connection.CreateCommand();
            PrepareCommand(cmd, trans.Connection, trans, commandType, commandText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Execute an OracleCommand (that returns no resultset) against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
        /// </remarks>
        /// <param name="connection">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {

            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        /// <summary>
        /// 执行非查询操作的重载，增加了是否清除oracle参数的参数字段 
        /// </summary>
        /// <param name="command">数据库连接命令对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令</param>
        /// <param name="isClearPara">是否清除oracle参数</param>
        /// <param name="commandParameters">oracle参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(OracleCommand command, CommandType commandType, string commandText, bool isClearPara, params OracleParameter[] commandParameters)
        {
            command.Connection = new OracleConnection(ConnectionString);
            command.CommandType = commandType;
            command.CommandText = commandText;
            if (commandParameters != null)
            {
                foreach (OracleParameter parm in commandParameters)
                {
                    command.Parameters.Add(parm);
                }
            }
            command.Connection.Open();
            int val = command.ExecuteNonQuery();
            if (isClearPara)
            {
                command.Parameters.Clear();
            }
            return val;
        }
        /// <summary>
        /// 执行非查询操作的重载，增加了是否清除oracle参数的参数字段 
        /// </summary>
        /// <param name="command">数据库连接命令对象</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(ref OracleCommand command)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                command.Connection = conn;
                command.Connection.Open();
                int val = command.ExecuteNonQuery();
                return val;
            }
        }
        /// <summary>
        /// 执行SQL语句操作数据库
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string commandText, params OracleParameter[] commandParameters) {

            using (OracleConnection conn = new OracleConnection(ConnectionString)) {
                OracleCommand cmd = new OracleCommand();
                try {
                    if (conn.State != ConnectionState.Open) {
                        conn.Open();
                    }
                    cmd = BuildCommand(conn, commandText, commandParameters);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    conn.Close();
                    return val;
                } catch (System.Data.OracleClient.OracleException ex) {
                    if (conn.State != ConnectionState.Closed) {
                        conn.Close();
                    }
                    throw new Exception(ex.Message);
                } finally {
                    if (conn.State != ConnectionState.Closed) {
                        conn.Close();
                    }
                    cmd.Dispose();
                }
            }
        }

        /// <summary>
        /// 执行SQL语句操作数据库
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(string cmdText)
        {

            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                StringBuilder commandText = new StringBuilder(cmdText);
                OracleCommand cmd = new OracleCommand(commandText.ToString(), conn);
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    int val = cmd.ExecuteNonQuery();
                    conn.Close();
                    return val;
                }
                catch (Exception err)
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                    throw new Exception(err.Message);
                }
            }

        }
        /// <summary>
        /// Execute a select query that will return a result set
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns></returns>
        public static OracleDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {

            //Create the command and connection
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(connectionString);

            try
            {
                //Prepare the command to execute
                PrepareCommand(cmd, conn, null, commandType, commandText, commandParameters);

                //Execute the query, stating that the connection should close when the resulting datareader has been read
                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;

            }
            catch
            {

                //If an error occurs close the connection as the reader will not be used and we expect it to close the connection
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行Reader.提供从数据源读取数据行的只进流的方法
        /// </summary>
        /// <param name="commandText">SQL文本命令</param>
        /// <returns>OracleDataReader</returns>
        public static OracleDataReader ExecuteReader(string commandText)
        {

            //Create the command and connection
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(ConnectionString);

            try
            {
                //Prepare the command to execute
                PrepareCommand(cmd, conn, null, CommandType.Text, commandText, null);

                //Execute the query, stating that the connection should close when the resulting datareader has been read
                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return rdr;

            }
            catch
            {

                //If an error occurs close the connection as the reader will not be used and we expect it to close the connection
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行查询，并将查询返回的结果集中第一行的第一列作为.Net的数据类型返回。忽略额外的列或行。
        /// Execute an OracleCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connectionString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, commandType, commandText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        ///	<summary>
        /// 执行查询，并将查询返回的结果集中第一行的第一列作为.Net的数据类型返回。忽略额外的列或行。
        ///	Execute	a OracleCommand (that returns a 1x1 resultset)	against	the	specified SqlTransaction
        ///	using the provided parameters.
        ///	</summary>
        ///	<param name="transaction">A	valid SqlTransaction</param>
        ///	<param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        ///	<param name="commandText">The stored procedure name	or PL/SQL command</param>
        ///	<param name="commandParameters">An array of	OracleParamters used to execute the command</param>
        ///	<returns>An	object containing the value	in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked	or commited, please	provide	an open	transaction.", "transaction");

            // Create a	command	and	prepare	it for execution
            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

            // Execute the command & return	the	results
            object retval = cmd.ExecuteScalar();

            // Detach the SqlParameters	from the command object, so	they can be	used again
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// 执行查询，并将查询返回的结果集中第一行的第一列作为.Net的数据类型返回。忽略额外的列或行。
        /// Execute an OracleCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(conn, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
        /// </remarks>
        /// <param name="connection">一个现有的数据库连接 </param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行查询，并将查询返回的结果集中第一行的第一列作为.Net的数据类型返回。忽略额外的列或行。
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns></returns>
        /// 创建标识：张晋20070508
        public static object ExecuteScalar(CommandType commandType, string commandText)
        {
            OracleCommand cmd = new OracleCommand();

            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                PrepareCommand(cmd, conn, null, commandType, commandText, null);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行查询，并将查询返回的结果集中第一行的第一列作为.Net的数据类型返回。忽略额外的列或行。
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <returns></returns>
        /// 创建标识：张晋20070508
        public static Object ExecuteScalar(string commandText)
        {
            OracleCommand cmd = new OracleCommand();

            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, commandText, null);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Internal function to prepare a command for execution by the database
        /// </summary>
        /// <param name="cmd">Existing command object</param>
        /// <param name="conn">Database connection object</param>
        /// <param name="trans">Optional transaction object</param>
        /// <param name="commandType">Command type, e.g. stored procedure</param>
        /// <param name="commandText">Command test</param>
        /// <param name="commandParameters">Parameters for the command</param>
        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType commandType, string commandText, OracleParameter[] commandParameters)
        {

            //Open the connection if required
            if (conn.State != ConnectionState.Open)
                conn.Open();

            //Set up the command
            cmd.Connection = conn;
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            //Bind it to the transaction if it exists
            if (trans != null)
                cmd.Transaction = trans;

            // Bind the parameters passed in
            if (commandParameters != null)
            {
                foreach (OracleParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
        }

        ///// <summary>
        ///// Converter to use boolean data type with Oracle
        ///// </summary>
        ///// <param name="value">Value to convert</param>
        ///// <returns></returns>
        //public static string OraBit(bool value)
        //{
        //    if (value)
        //        return "Y";
        //    else
        //        return "N";
        //}

        ///// <summary>
        ///// Converter to use boolean data type with Oracle
        ///// </summary>
        ///// <param name="value">Value to convert</param>
        ///// <returns></returns>
        //public static bool OraBool(string value)
        //{
        //    if (value.Equals("Y"))
        //        return true;
        //    else
        //        return false;
        //}

        ///// <summary>
        ///// 执行存储过程  (使用该方法切记要手工关闭SqlDataReader和连接)
        ///// </summary>
        ///// <param name="storedProcName">存储过程名</param>
        ///// <param name="parameters">存储过程参数</param>
        ///// <returns>SqlDataReader</returns>
        //public static OracleDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        //{
        //    OracleConnection connection = new OracleConnection(ConnectionString);
        //    OracleDataReader returnReader;
        //    connection.Open();
        //    OracleCommand command = BuildQueryCommand(connection, storedProcName, parameters);
        //    command.CommandType = CommandType.StoredProcedure;
        //    returnReader = command.ExecuteReader();
        //    //Connection.Close(); 不能在此关闭，否则，返回的对象将无法使用            
        //    return returnReader;
        //}

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, OracleParameter[] parameters, string tableName)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                OracleDataAdapter oracleDA = new OracleDataAdapter();
                oracleDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                oracleDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, OracleParameter[] parameters)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                OracleDataAdapter oracleDA = new OracleDataAdapter();
                oracleDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                oracleDA.Fill(dataSet);
                connection.Close();
                return dataSet;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数集</param>
        /// <param name="out_result">int型输出值</param>
        /// <param name="out_msg">输出信息</param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref string out_msg, ref int out_result)
        {

            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["out_result"].Value != DBNull.Value)
                        out_result = int.Parse(cmd.Parameters["out_result"].Value.ToString());
                    if (cmd.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)cmd.Parameters["out_msg"].Value;
                    connection.Close();

                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }

            }

        }

        /// <summary>
        ///  获取
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="out_msg"></param>
        /// <param name="out_result"></param>
        public static void RunProcedureOutValue(string storedProcName, OracleParameter[] parameters, ref string out_msg, ref int out_result, ref int outValue)
        {

            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["out_result"].Value != DBNull.Value)
                        out_result = int.Parse(cmd.Parameters["out_result"].Value.ToString());
                    if (cmd.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)cmd.Parameters["out_msg"].Value;
                    if (cmd.Parameters["prmoutvalue"].Value != DBNull.Value)
                        outValue = int.Parse(cmd.Parameters["prmoutvalue"].Value.ToString());
                    connection.Close();

                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }

            }

        }


        /// <summary>
        /// 申请充值接口
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="out_msg"></param>
        /// <param name="out_result"></param>
        /// <param name="planid"></param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref string out_msg, ref int out_result, ref int planid)
        {

            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["prmplanid"].Value != DBNull.Value)
                        planid = int.Parse(cmd.Parameters["prmplanid"].Value.ToString());
                    if (cmd.Parameters["out_result"].Value != DBNull.Value)
                        out_result = int.Parse(cmd.Parameters["out_result"].Value.ToString());
                    if (cmd.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)cmd.Parameters["out_msg"].Value;
                    connection.Close();

                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }

            }

        }

        /// <summary>
        /// 执行存储过程（一个decimal型和一个字符型输出参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数集</param>
        /// <param name="out_result">decimal型输出数值</param>
        /// <param name="out_msg">输出信息</param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref decimal out_result, ref string out_msg)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["out_result"].Value != DBNull.Value)
                        out_result = decimal.Parse(cmd.Parameters["out_result"].Value.ToString());
                    if (cmd.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)cmd.Parameters["out_msg"].Value;
                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 执行存储过程（卡登记）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数集</param>
        /// <param name="out_result">decimal型输出数值</param>
        /// <param name="out_msg">输出信息</param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref int customerID, ref decimal cardNo, ref string id, ref string noUsedate, ref decimal out_result, ref string out_msg)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["out_result"].Value != DBNull.Value)
                        out_result = decimal.Parse(cmd.Parameters["out_result"].Value.ToString());
                    if (cmd.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)cmd.Parameters["out_msg"].Value;

                    if (cmd.Parameters["prmcustomerid"].Value != DBNull.Value)
                    {
                        customerID = int.Parse(cmd.Parameters["prmcustomerid"].Value.ToString());
                    }
                    if (cmd.Parameters["prmcardno"].Value != DBNull.Value)
                    {
                        cardNo = decimal.Parse(cmd.Parameters["prmcardno"].Value.ToString());
                    }
                    if (cmd.Parameters["prmid"].Value != DBNull.Value)
                    {
                        id = (String)cmd.Parameters["prmid"].Value;
                    }
                    if (cmd.Parameters["prmnousedate"].Value != DBNull.Value)
                    {
                        noUsedate = (String)cmd.Parameters["prmnousedate"].Value;
                    }
                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 执行存储过程（发放职员卡）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数集</param>
        /// <param name="out_result">decimal型输出数值</param>
        /// <param name="out_msg">输出信息</param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref decimal cardNo, ref string id, ref DateTime noUsedate, ref decimal out_result, ref string out_msg)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["out_result"].Value != DBNull.Value)
                        out_result = decimal.Parse(cmd.Parameters["out_result"].Value.ToString());
                    if (cmd.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)cmd.Parameters["out_msg"].Value;

                    if (cmd.Parameters["prmcardno"].Value != DBNull.Value)
                    {
                        cardNo = decimal.Parse(cmd.Parameters["prmcardno"].Value.ToString());
                    }
                    if (cmd.Parameters["prmid"].Value != DBNull.Value)
                    {
                        id = (String)cmd.Parameters["prmid"].Value;
                    }
                    if (cmd.Parameters["prmnousedate"].Value != DBNull.Value)
                    {
                        noUsedate = Convert.ToDateTime(cmd.Parameters["prmnousedate"].Value);
                    }
                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 执行存储过程（补办功能卡）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数集</param>
        /// <param name="out_result">decimal型输出数值</param>
        /// <param name="out_msg">输出信息</param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref decimal cardNo, ref string id, ref DateTime noUsedate, ref int cardSn, ref decimal out_result, ref string out_msg)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["out_result"].Value != DBNull.Value)
                        out_result = decimal.Parse(cmd.Parameters["out_result"].Value.ToString());
                    if (cmd.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)cmd.Parameters["out_msg"].Value;

                    if (cmd.Parameters["prmcardno"].Value != DBNull.Value)
                    {
                        cardNo = decimal.Parse(cmd.Parameters["prmcardno"].Value.ToString());
                    }
                    if (cmd.Parameters["prmcardid"].Value != DBNull.Value)
                    {
                        id = (String)cmd.Parameters["prmcardid"].Value;
                    }
                    if (cmd.Parameters["prmnousedate"].Value != DBNull.Value)
                    {
                        noUsedate = Convert.ToDateTime(cmd.Parameters["prmnousedate"].Value);
                    }
                    if (cmd.Parameters["prmcardsn"].Value != DBNull.Value)
                    {
                        cardSn = int.Parse(cmd.Parameters["prmcardsn"].Value.ToString());
                    }
                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 执行存储过程（补办普通乘车卡）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数集</param>
        /// <param name="cardNo">输出卡号</param>
        /// <param name="id">输出发卡流水号</param>
        /// <param name="out_result">decimal型输出数值</param>
        /// <param name="out_msg">输出信息</param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref decimal cardNo, ref string id, ref decimal out_result, ref string out_msg)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["out_result"].Value != DBNull.Value)
                        out_result = decimal.Parse(cmd.Parameters["out_result"].Value.ToString());
                    if (cmd.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)cmd.Parameters["out_msg"].Value;

                    if (cmd.Parameters["prmcardno"].Value != DBNull.Value)
                    {
                        cardNo = decimal.Parse(cmd.Parameters["prmcardno"].Value.ToString());
                    }
                    if (cmd.Parameters["prmid"].Value != DBNull.Value)
                    {
                        id = (String)cmd.Parameters["prmid"].Value;
                    }

                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref string id, ref DateTime noUsedate, ref decimal out_result, ref string out_msg)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["out_result"].Value != DBNull.Value)
                        out_result = decimal.Parse(cmd.Parameters["out_result"].Value.ToString());
                    if (cmd.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)cmd.Parameters["out_msg"].Value;


                    if (cmd.Parameters["prmcardid"].Value != DBNull.Value)
                    {
                        id = (String)cmd.Parameters["prmcardid"].Value;
                    }
                    if (cmd.Parameters["prmcurrenttime"].Value != DBNull.Value)
                    {
                        noUsedate = Convert.ToDateTime(cmd.Parameters["prmcurrenttime"].Value);
                    }
                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }


        /// <summary>
        /// 执行存储过程（一个数值型输出参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数集</param>
        /// <param name="out_result">输出信息</param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref decimal out_result)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["out_result"].Value != DBNull.Value)
                        out_result = decimal.Parse(cmd.Parameters["out_result"].Value.ToString());
                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 执行存储过程（一个字符型输出参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数集</param>
        /// <param name="out_msg">输出信息</param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref string out_msg)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)cmd.Parameters["out_msg"].Value;
                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 执行存储过程（一个decimal型、一个字符型、一个游标输出参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数集</param>
        /// <param name="out_result">decimal型输出数值</param>
        /// <param name="out_msg">输出信息</param>
        /// <param name="io_allrec">游标</param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref decimal out_result, ref string out_msg, ref DataSet io_allrec)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    connection.Open();
                    OracleDataAdapter oracleDA = new OracleDataAdapter();
                    oracleDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                    oracleDA.Fill(dataSet);
                    io_allrec = dataSet;
                    if (oracleDA.SelectCommand.Parameters["out_result"].Value != DBNull.Value)
                        out_result = decimal.Parse(oracleDA.SelectCommand.Parameters["out_result"].Value.ToString());
                    if (oracleDA.SelectCommand.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)oracleDA.SelectCommand.Parameters["out_msg"].Value;
                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 执行存储过程（一个string型、一个int型、一个游标输出参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数集</param>
        /// <param name="out_msg">输出信息</param>
        /// <param name="out_result">输入标记</param>
        /// <param name="io_allrec">游标</param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref string out_msg, ref int out_result, ref DataSet io_allrec)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    connection.Open();
                    OracleDataAdapter oracleDA = new OracleDataAdapter();
                    oracleDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                    oracleDA.Fill(dataSet);
                    io_allrec = dataSet;
                    if (oracleDA.SelectCommand.Parameters["out_result"].Value != DBNull.Value)
                        out_result = int.Parse(oracleDA.SelectCommand.Parameters["out_result"].Value.ToString());
                    if (oracleDA.SelectCommand.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)oracleDA.SelectCommand.Parameters["out_msg"].Value;
                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        /// <summary>
        /// 执行存储过程（一个decimal型、一个字符型、一个游标输出参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数集</param>
        /// <param name="out_back">decimal型输出数值<</param>
        /// <param name="out_result">decimal型输出数值</param>
        /// <param name="out_msg">输出信息</param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref decimal out_back, ref decimal out_result, ref string out_msg)
        {

            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    connection.Open();
                    OracleDataAdapter oracleDA = new OracleDataAdapter();
                    oracleDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                    oracleDA.Fill(dataSet);
                    if (oracleDA.SelectCommand.Parameters["out_back"].Value != DBNull.Value)
                        out_back = decimal.Parse(oracleDA.SelectCommand.Parameters["out_back"].Value.ToString());
                    if (oracleDA.SelectCommand.Parameters["out_result"].Value != DBNull.Value)
                        out_result = decimal.Parse(oracleDA.SelectCommand.Parameters["out_result"].Value.ToString());
                    if (oracleDA.SelectCommand.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)oracleDA.SelectCommand.Parameters["out_msg"].Value;
                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    out_msg = ex.ToString();
                    throw new Exception(ex.Message);
                }
            }

        }
        /// <summary>
        /// 执行存储过程（一个decimal型、一个字符型、一个游标输出参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数集</param>
        /// <param name="out_back">decimal型输出数值<</param>
        /// <param name="out_result">decimal型输出数值</param>
        /// <param name="out_msg">输出信息</param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref decimal out_back, ref decimal out_result, ref string out_msg, ref int opcount)
        {

            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    connection.Open();
                    OracleDataAdapter oracleDA = new OracleDataAdapter();
                    oracleDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                    oracleDA.Fill(dataSet);
                    if (oracleDA.SelectCommand.Parameters["prmopcount"].Value != DBNull.Value)
                        opcount = int.Parse(oracleDA.SelectCommand.Parameters["prmopcount"].Value.ToString());
                    if (oracleDA.SelectCommand.Parameters["out_back"].Value != DBNull.Value)
                        out_back = decimal.Parse(oracleDA.SelectCommand.Parameters["out_back"].Value.ToString());
                    if (oracleDA.SelectCommand.Parameters["out_result"].Value != DBNull.Value)
                        out_result = decimal.Parse(oracleDA.SelectCommand.Parameters["out_result"].Value.ToString());
                    if (oracleDA.SelectCommand.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)oracleDA.SelectCommand.Parameters["out_msg"].Value;
                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    out_msg = ex.ToString();
                    throw new Exception(ex.Message);
                }
            }

        }
        /// <summary>
        /// 执行存储过程（一个int型、一个字符型、一个游标输出参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数集</param>
        /// <param name="out_back">decimal型输出数值</param>
        /// <param name="out_result">decimal型输出数值</param>
        /// <param name="out_msg">输出信息</param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref int out_back, ref int out_result, ref string out_msg)
        {

            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    connection.Open();
                    OracleDataAdapter oracleDA = new OracleDataAdapter();
                    oracleDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                    oracleDA.Fill(dataSet);
                    if (oracleDA.SelectCommand.Parameters["out_back"].Value != DBNull.Value)
                        out_back = int.Parse(oracleDA.SelectCommand.Parameters["out_back"].Value.ToString());
                    if (oracleDA.SelectCommand.Parameters["out_result"].Value != DBNull.Value)
                        out_result = int.Parse(oracleDA.SelectCommand.Parameters["out_result"].Value.ToString());
                    if (oracleDA.SelectCommand.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)oracleDA.SelectCommand.Parameters["out_msg"].Value;
                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    out_msg = ex.ToString();
                    throw new Exception(ex.Message);
                }
            }

        }

        /// <summary>
        /// 执行存储过程（一个int型、一个字符型、一个游标输出参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数集</param>
        /// <param name="out_back">decimal型输出数值<</param>
        /// <param name="out_result">decimal型输出数值</param>
        /// <param name="out_msg">输出信息</param>
        /// <param name="out_cardno">客户卡号</param>
        /// <param name="out_cardsn">持卡序号</param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref decimal out_cardno, ref int out_cardsn, ref int out_back, ref int out_result, ref string out_msg)
        {

            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    connection.Open();
                    OracleDataAdapter oracleDA = new OracleDataAdapter();
                    oracleDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                    oracleDA.Fill(dataSet);
                    if (oracleDA.SelectCommand.Parameters["out_back"].Value != DBNull.Value)
                        out_back = int.Parse(oracleDA.SelectCommand.Parameters["out_back"].Value.ToString());
                    if (oracleDA.SelectCommand.Parameters["out_result"].Value != DBNull.Value)
                        out_result = int.Parse(oracleDA.SelectCommand.Parameters["out_result"].Value.ToString());
                    if (oracleDA.SelectCommand.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)oracleDA.SelectCommand.Parameters["out_msg"].Value;
                    if (oracleDA.SelectCommand.Parameters["prmcardno"].Value != DBNull.Value)
                        out_cardno = decimal.Parse(oracleDA.SelectCommand.Parameters["prmcardno"].Value.ToString());
                    if (oracleDA.SelectCommand.Parameters["out_cardsn"].Value != DBNull.Value)
                        out_cardsn = int.Parse(oracleDA.SelectCommand.Parameters["out_cardsn"].Value.ToString());
                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    out_msg = ex.ToString();
                    throw new Exception(ex.Message);
                }
            }

        }
        /// <summary>
        /// 执行存储过程（一个decimal型、一个游标输出参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数集</param>
        /// <param name="out_result">decimal型输出数值</param>
        /// <param name="io_allrec">游标</param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref decimal out_result, ref DataSet io_allrec)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    connection.Open();
                    OracleDataAdapter oracleDA = new OracleDataAdapter();
                    oracleDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                    oracleDA.Fill(dataSet);
                    io_allrec = dataSet;
                    if (oracleDA.SelectCommand.Parameters["out_result"].Value != DBNull.Value)
                        out_result = decimal.Parse(oracleDA.SelectCommand.Parameters["out_result"].Value.ToString());
                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }



        /// <summary>
        /// 执行存储过程（一个字符型、一个游标输出参数）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数集</param>
        /// <param name="out_msg">输出信息</param>
        /// <param name="io_allrec">游标</param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref string out_msg, ref DataSet io_allrec)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    connection.Open();
                    OracleDataAdapter oracleDA = new OracleDataAdapter();
                    oracleDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                    oracleDA.Fill(dataSet);
                    io_allrec = dataSet;
                    if (oracleDA.SelectCommand.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)oracleDA.SelectCommand.Parameters["out_msg"].Value;
                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    out_msg = ex.ToString();
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        ///  执行存储过程
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="out_outid"></param>
        /// <param name="out_opfare"></param>
        /// <param name="out_bankcardno"></param>
        /// <param name="out_msg"></param>
        /// <param name="out_result"></param>
        /// <param name="out_cardno"></param>
        /// <param name="out_cardsn"></param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref string out_outid, ref decimal out_opfare,
            ref string out_bankcardno, ref string out_msg, ref int out_result, ref string out_cardno, ref int out_cardsn)
        {

            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["out_result"].Value != DBNull.Value)
                        out_result = int.Parse(cmd.Parameters["out_result"].Value.ToString());
                    if (cmd.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)cmd.Parameters["out_msg"].Value;

                    if (cmd.Parameters["prmoutid"].Value != DBNull.Value)
                        out_outid = (String)cmd.Parameters["prmoutid"].Value;
                    if (cmd.Parameters["prmopfare"].Value != DBNull.Value)
                        out_opfare = (Decimal)cmd.Parameters["prmopfare"].Value;
                    if (cmd.Parameters["prmbankcardno"].Value != DBNull.Value)
                        out_bankcardno = (String)cmd.Parameters["prmbankcardno"].Value;
                    if (cmd.Parameters["prmasn"].Value != DBNull.Value)
                        out_cardno = cmd.Parameters["prmasn"].Value.ToString();
                    if (cmd.Parameters["prmcardsn"].Value != DBNull.Value)
                        out_cardsn = int.Parse(cmd.Parameters["prmcardsn"].Value.ToString());
                    connection.Close();

                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }

            }

        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="out_outid"></param>
        /// <param name="out_msg"></param>
        /// <param name="out_result"></param>
        public static void RunProcedure(string storedProcName, OracleParameter[] parameters, ref string out_outid,
             ref string out_msg, ref int out_result)
        {

            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["out_result"].Value != DBNull.Value)
                        out_result = int.Parse(cmd.Parameters["out_result"].Value.ToString());
                    if (cmd.Parameters["out_msg"].Value != DBNull.Value)
                        out_msg = (String)cmd.Parameters["out_msg"].Value;

                    if (cmd.Parameters["prmcustoutid"].Value != DBNull.Value)
                        out_outid = (String)cmd.Parameters["prmcustoutid"].Value;
                    connection.Close();

                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }

            }

        }

        /// <summary>
        /// 获得记录总数（仅在存储过程分页中使用）
        /// </summary>
        /// <param name="sqlCount">查询语句,含排序部分</param>
        /// <returns></returns>
        public static int GetPagedRecordsCount(string sqlCount)
        {
            string storedProcName = "pkg_page.sp_getrecordcount";	//分页存储过程名称
            int recordsCount = 0;
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                OracleParameter[] para = new OracleParameter[2];
                OracleParameter p1 = new OracleParameter();
                p1.Direction = ParameterDirection.Input;
                p1.OracleType = OracleType.VarChar;
                p1.ParameterName = "p_sqlcount";
                p1.Value = sqlCount;
                p1.Size = 1024;

                OracleParameter p2 = new OracleParameter();
                p2.Direction = ParameterDirection.Output;
                p2.OracleType = OracleType.Number;
                p2.ParameterName = "p_outrecordcount";
                //p2.Value = 0;

                para[0] = p1;
                para[1] = p2;

                try
                {
                    connection.Open();
                    OracleCommand cmd = BuildQueryCommand(connection, storedProcName, para);
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["p_outrecordcount"].Value != DBNull.Value)
                    {
                        recordsCount = int.Parse(cmd.Parameters["p_outrecordcount"].Value.ToString());
                    }
                    connection.Close();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
                return recordsCount;
            }
        }

        public static DataSet GetPagedRecords(string sql, int pageSize, int pageNO, ref int allRowCount)
        {
            string storedProcName = "pkg_page.sp_page";	//分页存储过程名称
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                OracleParameter[] para = new OracleParameter[5];
                OracleParameter p1 = new OracleParameter();
                OracleParameter p2 = new OracleParameter();
                OracleParameter p3 = new OracleParameter();
                OracleParameter p4 = new OracleParameter();
                OracleParameter p5 = new OracleParameter();
                p1.Direction = ParameterDirection.Input;
                p1.OracleType = OracleType.Number;
                p1.ParameterName = "p_pagesize";
                p1.Value = pageSize;

                p2.Direction = ParameterDirection.Input;
                p2.OracleType = OracleType.Number;
                p2.ParameterName = "p_pageno";
                p2.Value = pageNO;

                p3.Direction = ParameterDirection.Input;
                p3.OracleType = OracleType.VarChar;
                p3.ParameterName = "p_sqlselect";
                p3.Value = sql;
                p3.Size = 2048;

                p5.Direction = ParameterDirection.Output;
                p5.OracleType = OracleType.Int32;
                p5.ParameterName = "p_outrecordcount";

                p4.Direction = ParameterDirection.Output;
                p4.OracleType = OracleType.Cursor;
                p4.ParameterName = "p_outcursor";

                para[0] = p1;
                para[1] = p2;
                para[2] = p3;
                para[3] = p5;
                para[4] = p4;

                DataSet dstemp = OracleHelper.RunProcedure(storedProcName, para);
                allRowCount = Convert.ToInt32(p5.Value);
                return dstemp;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string sqlString)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OracleDataAdapter command = new OracleDataAdapter(sqlString, connection);
                    command.Fill(ds);
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        /// <summary>
        /// 执行查询语句,返回DataTable
        /// </summary>
        /// <param name="commandType">执行类型</param>
        /// <param name="commandText">语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns></returns>
        public static DataTable Query(CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                DataTable tempTable = new DataTable();
                connection.Open();
                OracleDataAdapter oracleDA = new OracleDataAdapter();
                if (commandType == CommandType.Text)
                {
                    oracleDA.SelectCommand = BuildCommand(connection, commandText, commandParameters);
                }
                else
                {
                    oracleDA.SelectCommand = BuildQueryCommand(connection, commandText, commandParameters);
                }
                oracleDA.Fill(tempTable);
                connection.Close();
                return tempTable;
            }
        }


        /// <summary>
        /// 构建 OracleCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OracleCommand</returns>
        private static OracleCommand BuildQueryCommand(OracleConnection connection, string storedProcName, OracleParameter[] parameters)
        {
            OracleCommand command = new OracleCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (OracleParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }

        /// <summary>
        /// 构建 OracleCommand 对象
        /// </summary>
        /// <param name="connection">OracleConnection对象</param>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="parameters">OracleParameter参数对象</param>
        /// <returns>OracleCommand对象</returns>		
        private static OracleCommand BuildCommand(OracleConnection connection, string sqlStr, OracleParameter[] parameters)
        {
            OracleCommand command = new OracleCommand(sqlStr, connection);
            command.CommandType = CommandType.Text;
            if (parameters != null)
            {
                foreach (OracleParameter parameter in parameters)
                {
                    if (parameter != null)
                    {
                        // 检查未分配值的输出参数,将其分配以DBNull.Value.
                        if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                            (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        command.Parameters.Add(parameter);
                    }
                }
            }
            return command;
        }

        /// <summary>
        /// 通过命令类型、命令和参数组执行费查询操作
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            // Create a new Oracle command
            OracleCommand cmd = new OracleCommand();
            //Create a connection
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                //Prepare the command
                PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);
                //Execute the command
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
    }
}
