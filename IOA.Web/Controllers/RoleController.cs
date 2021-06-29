using IOA.Common;
using IOA.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace IOA.Web.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //显示角色列表
        public IActionResult Show()
        {
            string data = HttpClientHelper.GetAll(HttpType.HttpGet, "/RoleAPI/Index");
            List<RoleModel> jsonData = JsonConvert.DeserializeObject<List<RoleModel>>(data);
            return Ok(new { data = jsonData, count = 0, code = 0 });
        }
        //显示树
        public IActionResult Assign()
        {
            return View();
        }
        //拼接树形父级
        public IActionResult Joining()
        {
            //string data = HttpClientHelper.GetAll(HttpType.HttpGet, "/MenuAPI/Trees");
            //return Json(data);
            List<MenuModel> data = DapperHelper<MenuModel>.Query("select * from MenuModel", null);
            List<MenuModel> treeFather = data.Where(x => x.MenuParentID == 0).ToList();
            List<Dictionary<string, object>> treeJson = new List<Dictionary<string, object>>();
            foreach (var item in treeFather)
            {
                Dictionary<string, object> json = new Dictionary<string, object>();
                json.Add("id", item.MenuId);
                json.Add("title", item.MenuName);
                Tree_Next(data, json, item.MenuId);//调用递归完成子集拼接
                treeJson.Add(json);
            }
            return Ok(treeJson);
        }
        //递归拼接树形子集
        public void Tree_Next(List<MenuModel> data, Dictionary<string, object> json, int parentId)
        {
            List<MenuModel> treeFather = data.Where(x => x.MenuParentID == parentId).ToList();
            List<Dictionary<string, object>> treeJson = new List<Dictionary<string, object>>();
            if (treeFather.Count == 0)
            {
                json.Add("children", null);
                return;
            }
            foreach (var item in treeFather)
            {
                Dictionary<string, object> json1 = new Dictionary<string, object>();
                json1.Add("id", item.MenuId);
                json1.Add("title", item.MenuName);
                Tree_Next(data, json1, item.MenuId);//调用递归完成子集拼接
                treeJson.Add(json1);
            }
            json.Add("children", treeJson);
        }


    }

}
