using System.Collections.Generic;

namespace Framework.Orm.Dapper.Domain
{
    /// <summary>
    /// 分页基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingEntity<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Count { get; set; }
    }
}
