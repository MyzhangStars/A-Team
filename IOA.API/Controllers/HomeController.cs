using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOA.API.Controllers
{
    public class HomeController : Controller
    {
        //主页面
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
