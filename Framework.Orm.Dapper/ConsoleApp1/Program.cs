using Dal;
using Framework.Orm.Dapper.Core;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            InitConnectionStrings();
            TestGetList();
            TestGetOne();
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
            Console.WriteLine(list == null ? 0 : list.Count);
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

        private static void InitConnectionStrings()
        {
            ConfigurationContainer.Init();
        }
    }
}
