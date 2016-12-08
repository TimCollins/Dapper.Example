using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace Persistence
{
    public class BaseRepository
    {
        internal static string ConnectionString = ConfigurationManager.ConnectionStrings["Dapper.Example"].ConnectionString;

        internal static IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                return conn.Query<T>(sql, param);
            }
        }

        internal static int Execute(string sql, object param = null)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                return conn.Execute(sql, param);
            }
        }
    }
}
