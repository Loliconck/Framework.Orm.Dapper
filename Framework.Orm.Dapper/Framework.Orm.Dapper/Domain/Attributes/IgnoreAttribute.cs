using System;

namespace Framework.Orm.Dapper.Domain.Attributes
{
    /// <summary>
    /// 忽略字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class IgnoreAttribute : BaseAttribute
    {
    }
}
