using System.Data.SqlClient;

namespace Persistence
{
    public class QueryObserver
    {
        private static QueryObserver _observer;

        private QueryObserver()
        {
        }

        public static QueryObserver Observer
        {
            get
            {
                if (_observer == null)
                    _observer = new QueryObserver();

                return _observer;
            }
        }

        public delegate void DatabaseAccessDelegate(SqlCommand cmd);
        public event DatabaseAccessDelegate DatabaseAccessEvent;

        public void RaiseDatabaseAccessEvent(SqlCommand cmd)
        {
            if (DatabaseAccessEvent != null)
                DatabaseAccessEvent(cmd);
        }

    }
}
