using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Architecture.Test.View
{
    [TestClass]
    public class WinTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Architecture.View.Win.ResourceTree tree = new Architecture.View.Win.ResourceTree("nevzat@firm.com", "123456");
            Assert.IsNotNull(tree);
        }
    }
}
