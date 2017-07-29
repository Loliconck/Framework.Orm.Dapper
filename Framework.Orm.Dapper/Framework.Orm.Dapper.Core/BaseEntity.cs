using System;
using System.ComponentModel;

namespace Framework.Orm.Dapper.Core
{
    public class BaseEntity
    {
        [Description("主键")]
        public virtual Guid Id { get; set; }
    }
}
