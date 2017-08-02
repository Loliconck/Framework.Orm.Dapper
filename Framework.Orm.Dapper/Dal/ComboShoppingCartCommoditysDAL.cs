using Framework.Orm.Dapper.Core;
using Model;

namespace Dal
{
    public class ComboShoppingCartCommoditysDAL : BaseRepository<Cb_ComboShoppingCartCommoditys>
    {
        public ComboShoppingCartCommoditysDAL()
        {
            base.DbKey = "YGOP_Combo";
        }
    }
}
