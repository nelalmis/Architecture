using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Architecture.Test.Root.BusinessHelper
{
    [TestClass]
    public class BusinessHelperTest
    {
        [TestMethod]
        public void GetParameterTest()
        {
            var response=Architecture.View.Root.BusinessHelper.BusinessHelper.GetParameter(null);
           
            Assert.AreEqual(0, response.Results.Count);
           
        }
        [TestMethod]
        public void GetResourceTest()
        {
            var response = Architecture.View.Root.BusinessHelper.BusinessHelper.GetResource("nevzat@firm.com", "123456", null,null);

            Assert.AreEqual(0, response.Results.Count);
        }
        [TestMethod]
        public void GetCompanyTest()
        {
            Architecture.DataAccess.Root.BusinessHelper.Company co = new DataAccess.Root.BusinessHelper.Company();
            var response = co.SelectByColumns("elalmis.ne@gmail.com", "123456");
            Assert.AreEqual(0, response.Results.Count);

        }
        [TestMethod]
        public void GetAllCountryTest()
        {
            var response = Architecture.View.Root.BusinessHelper.BusinessHelper.GetAllCountry();

            Assert.AreEqual(0, response.Results.Count);
        }
        [TestMethod]
        public void GetAllActionTest()
        {
            var response = Architecture.View.Root.BusinessHelper.BusinessHelper.GetAction();

            Assert.AreEqual(0, response.Results.Count);
        }
    }
}
