using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Framework.Orm.Dapper.Core
{
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : BaseEntity
    {
        private IDbConnection connection;
        private string connectionString;
        private string connectionStringKey;

        private IDbConnection GetConnection()
        {
            connection = new SqlConnection(ConnectionString);

            return connection;
        }

        public List<T> GetList(string sql, object param)
        {
            using (connection = GetConnection())
            {
                var result = connection.Query<T>(sql, param);
                return result == null ? null : result.ToList();
            }
        }

        public virtual string ConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(connectionString))
                {
                    return connectionString;
                }

                connectionStringKey = SetConnectionStringKey();
                connectionString = ConfigurationContainer.ConnectionStringManager[connectionStringKey];
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

        public virtual string SetConnectionStringKey()
        {
            return ConfigurationContainer.ConnectionStringManager.GetDefaultKey();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
