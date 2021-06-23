using IOA.Common;
using IOA.IRepository;
using IOA.Model;
using IOA.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOA.Web.Controllers
{
    public class HomeController : Controller
    {
     
        public readonly IHomeRepositroy _ihomeRepositroy;
        public HomeController(IHomeRepositroy ihomeRepositroy)
        {
            _ihomeRepositroy = ihomeRepositroy;
        }



        public IActionResult Index(int parentID)
        {
            //获取全部菜单
            List<MenuModel> leftNext = _ihomeRepositroy.Show("select * from MenuModel");
            //获取左侧菜单栏
            List<MenuModel> left = _ihomeRepositroy.leftData(parentID);
            StringBuilder leftData = new StringBuilder();
                foreach (var item in left)
                {
                    leftData.Append("<li data-name = 'home' class='layui-nav-item layui-nav-itemed'>");
                    leftData.Append($"<a href = 'javascript:;'  lay-direction = '2' >");
                    leftData.Append($"<cite>{item.MenuName}</cite></a>");
                    foreach (var itemNext in leftNext)
                    {
                        if (itemNext.MenuParentID.Equals(item.MenuId))
                        {
                            leftData.Append("<dl class='layui-nav-child'>");
                            leftData.Append("<dd class='layui-nav-itemed'>");
                            leftData.Append($"<a href ='javascript:;'>{itemNext.MenuName}</a>");
                            foreach (var itemNext2 in leftNext)
                            {
                                if (itemNext2.MenuParentID.Equals(itemNext.MenuId))
                                {
                                    leftData.Append("<dl class='layui-nav-child'>");
                                    if (itemNext2.MenuLink == null || itemNext2.MenuLink == "")
                                    {
                                        itemNext2.MenuLink = "javascript:;";
                                    }
                                    leftData.Append($"<dd><a lay-href='{itemNext2.MenuLink}'>{itemNext2.MenuName}</a></dd>");
                                    leftData.Append("</dl>");

                                }
                            }
                            leftData.Append("</dd></dl>");
                        }
                    }
                    leftData.Append("</li>");
                }
           

            ViewBag.LeftMenu = leftData.ToString();
            return View();
        }

        public IActionResult Class()
        {
            return View();
        }
      
    }
}
