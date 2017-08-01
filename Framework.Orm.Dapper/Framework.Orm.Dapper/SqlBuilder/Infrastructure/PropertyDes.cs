using Framework.Orm.Dapper.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Framework.Orm.Dapper.SqlBuilder.Infrastructure
{
    /// <summary>
    /// 转换实体属性描述,只包含有映射关系的属性
    /// </summary>
    public class PropertyDes
    {
        public PropertyDes()
        {
            CusAttribute = new List<BaseAttribute>();
        }

        /// <summary>
        /// 表列名
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// 属性字段名
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public Type PropertyType { get; set; }

        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }

        /// <summary>
        /// 映射特性
        /// </summary>
        public IEnumerable<BaseAttribute> CusAttribute { get; set; }
    }
}
