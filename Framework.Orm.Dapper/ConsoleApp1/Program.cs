using Dal;
using Framework.Orm.Dapper.Core;
using Framework.Orm.Dapper.Domain;
using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            InitConnectionStrings();
            //TestGetList();
            //TestGetOne();
            //TestCount();
            //TestGetList2();
            //TestPaging();
            TestDelete();
            Console.ReadKey();
        }

        public static void TestGetList()
        {
            ComboShoppingCartCommoditysDAL dal = new ComboShoppingCartCommoditysDAL();
            string sql = @"SELECT  *
                            FROM    dbo.Cb_ComboShoppingCartCommoditys
                            WHERE   Disabled = 0;";

            Console.WriteLine(dal.ConnectionString);
            var list = dal.GetList(sql, null);
            Console.WriteLine(list == null ? 0 : list.Count());
        }

        public static void TestGetList2()
        {
            ComboShoppingCartCommoditysDAL dal = new ComboShoppingCartCommoditysDAL();
            var list = dal.GetList(t => t.ComboSetCategoryId == Guid.Parse("7DE97E1B-161E-43A2-9AD7-F23EC67EEEA7"), t => new { t.Id, t.ComboSetCategoryId, t.ComboShoppingCartId });
            Console.WriteLine(list != null ? list.Count() : 0);
        }

        public static void TestPaging()
        {
            ComboShoppingCartCommoditysDAL dal = new ComboShoppingCartCommoditysDAL();
            PageParam param = new PageParam()
            {
                IsDesc = true,
                PageIndex = 1,
                PageSize = 20
            };
            var data = dal.GetPaging(param, t => t.ComboSetCategoryId == Guid.Parse("7DE97E1B-161E-43A2-9AD7-F23EC67EEEA7"), t => new { t.Id, t.ComboSetCategoryId, t.ComboShoppingCartId });
            Console.WriteLine(data.Count);
            Console.WriteLine(data.Data.Count());
        }

        public static void TestGetOne()
        {
            ComboShoppingCartCommoditysDAL dal = new ComboShoppingCartCommoditysDAL();
            string sql = @"SELECT  *
                            FROM    dbo.Cb_ComboShoppingCartCommoditys
                            WHERE   Disabled = 0 and Id=@Id;";

            Console.WriteLine(dal.ConnectionString);
            var one = dal.GetSingle(t => t.Id == Guid.Parse("26D8485B-0850-4D10-8A0A-0C1F43E47280"));
            Console.WriteLine(one == null ? "0" : one.CommodityId.ToString());
        }

        public static void TestCount()
        {
            ComboShoppingCartCommoditysDAL dal = new ComboShoppingCartCommoditysDAL();
            var one = dal.GetCount(t => t.ComboShoppingCartId == Guid.Parse("A06C6359-EF1E-4A2B-BFEC-D52378A7667F"));
            Console.WriteLine(one);
        }

        public static void TestDelete()
        {
            ComboShoppingCartCommoditysDAL dal = new ComboShoppingCartCommoditysDAL();
            var one = dal.GetDelete(t => t.Id == Guid.Parse("26D8485B-0850-4D10-8A0A-0C1F43E47280"));
            Console.WriteLine(one);
        }

        private static void InitConnectionStrings()
        {
            ConfigurationContainer.Init();
        }
    }
}
