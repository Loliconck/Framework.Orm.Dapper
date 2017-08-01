using System;

namespace Framework.Orm.Dapper.Domain.Attributes
{
    /// <summary>
    /// 自增长列
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class IdentityAttribute : BaseAttribute
    {
        public IdentityAttribute()
        {
        }
    }
}
