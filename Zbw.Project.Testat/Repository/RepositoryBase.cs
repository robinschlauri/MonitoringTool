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
    public abstract class RepositoryBase<M> //: IRepositoryBase<M>
    {
        protected RepositoryBase()
        {
                public static string CONNECTION_STRING_1
            {
                get
                {
                    return ConfigurationManager.ConnectionStrings["Conn1"].ConnectionString;
                }
            }

        SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING_1);


    }
}

