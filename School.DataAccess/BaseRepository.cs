namespace School.DataAccess
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;

    public abstract class BaseRepository : IDisposable
    {
        private bool _isDisposed = false;

        protected readonly SqlConnection Connection;

        public BaseRepository(string connectionString = null) => Connection = new SqlConnection(connectionString ?? ConfigurationManager.ConnectionStrings["SchoolDb"].ConnectionString);

        public void Dispose()
        {
            if (_isDisposed) return;

            Dispose(true);
        }

        protected virtual void Dispose(bool dispose)
        {
            Connection.Dispose();

            _isDisposed = true;
            GC.SuppressFinalize(this);
        }

        ~BaseRepository()
        {
            Dispose(false);
        }
    }
}
