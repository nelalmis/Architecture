using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Resources;
using System.Windows.Media.Imaging;

namespace ArchitectureTestProject
{
    [TestClass]
    public class ViewResourceTest
    {
        [TestMethod]
        public void GetResourceImageTest()
        {
           var image= ((System.Drawing.Image)(Architecture.View.Resource.Properties.Resources.ResourceManager.GetObject("btn_Add_24")));
            
        }
    }
}
