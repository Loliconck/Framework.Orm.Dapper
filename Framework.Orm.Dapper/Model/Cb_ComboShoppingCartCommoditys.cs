using Framework.Orm.Dapper.Domain;
using Framework.Orm.Dapper.Domain.Attributes;
using Framework.Orm.Dapper.Domain.Enum;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Cb_ComboShoppingCartCommoditys")]
    public class Cb_ComboShoppingCartCommoditys : BaseEntity
    {
        [Description("购物车ID")]
        public Guid ComboShoppingCartId { get; set; }

        [Description("分类ID")]
        public Guid ComboSetCategoryId { get; set; }

        [Description("商品ID")]
        public Guid CommodityId { get; set; }

        [Description("商品类型")]
        public int CategoryType { get; set; }

        /// <summary>
        /// 是否无效
        /// </summary>
        [Base(ColumnTypeEnum.Delete)]
        public virtual int Disabled { get; set; }
    }
}
