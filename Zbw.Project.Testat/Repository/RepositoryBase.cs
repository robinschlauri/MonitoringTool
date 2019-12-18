using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zbw.Project.Testat.DataAccess.Repository;

namespace Zbw.Project.Testat.Repository
{
    public abstract class RepositoryBase<M> : IRepositoryBase<M>
    {
        /* protected RepositoryBase()
         {
                 public static string CONNECTION_STRING_1
             {
                 get
                 {
                     return ConfigurationManager.ConnectionStrings["Conn1"].ConnectionString;
                 }
             }

         SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING_1());
         */

        public string connectionString = "Server=localhost;Port=3306;Database=inventarisierung;Uid=root;Pwd=....;";


        public abstract string TableName { get; }

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
        public abstract List<M> GetAll();
        public abstract M GetSingle<P>(P pkValue);
        public abstract IQueryable<M> Query(string whereCondition, Dictionary<string, object> parameterValues);
        public abstract void Update(M entity);
    }
}

