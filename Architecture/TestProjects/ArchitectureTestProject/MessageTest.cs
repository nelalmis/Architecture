using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Architecture.Test.Base
{
    [TestClass]
    public class MessageTest
    {
        [TestMethod]
        public void MessageDictionaryTest()
        {
            var response=Architecture.Messages.MessageDictionary.GetMessage(null);
            Assert.IsNotNull(response);
        }
    }
}
