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
            ComboShoppingCartCommoditysDAL dal = new ComboShoppingCartCommoditysDAL();
            string sql = @"SELECT  *
                            FROM    dbo.Cb_ComboShoppingCartCommoditys
                            WHERE   Disabled = 0;";

            Console.WriteLine(dal.ConnectionString);
            var list = dal.GetList(sql, null);
            Console.WriteLine(list == null ? 0 : list.Count);

            Console.ReadKey();
        }

        private static void InitConnectionStrings()
        {
            ConfigurationContainer.Init();
        }
    }
}
