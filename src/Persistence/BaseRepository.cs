using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Persistence
{
    public class BaseRepository
    {
        protected IDbConnection _conn { get; set; }

        public BaseRepository(IDbConnection conn)
        {
            _conn = conn;
            _conn.Open();
        }

        public BaseRepository()
        {
            _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Dapper.Example"].ConnectionString);
            _conn.Open();
        }
    }
}
