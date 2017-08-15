using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Orm.Dapper.Domain.Extension
{
    internal static class EntityExtensions
    {
        internal static bool HasValue(this BaseEntity entity)
        {
            return entity != null && entity.Id != Guid.Empty;
        }

        internal static bool HasValue(this List<BaseEntity> entitys)
        {
            return entitys != null && entitys.Any(entity => entity.Id != Guid.Empty);
        }
    }
}
