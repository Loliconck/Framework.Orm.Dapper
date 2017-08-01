using Framework.Orm.Dapper.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Framework.Orm.Dapper.Domain
{
    public class BaseEntity
    {
        [Description("主键")]
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 获取默认排序字段
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, OrderByTypeEnum> GetDefaultOrderBy()
        {
            return new Dictionary<string, OrderByTypeEnum> { { "Id", OrderByTypeEnum.Desc } };
        }
    }
}
