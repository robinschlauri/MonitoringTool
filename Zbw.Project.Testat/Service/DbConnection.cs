using MySql.Data.MySqlClient;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;

namespace Zbw.Project.Testat.Service
{
    public class DbConnection : BindableBase
    {
        private string _connectionstring;
        private List<LogEntries> _logEntries;
        private IDbConnection _connection;

        public DbConnection()
        {

        }

        public List<LogEntries> GetLogData(string connectionstring, string query)
        {
            _connectionstring = connectionstring;
            try
            {
                _connection = new MySqlConnection(_connectionstring);
                _connection.Open();

                var command = _connection.CreateCommand();
                command.CommandText = query;
                IDataReader reader = command.ExecuteReader();
                _logEntries = new List<LogEntries>();
                while (reader.Read())
                {
                    var logEntries = new LogEntries();
                    logEntries.Id = reader.GetInt32(0);
                    logEntries.Pod = reader.GetString(1);
                    logEntries.Location = reader.GetString(2);
                    logEntries.Hostname = reader.GetString(3);
                    logEntries.Severity = reader.GetInt32(4);
                    logEntries.DateTime = reader.GetDateTime(5);
                    logEntries.Message = reader.GetString(6);

                    _logEntries.Add(logEntries);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception e)
            {
                _connection.Close();
            }
            return _logEntries;
        }
    }
}