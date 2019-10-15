using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Architecture.Helper;

namespace Architecture.Test.Common
{
    [TestClass]
    public class ExcelClassTest
    {
        [TestMethod]
        public void ExportExcelMethodTest()
        {
            List<Araba> dataList = new List<Araba>();

            dataList.Add(new Araba
            {
                Marka = "Fiat",
                Model = "Punto Evo",
                Renk = "Siyah"
            });

            dataList.Add(new Araba
            {
                Marka = "Opel",
                Model = "Corsa",
                Renk = "Kırmızı"
            });

            ExcelClass<Araba> y = new ExcelClass<Araba>(dataList);
            y.GenerateReport();


        }
        public class Araba
        {
            public string Marka { get; set; }
            public string Model { get; set; }
            public string Renk { get; set; }
        }

    }
}
