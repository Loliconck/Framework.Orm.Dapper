using Framework.Orm.Dapper.Domain;
using Framework.Orm.Dapper.Domain.Enum;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Orm.Dapper.SqlBuilder
{
    /// <summary>
    /// SQL生成器接口
    /// </summary>
    internal interface ISqlAdapter
    {
        /// <summary>
        /// 参数对象
        /// </summary>
        ConcurrentDictionary<string, object> ParamValues { get; set; }

        /// <summary>
        /// 生成查询SQL语句
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">where条件表达式</param>
        /// <param name="selector">查询字段表达式</param>
        /// <param name="topNum">指定条数</param>
        /// <param name="ordeBy">排序类型字典，未指定的将默认降序</param>
        /// <returns>查询的SQL语句</returns>
        string GetSelect<T>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> selector = null, int topNum = 0, IDictionary<string, OrderByTypeEnum> ordeBy = null) where T : BaseEntity;

        /// <summary>
        /// 获取 Count 查询语句
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="predicate">where条件表达式</param>
        /// <returns></returns>
        string GetCount<T>(Expression<Func<T, bool>> predicate = null) where T : BaseEntity;
    }
}
