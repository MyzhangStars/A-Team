using IOA.Common;
using IOA.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IOA.Web.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Show()
        {
           string  data= HttpClientHelper.GetAll(HttpType.HttpGet, "/RoleAPI/Index");
            List<RoleModel> jsonData = JsonConvert.DeserializeObject<List<RoleModel>>(data);
            return Ok(new { data= jsonData, count=0,code=0});
        }
        public IActionResult Assign()
        {
            return View();
        }
        public IActionResult Joining()
        {
            string data = HttpClientHelper.GetAll(HttpType.HttpGet, "/MenuAPI/Trees");
            return Json(data);
        }
    }
}
