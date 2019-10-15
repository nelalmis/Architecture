using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonMethodsTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DataAccessTest()
        {
            Architecture.DataAccess.Authentication aut = new Architecture.DataAccess.Authentication();
            var response= aut.SelectByColumns("nevzat@firm.com", "123456");
        }
    }
}
