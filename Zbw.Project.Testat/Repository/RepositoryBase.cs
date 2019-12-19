using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zbw.Project.Testat.DataAccess.Repository;

namespace Zbw.Project.Testat.Repository
{
    public abstract class RepositoryBase<M> : IRepositoryBase<M>
    {

        public string connectionString = "Server=localhost;Port=3306;Database=inventarisierung;Uid=root;Pwd=maragia88;";
        private IDbConnection _connection;

        public abstract string TableName { get; }

        public abstract List<M> GetAll();

        public long Count()
        {
            using (var conn = new MySqlConnection(this.connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = $"select count(*) from {this.TableName}";
                    return (long)cmd.ExecuteScalar();
                }
            }
        }

        public void Add(String query)
        {
            {
                _connection = new MySqlConnection(this.connectionString);
                try
                {
                    _connection.Open();

                    var command = _connection.CreateCommand();
                    command.CommandText = query;
                    command.ExecuteNonQuery();

                    _connection.Close();
                    MessageBox.Show("Der Eintrag wurde hinzugefügt ", "Hinzugefügt");
                }
                catch (Exception)
                {
                    _connection.Close();
                    MessageBox.Show("Überprüfen Sie die Eingabe", "Kein Eintrag hinzugefügt",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public abstract void Add(M entity);
        public abstract long Count(string whereCondition, Dictionary<string, object> parameterValues);
        public abstract void Delete(M entity);
        public abstract List<M> GetAll(string whereCondition, Dictionary<string, object> parameterValues);

        public abstract M GetSingle<P>(P pkValue);
        public abstract IQueryable<M> Query(string whereCondition, Dictionary<string, object> parameterValues);
        public abstract void Update(M entity);
    }
}

