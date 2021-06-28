using IOA.IRepository;
using IOA.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOA.API.Controllers
{
    [ApiController]
    [Route("MenuAPI")]
    public class MenuAPIController : Controller
    {
        public readonly IMenuRepositroy _imenuRepositroy;

        public MenuAPIController(IMenuRepositroy menuRepositroy)
        {
            _imenuRepositroy = menuRepositroy;
        }



        #region 拼接树

     
        /// <summary>
        /// 字典拼接子级传递值报错
        /// 系统。IOA.API.Controllers.MenuAPIController InvalidOperationException:“行动”。Tree_Next (IOA.API)'有多个参数，
        /// 这些参数被指定或推断为来自请求体的绑定。每个操作只能绑定一个参数。检查以下参数，并使用'FromQueryAttribute'指定从query绑定，
        /// 'FromRouteAttribute'指定从route绑定，'FromBodyAttribute'指定从body绑定参数:
        /// 列表<MenuModel> 数据
        /// 字典<字符串、对象> json”
        /// </summary>
        /// <returns></returns>
        [Route(nameof(Trees))]
        [HttpGet]
        //拼接树形父级
        public object Trees()
        {
            List<MenuModel> data = _imenuRepositroy.Show("select * from MenuModel");
            List<MenuModel> treeFather = data.Where(x => x.MenuParentID == 0).ToList();
            List<Dictionary<string, object>> treeJson = new List<Dictionary<string, object>>();
            foreach (var item in treeFather)
            {
                Dictionary<string, object> json = new Dictionary<string, object>();
                json.Add("id", item.MenuId);
                json.Add("title", item.MenuName);
                json.Add("spread", true);
                //Tree_Next(data, json, item.MenuId);//调用递归完成子集拼接
                treeJson.Add(json);
            }
            return treeJson;
        }
        ////递归拼接树形子集
        //public void Tree_Next(List<MenuModel> data, Dictionary<string, object> json, int parentId)
        //{
        //    List<MenuModel> treeFather = data.Where(x => x.MenuParentID == parentId).ToList();
        //    List<Dictionary<string, object>> treeJson = new List<Dictionary<string, object>>();
        //    if (treeFather.Count == 0)
        //    {
        //        json.Add("children", null);
        //        return;
        //    }
        //    foreach (var item in treeFather)
        //    {
        //        Dictionary<string, object> json1 = new Dictionary<string, object>();
        //        json1.Add("id", item.MenuId);
        //        json1.Add("title", item.MenuName);
        //        json1.Add("spread", true);
        //        Tree_Next(data, json1, item.MenuId);//调用递归完成子集拼接
        //        treeJson.Add(json1);
        //    }
        //    json.Add("children", treeJson);
        //}
        #endregion


    }
}
