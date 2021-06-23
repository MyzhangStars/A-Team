using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOA.Web.Controllers
{
    public class LoginController : Controller
    {
        //登录视图
        public IActionResult Index()
        {
            return View();
        }
    }
}
