using Framework.Orm.Dapper.Domain;
using Framework.Orm.Dapper.Domain.Enum;
using Framework.Orm.Dapper.SqlBuilder;
using Framework.Orm.Dapper.SqlBuilder.DataAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using Dapper;

namespace Framework.Orm.Dapper.Core
{
    internal static class DapperExtension
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connection"></param>
        /// <param name="param"></param>
        /// <param name="predicate"></param>
        /// <param name="keySelector"></param>
        /// <param name="topNumber"></param>
        /// <param name="orderByTypes"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static IEnumerable<T> Select<T>(this IDbConnection connection, object param,
            Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> keySelector = null, int topNumber = 0,
            IDictionary<string, OrderByTypeEnum> orderByTypes = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
           where T : BaseEntity
        {
            ISqlAdapter adapter = GetSqlAdapter(connection);

            var sql = adapter.GetSelect(predicate, keySelector, topNumber, orderByTypes);

            if (param == null)
            {
                param = adapter.ParamValues;
            }

            var result = connection.Query<T>(sql, param, transaction, true, commandTimeout);

            return result;
        }

        public static T GetSingle<T>(this IDbConnection connection, object param = null,
            Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> keySelector = null,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : BaseEntity
        {
            ISqlAdapter adapter = GetSqlAdapter(connection);

            var sql = adapter.GetSelect(predicate, keySelector);

            if (param == null)
            {
                param = adapter.ParamValues;
            }

            var result = connection.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout);

            return result;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="param"></param>
        /// <param name="page"></param>
        /// <param name="orderByTypes"></param>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static IEnumerable<T> SelectPage<T>(this IDbConnection connection, object param, PageParam page,
            Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> selector = null, IDictionary<string, OrderByTypeEnum> orderByTypes = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
           where T : BaseEntity
        {
            ISqlAdapter adapter = GetSqlAdapter(connection);

            var sql = adapter.GetPage(page, predicate, selector, orderByTypes);

            if (param == null)
            {
                param = adapter.ParamValues;
            }

            return connection.Query<T>(sql, param, transaction, true, commandTimeout);
        }

        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connection"></param>
        /// <param name="param"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int Count<T>(this IDbConnection connection, object param = null,
            Expression<Func<T, bool>> predicate = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
           where T : BaseEntity
        {
            ISqlAdapter adapter = GetSqlAdapter(connection);

            var sql = adapter.GetCount(predicate);

            if (param == null)
            {
                param = adapter.ParamValues;
            }

            var result = connection.ExecuteScalar<int>(sql, param, transaction, commandTimeout);

            return result;
        }

        /// <summary>
        /// 根据指定字段生成where条件获得Delete语句
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">where条件表达式</param>
        /// <returns></returns>
        public static bool GetDelete<T>(this IDbConnection connection, object param = null, Expression<Func<T, bool>> predicate = null, IDbTransaction transaction = null, int? commandTimeout = null) where T : BaseEntity
        {
            ISqlAdapter adapter = GetSqlAdapter(connection);

            var sql = adapter.GetDelete(predicate);

            if (param == null)
            {
                param = adapter.ParamValues;
            }

            return connection.ExecuteScalar<int>(sql, param, transaction, commandTimeout) > 0;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int Insert<T>(this IDbConnection connection, IEnumerable<T> param, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : BaseEntity
        {
            ISqlAdapter adapter = GetSqlAdapter(connection);

            var sql = adapter.GetInsert<T>();

            return connection.Execute(sql, param, transaction, commandTimeout);
        }

        private static ISqlAdapter GetSqlAdapter(this IDbConnection connection)
        {
            ISqlAdapter adapter = new SqlServerAdapter();
            return adapter;
        }
    }
}
