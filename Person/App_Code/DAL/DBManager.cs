using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DBManager
/// </summary>
public class DBManager
{
        public static SqlConnection _connection;
        public static SqlCommand _command;
        public static SqlDataReader _reader;
        public static SqlDataAdapter _dataAdapter;
        public List<SqlParameter> Parameters = new List<SqlParameter>();
        public static string _connectionString = "DefaultConnectionString";
        SqlTransaction _sqlTransaction;
        public static string ConnectionString_Using;
        public DBManager()
        {
            //
            // TODO: Add constructor logic here
            //
            ConnectionString_Using = ConfigurationManager.ConnectionStrings[_connectionString].ConnectionString;
        }

        public DBManager(SqlTransaction sqlTransaction = null)
        {
            _sqlTransaction = sqlTransaction;
            ConnectionString_Using = ConfigurationManager.ConnectionStrings[_connectionString].ConnectionString;
        }

        public DBManager(string connectionStringName, SqlTransaction sqlTransaction = null)
        {
            _connectionString = connectionStringName;
            _sqlTransaction = sqlTransaction;
            ConnectionString_Using = ConfigurationManager.ConnectionStrings[_connectionString].ConnectionString;
        }


        public void AddParameter(string parameterName, object value, SqlDbType sqlDbType, int size)
        {
            SqlParameter parameter = new SqlParameter(parameterName, sqlDbType, size);
            parameter.Value = value;

            Parameters.Add(parameter);
        }

        public void AddParameter(string parameterName, object value, SqlDbType sqlDbType, int size, ParameterDirection parameterDirection)
        {
            SqlParameter parameter = new SqlParameter(parameterName, sqlDbType, size);
            parameter.Value = value;
            parameter.Direction = parameterDirection;
            Parameters.Add(parameter);
        }

        public void AddParameter(string parameterName, object value)
        {
            SqlParameter parameter = new SqlParameter(parameterName, value);
            Parameters.Add(parameter);
        }


        public int ExecuteNonQuery(string procedureName)
        {
            int result = 0;

            try
            {

                using (_connection = new SqlConnection(ConnectionString_Using))
                {
                    _connection.Open();
                    using (_command = new SqlCommand(procedureName, _connection))
                    {
                        if (Parameters.Count != 0)
                        {
                            for (int i = 0; i < Parameters.Count; i++)
                            {
                                _command.Parameters.Add(Parameters[i]);
                            }

                        }
                        _command.CommandType = CommandType.StoredProcedure;
                        result = _command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }


            return result;
        }

        public SqlDataReader ExecuteReader(string procedureName)
        {
            SqlDataReader reader;
            try
            {
                using (_connection = new SqlConnection(ConnectionString_Using))
                {
                    _connection.Open();
                    using (_command = new SqlCommand(procedureName, _connection))
                    {
                        if (Parameters.Count != 0)
                        {
                            for (int i = 0; i < Parameters.Count; i++)
                            {
                                _command.Parameters.Add(Parameters[i]);
                            }

                        }
                        _command.CommandType = CommandType.StoredProcedure;
                        reader = _command.ExecuteReader(CommandBehavior.CloseConnection);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return reader;
        }


        public DataSet ExecuteDataSet(string procedureName)
        {
            DataSet dataSet = new DataSet();

            try
            {
                using (_connection = new SqlConnection(ConnectionString_Using))
                {
                    _connection.Open();
                    using (_command = new SqlCommand(procedureName, _connection))
                    {
                        if (Parameters.Count != 0)
                        {
                            for (int i = 0; i < Parameters.Count; i++)
                            {
                                _command.Parameters.Add(Parameters[i]);
                            }

                        }
                        _command.CommandType = CommandType.StoredProcedure;

                        using (_dataAdapter = new SqlDataAdapter(_command))
                        {
                            _dataAdapter.Fill(dataSet);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return dataSet;
        }

        public DataTable ExecuteDataTable(string procedureName)
        {


            DataTable dataTable = new DataTable();

            try
            {
                using (_connection = new SqlConnection(ConnectionString_Using))
                {
                    _connection.Open();
                    using (_command = new SqlCommand(procedureName, _connection))
                    {
                        if (Parameters.Count != 0)
                        {
                            for (int i = 0; i < Parameters.Count; i++)
                            {


                                _command.Parameters.Add(Parameters[i]);

                            }

                        }
                        _command.CommandType = CommandType.StoredProcedure;
                        using (_dataAdapter = new SqlDataAdapter(_command))
                        {
                            _dataAdapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return dataTable;
        }


        public string ExecuteScalar(string procedureName)
        {
            string result = "";

            try
            {
                using (_connection = new SqlConnection(ConnectionString_Using))
                {
                    _connection.Open();
                    using (_command = new SqlCommand(procedureName, _connection))
                    {
                        if (Parameters.Count != 0)
                        {
                            for (int i = 0; i < Parameters.Count; i++)
                            {
                                _command.Parameters.Add(Parameters[i]);
                            }

                        }
                        _command.CommandType = CommandType.StoredProcedure;
                        result = _command.ExecuteScalar().ToString();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
}