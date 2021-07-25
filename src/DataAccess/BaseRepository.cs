using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class BaseRepository
    {
        public BaseRepository()
        {
            var config = GetConfig();
            var env = config.GetSection("Environment");
            ConnectionString = config.GetConnectionString(env.Value);
        }

        private static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", true, true);

            return builder.Build();
        }

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
