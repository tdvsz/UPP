using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace ConsoleApp5
{
    public enum ConnectionType
    {
        Sql,
        OleDb,
        Odbc
    }

    public class MyDataBase
    {
        private string ConnectionString;
        private ConnectionType ConnectionType;

        private SqlConnection sqlConnection;
        private OleDbConnection oleDbConnection;
        private OdbcConnection odbcConnection;

        public MyDataBase(string connectionString, ConnectionType connectionType)
        {
            ConnectionString = connectionString;
            ConnectionType = connectionType;
        }

        public void Connect()
        {
            switch (ConnectionType)
            {
                case ConnectionType.Sql:
                    sqlConnection = new SqlConnection(ConnectionString);
                    sqlConnection.Open();
                    break;
                case ConnectionType.OleDb:
                    oleDbConnection = new OleDbConnection(ConnectionString);
                    oleDbConnection.Open();
                    break;
                case ConnectionType.Odbc:
                    odbcConnection = new OdbcConnection(ConnectionString);
                    odbcConnection.Open();
                    break;
            }
        }

        public void Disconnect()
        {
            switch (ConnectionType)
            {
                case ConnectionType.Sql:
                    sqlConnection.Close();
                    break;
                case ConnectionType.OleDb:
                    oleDbConnection.Close();
                    break;
                case ConnectionType.Odbc:
                    odbcConnection.Close();
                    break;
            }
        }

        public void Insert(string query)
        {
            switch (ConnectionType)
            {
                case ConnectionType.Sql:
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        command.ExecuteNonQuery();
                    }
                    break;
                case ConnectionType.OleDb:
                    using (OleDbCommand command = new OleDbCommand(query, oleDbConnection))
                    {
                        command.ExecuteNonQuery();
                    }
                    break;
                case ConnectionType.Odbc:
                    using (OdbcCommand command = new OdbcCommand(query, odbcConnection))
                    {
                        command.ExecuteNonQuery();
                    }
                    break;
            }
        }

        public DataTable Select(string query)
        {
            switch (ConnectionType)
            {
                case ConnectionType.Sql:
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                case ConnectionType.OleDb:
                    using (OleDbCommand command = new OleDbCommand(query, oleDbConnection))
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                case ConnectionType.Odbc:
                    using (OdbcCommand command = new OdbcCommand(query, odbcConnection))
                    using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                default:
                    throw new InvalidOperationException("Неизвестный тип подключения.");
            }
        }
    }
}
