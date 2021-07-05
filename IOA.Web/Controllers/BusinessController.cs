using IOA.Common;
using IOA.IRepository;
using IOA.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOA.Web.Controllers
{
    public class BusinessController : Controller
    {
        public readonly IBusinessRepositroy _ibusinessRepositroy;
        public BusinessController(IBusinessRepositroy businessRepositroy)
        {
            _ibusinessRepositroy = businessRepositroy;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BusinessPut(BusinessModel businessModel)
        {
            string data = HttpClientHelper.GetAll(HttpType.HttpPost, "/BusinessAPI/Index", businessModel);
            return Ok(data);
        }
        //反填
        //修改（）

    }
}
