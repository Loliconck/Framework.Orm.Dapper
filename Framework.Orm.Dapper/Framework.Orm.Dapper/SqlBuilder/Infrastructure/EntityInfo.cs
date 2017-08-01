using System;
using System.Collections.Generic;

namespace Framework.Orm.Dapper.SqlBuilder.Infrastructure
{
    /// <summary>
    /// 转换实体对象描述
    /// </summary>
    public class EntityInfo
    {
        public EntityInfo()
        {
            Properties = new List<PropertyDes>();
            ParamColumns = new Dictionary<SqlTypeEnum, List<ParamColumnModel>>();
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 对象的所有属性,只包含有映射关系的属性
        /// </summary>
        public IList<PropertyDes> Properties { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public Type ClassType { get; set; }

        /// <summary>
        /// 是否需要记录日志
        /// </summary>
        public bool IsLog { get; set; }

        /// <summary>
        /// SQL操作类型与实体与数据库字段对应的字典
        /// </summary>
        public Dictionary<SqlTypeEnum, List<ParamColumnModel>> ParamColumns { get; set; }
    }
}
