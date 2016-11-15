using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Persistence
{
    internal class DbTransaction : IDisposable
    {
        [ThreadStatic]
        internal static DbTransaction CurrentTransaction;

        private readonly SqlTransaction _transaction;
        private bool _complete;

        public DbTransaction()
        {
            if (CurrentTransaction != null)
                throw new ApplicationException("There is another transaction active on this thread!");

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Dapper.Example"].ConnectionString);
            conn.Open();
            _transaction = conn.BeginTransaction();

            CurrentTransaction = this;
        }

        public void Commit()
        {
            if (_complete)
                throw new ApplicationException("This transaction has already completed!");

            SqlConnection conn = _transaction.Connection;
            _transaction.Commit();
            Cleanup(conn);
        }

        public void Rollback()
        {
            if (_complete)
                throw new ApplicationException("This transaction has already completed!");

            SqlConnection conn = _transaction.Connection;

            _transaction.Rollback();

            Cleanup(conn);
        }

        public void Dispose()
        {
            if (!_complete)
                Rollback();
        }

        private void Cleanup(SqlConnection conn)
        {
            CurrentTransaction = null;

            conn.Close();
            conn.Dispose();

            _complete = true;
        }

        internal class CommandContext : IDisposable
        {
            internal SqlCommand Command { get; }
            private SqlConnection Connection { get; }

            internal CommandContext()
            {
                if (CurrentTransaction == null)
                {
                    Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Dapper.Example"].ConnectionString);
                    Connection.Open();
                    Command = Connection.CreateCommand();
                }
                else
                {
                    Connection = CurrentTransaction._transaction.Connection;
                    Command = Connection.CreateCommand();
                    Command.Transaction = CurrentTransaction._transaction;
                }
            }

            public virtual void Dispose()
            {
                Command.Dispose();

                if (CurrentTransaction == null)
                    Connection.Close();
            }
        }

        internal class ReaderContext : CommandContext
        {
            internal CommandBehavior Behavior
            {
                get
                {
                    return CurrentTransaction == null ? CommandBehavior.CloseConnection : CommandBehavior.Default;
                }
            }

            public override void Dispose()
            {
            }
        }

        public static SqlBulkCopy CreateBulkCopy()
        {
            if (CurrentTransaction != null)
            {
                return new SqlBulkCopy(CurrentTransaction._transaction.Connection, SqlBulkCopyOptions.Default, CurrentTransaction._transaction);
            }

            return new SqlBulkCopy(ConfigurationManager.ConnectionStrings["Dapper.Example"].ConnectionString);
        }
    }
}
