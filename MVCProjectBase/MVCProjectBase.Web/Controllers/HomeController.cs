using MVCProjectBase.Data.UnitOfWork;
using MVCProjectBase.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjectBase.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserService _userService;
        public HomeController(IUserService userService, IUnitOfWork uow)
           : base(uow)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            var response = _userService.Select();
            return View(response.Value);
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