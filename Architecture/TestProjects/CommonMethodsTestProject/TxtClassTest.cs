using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Architecture.Helper;

namespace Architecture.Test.Common
{
    [TestClass]
    public class TxtClassTest
    {
        [TestMethod]
        public void ExportToTxtFileTest()
        {
            TxtClass txt = new TxtClass(null,null);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Text dosyası");
            sb.AppendLine("Deneme yazısı");
            txt.ExportToTxtFile(sb);
        }
    }
}
