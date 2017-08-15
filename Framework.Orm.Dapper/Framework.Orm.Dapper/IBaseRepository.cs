using Framework.Orm.Dapper.Domain;
using Framework.Orm.Dapper.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Orm.Dapper.Core
{
    public interface IBaseRepository<T> : IRepository where T : BaseEntity
    {
        string DbKey { set; get; }

        #region Query

        T GetSingle(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetList(string sql, object param);

        /// <summary>
        /// 获取集合列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">查询字段</param>
        /// <param name="topNumber">查询条数</param>
        /// <param name="orderByTypes">排序条件</param>
        /// <returns></returns>
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> keySelector = null, int topNumber = 0,
            IDictionary<string, OrderByTypeEnum> orderByTypes = null);

        /// <summary>
        /// 获取分页信息(默认ID降序)
        /// </summary>
        /// <param name="page">分页参数</param>
        /// <param name="orderByTypes">排序字段</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="selector">查询字段</param>
        /// <returns></returns>
        PagingEntity<T> GetPaging(PageParam page, Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, object>> selector = null, IDictionary<string, OrderByTypeEnum> orderByTypes = null);

        /// <summary>
        /// 查询满足条件的记录的条数
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        int GetCount(Expression<Func<T, bool>> predicate);

        #endregion

        #region Command

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        int Insert(params T[] entitys);

        /// <summary>
        /// 修改（默认根据主键修改）
        /// </summary>
        /// <param name="entity">要修改的实体</param>
        /// <param name="selector">指定修改字段（默认为全部字段）</param>
        /// <param name="predicate">修改条件（值必须包含在TEntity里面）</param>
        /// <returns></returns>
        int Update(T entity, Expression<Func<T, object>> selector = null, Expression<Func<T, bool>> predicate = null);

        /// <summary>
        /// 批量修改（默认根据主键修改）
        /// </summary>
        /// <param name="entitys">需要修改的实体对象集合</param>
        /// <param name="selector">指定修改字段（默认为全部字段）</param>
        /// <param name="predicate">修改条件（值必须包含在TEntity里面）</param>
        /// <returns></returns>
        int Update(IEnumerable<T> entitys, Expression<Func<T, object>> selector = null, Expression<Func<T, bool>> predicate = null);

        /// <summary>
        ///  删除（根据指定条件逻辑删除）
        /// </summary>
        /// <param name="predicate">删除条件</param>
        /// <returns></returns>
        int Delete(Expression<Func<T, bool>> predicate);

        #endregion
    }
}
