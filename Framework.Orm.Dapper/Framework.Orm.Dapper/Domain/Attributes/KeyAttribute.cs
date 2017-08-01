using System;

namespace Framework.Orm.Dapper.Domain.Attributes
{
    /// <summary>
    /// 主键
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class KeyAttribute : BaseAttribute
    {
        /// <summary>
        /// 是否为自增主键
        /// </summary>
        public bool IsIdentity { get; set; }

        public KeyAttribute()
        {
            this.IsIdentity = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isIdentity">是否为自增主键</param>
        public KeyAttribute(bool isIdentity = false)
        {
            this.IsIdentity = isIdentity;
        }
    }
}
