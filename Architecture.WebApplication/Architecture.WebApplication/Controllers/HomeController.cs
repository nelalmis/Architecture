using Architecture.View.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Architecture.View.Root.BusinessHelper;
using Architecture.Common.Types;
using System.Web.Helpers;

namespace Architecture.WebApplication.Controllers
{
    public class HomeController : BaseController
    {

        #region SaveCommand

        private bool CanSaveExecute()
        {
            return true;

        }
        public dynamic SaveExecute()
        {
            var a = this;
            return RedirectToAction("Index");
        }

        #endregion SaveCommand
        public ActionResult Index()
        {
            var response = BusinessHelper.GetResource("elalmis.ne@gmail.com","123456", null, null);
            if (!response.Success)
            {

            };
            WebGridColumn[] gridColumns = new WebGridColumn[3];
            WebGridColumn column = new WebGridColumn();
            column.ColumnName = "AssemblyName";
            column.Header = "Assembly Adı";
            gridColumns[0] = column;
            WebGrid a = new WebGrid();
            
            column = new WebGridColumn();
            column.ColumnName = "Code";
            column.Header = "Kod";
            gridColumns[1] = column;

            column = new WebGridColumn();
            column.ColumnName = "Text";
            column.Header = "Text";
            gridColumns[2] = column;

            ViewBag.Columns = gridColumns;
            ViewBag.DataSource = response.Value;
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}