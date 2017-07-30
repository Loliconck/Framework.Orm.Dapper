using System;
using System.Data;
using System.Data.SqlClient;

namespace Framework.Orm.Dapper.Core
{
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : BaseEntity
    {
        private IDbConnection connection;
        private string connectionString;

        public IDbConnection GetConnection()
        {
            connection = new SqlConnection(ConnectionString);

            return connection;
        }

        public string ConnectionString
        {
            get
            {
                ConfigurationContainer.GetConnectionString()
                if (!string.IsNullOrEmpty(connectionString))
                {
                    return connectionString;
                }

                connectionString = ConfigurationContainer.ConnectionString;
                if (!string.IsNullOrEmpty(connectionString))
                {
                    return connectionString;
                }
                throw new ArgumentNullException("ConnectionString不能为空");
            }
            set
            {
                connectionString = value;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
