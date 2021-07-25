using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace DataAccess
{
    internal class BaseRepository
    {
        //internal static string ConnectionString = "Data Source=DESKTOP-D4P4L24;Initial Catalog=Chinook;Integrated Security=true;";
        // TODO: Set this via configuration
        internal static string ConnectionString = string.Empty;

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

        internal static T ExecuteScalar<T>(string sql, object param = null)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                return conn.ExecuteScalar<T>(sql, param);
            }
        }

        internal static T QuerySingle<T>(string sql, object param = null)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                return conn.QuerySingle<T>(sql, param);
            }
        }
    }
}
