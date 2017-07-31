using Framework.Orm.Dapper.Core;
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
    }
}
