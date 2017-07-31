using System.Collections.Generic;

namespace Framework.Orm.Dapper.Core
{
    public interface IBaseRepository<T> : IRepository where T : BaseEntity
    {
        string SetConnectionStringKey();

        List<T> GetList(string sql, object param);
    }
}
