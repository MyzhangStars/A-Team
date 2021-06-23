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

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Class()
        {
            return View();
        }
        //拼接左侧菜单
        public IActionResult LeftMenu(int parentID)
        {
            StringBuilder leftData = new StringBuilder();
            //获取左侧菜单栏
            List<MenuModel> left = _ihomeRepositroy.leftData(parentID);
           

            foreach (var item in left)
            {
                if (item.MenuLink == null || item.MenuLink == "")
                {
                    item.MenuLink = "javascript:;";
                }
                            
                leftData.Append("<li data-name = 'home' class='layui-nav-item layui-nav-itemed'>");
                leftData.Append($"<a href = 'javascript:;' lay-tips = '主页' lay-direction = '2' >");
                leftData.Append("<i class='layui-icon layui-icon-home'></i>");
                leftData.Append($"<cite>{item.MenuName}</cite>");
                leftData.Append("</a></li>");

                            
                          
                        

            }

            ViewBag.LefuMenu = leftData.ToString();
            return View();
        }

    }
}
