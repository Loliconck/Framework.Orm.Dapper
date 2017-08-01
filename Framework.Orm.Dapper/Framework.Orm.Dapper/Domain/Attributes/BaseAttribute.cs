using Framework.Orm.Dapper.Domain.Enum;
using System;

namespace Framework.Orm.Dapper.Domain.Attributes
{
    public class BaseAttribute : Attribute
    {
        public BaseAttribute()
        {
            this.ColumnType = ColumnTypeEnum.None;
        }

        public BaseAttribute(ColumnTypeEnum columnType)
        {
            this.ColumnType = columnType;
        }

        /// <summary>
        /// 列特殊类型
        /// </summary>
        public ColumnTypeEnum ColumnType { get; set; }
    }
}
