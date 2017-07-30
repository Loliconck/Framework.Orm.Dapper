namespace Framework.Orm.Dapper.Core
{
    public interface IBaseRepository<T> : IRepository where T : BaseEntity
    {
        string ConnectionStringKey
        {
            set;
        }
    }
}
