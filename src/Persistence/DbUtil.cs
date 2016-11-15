using System.Data;
using System.Data.SqlClient;

namespace Persistence
{
    public static class DbUtil
    {
        public static SqlDataReader ExecuteReader(string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            using (var context = new DbTransaction.ReaderContext())
            {
                SqlCommand cmd = context.Command;
                cmd.CommandType = commandType;
                cmd.CommandText = sql;
                cmd.CommandTimeout = 0;

                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                QueryObserver.Observer.RaiseDatabaseAccessEvent(cmd);
                return cmd.ExecuteReader(context.Behavior);
            }
        }

        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] parameters)
        {
            return ExecuteReader(sql, CommandType.Text, parameters);
        }

        public static T Fetch<T>(this SqlDataReader rdr, string fname)
        {
            int ordinal = rdr.GetOrdinal(fname);

            if (rdr.IsDBNull(ordinal))
                return default(T);

            return (T)rdr[ordinal];
        }

        public static int ExecuteNonQuery(string sql, CommandType cmdType, params SqlParameter[] parameters)
        {
            int result;

            using (var context = new DbTransaction.CommandContext())
            {
                SqlCommand cmd = context.Command;
                cmd.CommandType = cmdType;
                cmd.CommandTimeout = 0;
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);

                QueryObserver.Observer.RaiseDatabaseAccessEvent(cmd);

                result = cmd.ExecuteNonQuery();
            }

            return result;
        }

        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(sql, CommandType.Text, parameters);
        }
    }
}
