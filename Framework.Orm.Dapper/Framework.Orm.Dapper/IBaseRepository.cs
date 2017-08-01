using Framework.Orm.Dapper.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Orm.Dapper.Core
{
    public interface IBaseRepository<T> : IRepository where T : BaseEntity
    {
        string SetConnectionStringKey();

        T GetSingle(Expression<Func<T, bool>> predicate);

        List<T> GetList(string sql, object param);
    }
}
