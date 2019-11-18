﻿using MySql.Data.MySqlClient;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace Zbw.Project.Testat.Service
{
    public class DbConnection : BindableBase
    {
        private List<LogEntries> _logEntries;
        private IDbConnection _connection;

        public List<LogEntries> GetLogData(string connectionstring, string query)
        {
            _connection = new MySqlConnection(connectionstring);
            try
            {
                _connection.Open();

                var command = _connection.CreateCommand();
                command.CommandText = query;

                IDataReader reader = command.ExecuteReader();
                _logEntries = new List<LogEntries>();

                while (reader.Read())
                {
                    var logEntries = new LogEntries
                    {
                        Id = reader.GetInt32(0),
                        Pod = reader.GetString(1),
                        Location = reader.GetString(2),
                        Hostname = reader.GetString(3),
                        Severity = reader.GetInt32(4),
                        DateTime = reader.GetDateTime(5),
                        Message = reader.GetString(6)
                    };

                    _logEntries.Add(logEntries);
                }

                reader.Close();
                _connection.Close();
            }
            catch (Exception e)
            {
                _connection.Close();
                MessageBox.Show("Der Connection String den Sie eingegeben haben ist falsch!\nVersuchen Sie es erneut!",
                    "Connectionstring Fehlerhaft!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return _logEntries;
        }

        public void ConfirmLogEntry(string connectionstring, string query)
        {
            _connection = new MySqlConnection(connectionstring);
            try
            {
                _connection.Open();

                var command = _connection.CreateCommand();
                command.CommandText = query;
                var result = command.ExecuteNonQuery();

                _connection.Close();
                if (result == 1)
                {
                    MessageBox.Show("Der Eintrag wurde gelöscht", "Erfolgreich ausgeführt");
                }
            }
            catch (Exception e)
            {
                _connection.Close();
                MessageBox.Show("Der Eintrag konnte nicht gelöscht werden", "Kein Eintrag gelöscht",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AddLogEntry(string connectionstring, string query)
        {
            _connection = new MySqlConnection(connectionstring);
            try
            {
                _connection.Open();

                var command = _connection.CreateCommand();
                command.CommandText = query;
                command.ExecuteNonQuery();

                _connection.Close();
                MessageBox.Show("Der Eintrag wurde hinzugefügt ", "Hinzugefügt");
            }
            catch (Exception e)
            {
                _connection.Close();
                MessageBox.Show("Der Eintrag konnte nicht hinzugefügt werden", "Kein Eintrag hinzugefügt",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}