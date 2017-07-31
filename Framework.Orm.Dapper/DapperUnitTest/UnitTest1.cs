using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dal;
using Model;

namespace DapperUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ComboShoppingCartCommoditysDAL dal = new ComboShoppingCartCommoditysDAL();
            string sql = @"SELECT  *
                            FROM    dbo.Cb_ComboShoppingCartCommoditys
                            WHERE   Disabled = 0;";
            var list = dal.GetList(sql, null);
            
        }
    }
}
