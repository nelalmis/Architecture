using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Architecture.Helper;

namespace CommonMethodsTestProject
{
    [TestClass]
    public class DllCollectTest
    {
        [TestMethod]
        public void DllCollectTestMethod()
        {
            DllCollect.DllCopy();
        }
    }
}
