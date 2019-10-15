using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Architecture.Common.Logger;

namespace Architecture.Test.Base
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void Logger()
        {
            LogManager.Log("bu da yeni mesaj",new DllNotFoundException("dll bulunmadı."));
            /*
            ExceptionLogger log = new ExceptionLogger();

            log.AddLogger(new TextFileLogger());
            log.LogException(new NullReferenceException("asdsdasdasda null olamaz."));
            */
        }
    }
}
