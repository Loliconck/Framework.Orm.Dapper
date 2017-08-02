using Dapper;
using Framework.Orm.Dapper.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Orm.Dapper.Core
{
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : BaseEntity
    {
        private IDbConnection connection;
        private string dbKey = null;
        private string connectionString;

        public BaseRepository()
        {
            dbKey = ConfigurationContainer.ConnectionStringManager.GetDefaultKey();
        }

        private IDbConnection GetConnection()
        {
            connection = new SqlConnection(ConnectionString);

            return connection;
        }

        public string ConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(connectionString))
                {
                    return connectionString;
                }

                connectionString = ConfigurationContainer.ConnectionStringManager[dbKey];
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

        public string DbKey
        {
            get
            {
                return dbKey;
            }
            set
            {
                dbKey = value;
            }
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            using (connection = GetConnection())
            {
                return connection.GetSingle(null, predicate);
            }
        }

        public List<T> GetList(string sql, object param)
        {
            using (connection = GetConnection())
            {
                var result = connection.Query<T>(sql, param);
                return result == null ? null : result.ToList();
            }
        }

        public int GetCount(Expression<Func<T, bool>> predicate)
        {
            using (connection = GetConnection())
            {
                var result = connection.Count<T>(predicate);
                return result;
            }
        }

        public bool GetDelete(Expression<Func<T, bool>> predicate)
        {
            using (connection = GetConnection())
            {
                var result = connection.GetDelete<T>(predicate);
                return result;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
