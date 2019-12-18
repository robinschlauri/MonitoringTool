using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zbw.Project.Testat.Repository
{
    class LogEntriesRepository : RepositoryBase<LogEntry>
    {
        public override string TableName => "v_logentries";

        public override void Add(LogEntry entity)
        {
            // "CALL `inventarisierung`.`LogMessageAdd`('{0}', '{1}', {2}, '{3}')"
            Add(string.Format("CALL `inventarisierung`.`LogMessageAdd`('{0}', '{1}', {2}, '{3}')", 
                entity.Pod, entity.Hostname, entity.Severity, entity.Message));
            throw new NotImplementedException();
        }

        public override long Count(string whereCondition, Dictionary<string, object> parameterValues)
        {
            throw new NotImplementedException();
        }

        public override void Delete(LogEntry entity)
        {
            throw new NotImplementedException();
        }

        public override List<LogEntry> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            throw new NotImplementedException();
        }

        public  LogEntry readDBRecord(IDataReader reader)
        {
            LogEntry logEntry = new LogEntry
            {

                Id = reader.GetInt32(0),
                Pod = reader.GetString(1),
                Location = reader.GetString(2),
                Hostname = reader.GetString(3),
                Severity = reader.GetInt32(4),
                DateTime = reader.GetDateTime(5),
                Message = reader.GetString(6)
            };
            return logEntry;
    }

        public override List<LogEntry> GetAll()
        {
            using (var _connection = new MySqlConnection(this.connectionString))
            {
                var _logEntries = new List<LogEntry>();
                try
                {
                    _connection.Open();

                    var command = _connection.CreateCommand();
                    command.CommandText = "select * from " + TableName;

                    IDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        _logEntries.Add(readDBRecord(reader));
                    }

                    reader.Close();
                    _connection.Close();
                }

                catch (Exception)
                {
                    _connection.Close();
                    MessageBox.Show(
                        "Der Connection String den Sie eingegeben haben ist falsch!\nVersuchen Sie es erneut!",
                        "Connectionstring Fehlerhaft!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return _logEntries;
            }
        }

        public override LogEntry GetSingle<P>(P pkValue)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<LogEntry> Query(string whereCondition, Dictionary<string, object> parameterValues)
        {
            throw new NotImplementedException();
        }

        public override void Update(LogEntry entity)
        {
            throw new NotImplementedException();
        }

   }

}
