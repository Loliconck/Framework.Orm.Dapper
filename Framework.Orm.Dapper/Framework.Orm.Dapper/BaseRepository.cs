using Dapper;
using Framework.Orm.Dapper.Domain;
using Framework.Orm.Dapper.Domain.Enum;
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

        #region Query

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            using (connection = GetConnection())
            {
                return connection.GetSingle(null, predicate);
            }
        }

        public IEnumerable<T> GetList(string sql, object param)
        {
            using (connection = GetConnection())
            {
                return connection.Query<T>(sql, param);
            }
        }

        public IEnumerable<T> GetList(Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> selector = null, int topNumber = 0,
            IDictionary<string, OrderByTypeEnum> orderByTypes = null)
        {
            using (connection = GetConnection())
            {
                return connection.Select<T>(null, predicate, selector, topNumber, orderByTypes);
            }
        }

        /// <summary>
        /// 获取分页信息(默认ID降序)
        /// </summary>
        /// <param name="page">分页参数</param>
        /// <param name="orderByTypes">排序字段</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="selector">查询字段</param>
        /// <returns></returns>
        public PagingEntity<T> GetPaging(PageParam page, Expression<Func<T, bool>> predicate = null,
             Expression<Func<T, object>> selector = null, IDictionary<string, OrderByTypeEnum> orderByTypes = null)
        {
            using (connection = GetConnection())
            {
                var data = connection.SelectPage(null, page, predicate, selector, orderByTypes);
                var result = connection.Count(null, predicate);
                return new PagingEntity<T>()
                {
                    Data = data.ToList(),
                    Count = result
                };
            }
        }

        public int GetCount(Expression<Func<T, bool>> predicate)
        {
            using (connection = GetConnection())
            {
                var result = connection.Count<T>(null, predicate);
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

        #endregion

        #region

        /// <summary>
        /// 支持事物的Insert操作
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public int Insert(params T[] entitys)
        {
            foreach (var entity in entitys)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity不能为空");
                }
                entity.SetInsertProperty();
            }
            //if (entitys.Length > Configuration.SingleMaxInsertCount)
            //{
            //    BulkInsert(entitys);
            //    return entitys.Length;
            //}

            //if (!base.IsTransaction)
            //{
            using (connection = new SqlConnection(ConnectionString))
            {
                var result = connection.Insert(entitys);
                return result;
            }
            //}
            //else
            //{
            //    var result = connection.Insert(entitys, transaction);
            //    return result;
            //}
        }

        #endregion

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
