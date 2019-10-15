using Architecture.View.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Architecture.WebApplication.Controllers
{
    public class BaseController : UserControllerBase
    {
        public BaseController() : base("elalmis.ne@gmail.com", "123456") { }
    }
}