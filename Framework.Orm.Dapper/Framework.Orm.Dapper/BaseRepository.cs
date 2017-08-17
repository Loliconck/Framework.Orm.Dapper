using Dapper;
using Framework.Orm.Dapper.Domain;
using Framework.Orm.Dapper.Domain.Enum;
using Framework.Orm.Dapper.Domain.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Orm.Dapper.Core
{
    public abstract class BaseRepository : UnitOfWork, IRepository
    {

    }

    public class BaseRepository<TEntity> : BaseRepository, IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
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

        public override string ConnectionString
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

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            using (connection = GetConnection())
            {
                return connection.GetSingle(null, predicate);
            }
        }

        public IEnumerable<TEntity> GetList(string sql, object param)
        {
            using (connection = GetConnection())
            {
                return connection.Query<TEntity>(sql, param);
            }
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> selector = null, int topNumber = 0,
            IDictionary<string, OrderByTypeEnum> orderByTypes = null)
        {
            using (connection = GetConnection())
            {
                return connection.Select<TEntity>(null, predicate, selector, topNumber, orderByTypes);
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
        public PagingEntity<TEntity> GetPaging(PageParam page, Expression<Func<TEntity, bool>> predicate = null,
             Expression<Func<TEntity, object>> selector = null, IDictionary<string, OrderByTypeEnum> orderByTypes = null)
        {
            using (connection = GetConnection())
            {
                var data = connection.SelectPage(null, page, predicate, selector, orderByTypes);
                var result = connection.Count(null, predicate);
                return new PagingEntity<TEntity>()
                {
                    Data = data.ToList(),
                    Count = result
                };
            }
        }

        public int GetCount(Expression<Func<TEntity, bool>> predicate)
        {
            using (connection = GetConnection())
            {
                var result = connection.Count<TEntity>(null, predicate);
                return result;
            }
        }

        public bool GetDelete(Expression<Func<TEntity, bool>> predicate)
        {
            using (connection = GetConnection())
            {
                var result = connection.GetDelete<TEntity>(predicate);
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
        public int Insert(params TEntity[] entitys)
        {
            foreach (var entity in entitys)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity不能为空");
                }
                entity.SetInsertProperty();
            }
            if (entitys.Length > ConfigurationContainer.SingleMaxInsertCount)
            {
                BulkInsert(entitys);
                return entitys.Length;
            }

            if (!base.IsTransaction)
            {
                using (connection = new SqlConnection(ConnectionString))
                {
                    var result = connection.Insert(entitys);
                    return result;
                }
            }
            else
            {
                var result = connection.Insert(entitys, transaction);
                return result;
            }
        }

        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Update(TEntity entity, Expression<Func<TEntity, object>> selector = null, Expression<Func<TEntity, bool>> predicate = null)
        {
            if (!entity.HasValue())
            {
                throw new ArgumentNullException("entity不能为空或者Id不能为空");
            }

            if (!base.IsTransaction)
            {
                using (connection = new SqlConnection(ConnectionString))
                {
                    var result = connection.Update(new List<TEntity> { entity }, selector, predicate);
                    return result;
                }
            }
            else
            {
                var result = connection.Update(new List<TEntity> { entity }, selector, predicate, transaction);
                return result;
            }
        }

        /// <summary>
        /// 批量更新实体集合
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Update(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> selector = null, Expression<Func<TEntity, bool>> predicate = null)
        {
            var enumerable = entities as TEntity[] ?? entities.ToArray();

            foreach (var entity in enumerable)
            {
                if (!entity.HasValue())
                {
                    throw new ArgumentNullException("entity不能为空或者Id不能为空");
                }
            }

            if (!base.IsTransaction)
            {
                using (connection = new SqlConnection(ConnectionString))
                {
                    var result = connection.Update(enumerable, selector, predicate);
                    return result;
                }
            }
            else
            {
                var result = connection.Update(enumerable, selector, predicate, transaction);
                return result;
            }
        }

        /// <summary>
        /// 根据条件删除数据库
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            if (!base.IsTransaction)
            {
                using (connection = new SqlConnection(ConnectionString))
                {
                    var result = connection.Delete<TEntity>(null, predicate);
                    return result;
                }
            }
            else
            {
                var result = connection.Delete<TEntity>(null, predicate, transaction);
                return result;
            }
        }

        /// <summary>
        /// 删除（根据主键逻辑删除）
        /// </summary>
        /// <param name="entitys">实体对象</param>
        /// <returns></returns>
        public int Delete(params TEntity[] entitys)
        {
            if (entitys.Length == 0)
            {
                throw new ArgumentNullException("entitys不能为空或者Id不能为空");
            }

            foreach (var entity in entitys)
            {
                if (!entity.HasValue())
                {
                    throw new ArgumentNullException("entity不能为空或者Id不能为空");
                }
            }

            if (!base.IsTransaction)
            {
                using (connection = new SqlConnection(ConnectionString))
                {
                    return connection.Delete<TEntity>(entitys, null);
                }
            }
            else
            {
                return connection.Delete<TEntity>(entitys, null, transaction);
            }
        }

        /// <summary>
        ///  删除（根据指定条件逻辑删除）
        /// </summary>
        /// <param name="predicate">删除条件</param>
        /// <returns></returns>
        public int Delete(TEntity entitie, Expression<Func<TEntity, bool>> predicate)
        {
            var entities = new List<TEntity>() { entitie };

            if (!base.IsTransaction)
            {
                using (connection = new SqlConnection(ConnectionString))
                {
                    return connection.Delete<TEntity>(entities, predicate);
                }
            }
            else
            {
                return connection.Delete<TEntity>(entities, predicate, transaction);
            }
        }

        /// <summary>
        ///  删除（根据指定条件逻辑删除）
        /// </summary>
        /// <param name="predicate">删除条件</param>
        /// <returns></returns>
        public int Delete(IEnumerable<TEntity> entities, Expression<Func<TEntity, bool>> predicate)
        {
            if (!base.IsTransaction)
            {
                using (connection = new SqlConnection(ConnectionString))
                {
                    return connection.Delete<TEntity>(entities, predicate);
                }
            }
            else
            {
                return connection.Delete<TEntity>(entities, predicate, transaction);
            }
        }

        /// <summary>
        ///  根据主键删除
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns></returns>
        public int Delete(Guid id)
        {
            var entity = new TEntity() { Id = id };
            IEnumerable<TEntity> entities = new List<TEntity>() { entity };

            if (!base.IsTransaction)
            {
                using (connection = new SqlConnection(ConnectionString))
                {

                    return connection.Delete<TEntity>(entities, null);
                }
            }
            else
            {
                return connection.Delete<TEntity>(entities, null, transaction: transaction);
            }
        }

        /// <summary>
        /// 使用SqlBulkCopy批量插入数据
        /// </summary>
        public void BulkInsert(IEnumerable<TEntity> entities)
        {
            var enumerable = entities as TEntity[] ?? entities.ToArray();

            if (entities == null || !enumerable.Any())
            {
                return;
            }

            var request = ToDataTable(enumerable);

            using (var conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();

                //事务锁
                SqlTransaction bulkTrans = conn.BeginTransaction();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.CheckConstraints, bulkTrans)
                {
                    BatchSize = 10000,
                    DestinationTableName = request.Item1
                })
                {
                    var dt = request.Item2;

                    if (dt == null)
                    {
                        return;
                    }
                    try
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                        }

                        bulkCopy.WriteToServer(dt);
                        bulkTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        bulkTrans.Rollback();

                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// 将泛类型集合List类转换成DataTable 并返回表名
        /// </summary>
        /// <param name="entitys">泛类型集合</param>
        /// <returns></returns>
        private static Tuple<string, DataTable> ToDataTable<T>(IEnumerable<T> entitys)
        {
            var entityInfos = EntityInfoManager.EntityInfos;

            //检查实体集合不能为空
            var enumerable = entitys as T[] ?? entitys.ToArray();

            if (entitys == null || !enumerable.Any())
            {
                throw new Exception("需转换的集合为空");
            }
            var entityType = typeof(T);

            if (!entityInfos.ContainsKey(entityType.FullName))
            {
                throw new Exception(string.Format("未找到{0}对应的表名", entityType.FullName));
            }

            var entity2Table = entityInfos[entityType.FullName];

            DataTable dt = new DataTable();

            var propertyDeses = entity2Table.Properties;

            foreach (var p in propertyDeses)
            {
                dt.Columns.Add(p.Column, p.PropertyType);
            }

            foreach (var entity in enumerable)
            {
                object[] entityValues = new object[propertyDeses.Count];

                for (int i = 0; i < entityValues.Length; i++)
                {
                    entityValues[i] = propertyDeses[i].PropertyInfo.GetValue(entity, null);
                }

                dt.Rows.Add(entityValues);
            }

            return new Tuple<string, DataTable>(entity2Table.TableName, dt);
        }

        #endregion

        #region Extentions

        public int Execute(string sql, object param)
        {
            if (!base.IsTransaction)
            {
                using (connection = new SqlConnection(ConnectionString))
                {
                    return connection.ExecuteExt(sql, param, isExecuteSql: true);
                }
            }
            else
            {
                return connection.ExecuteExt(sql, param, transaction, isExecuteSql: true);
            }
        }

        public T ExecuteScalar<T>(string sql, object param)
        {
            using (connection = GetConnection())
            {
                return connection.ExecuteScalarExt<T>(sql, param);
            }
        }

        public IEnumerable<TEntity> Query(string sql, object param)
        {
            using (connection = GetConnection())
            {
                var result = connection.QueryExt<TEntity>(sql, param);
                return result;
            }
        }

        public IEnumerable<T> Query<T>(string sql, object param)
        {
            using (connection = GetConnection())
            {
                var result = connection.QueryExt<T>(sql, param);
                return result;
            }
        }

        #endregion
    }
}
