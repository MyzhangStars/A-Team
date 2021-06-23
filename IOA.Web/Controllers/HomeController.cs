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

        public IActionResult Index(int parentID )
        {
            StringBuilder leftData = new StringBuilder();
            //获取左侧菜单栏
            List<MenuModel> left = _ihomeRepositroy.leftData(parentID);
            //获取全部菜单
            List<MenuModel> leftNext = _ihomeRepositroy.Show("select * from MenuModel");

            foreach (var item in left)
            {
                leftData.Append("<li data-name = 'home' class='layui-nav-item layui-nav-itemed'>");
                leftData.Append($"<a href = 'javascript:;'  lay-direction = '2' >");
                leftData.Append($"<cite>{item.MenuName}</cite>");
                leftData.Append("</a></li>");
            }

            ViewBag.LeftMenu = leftData.ToString();

            if (item.MenuLink == null || item.MenuLink == "")
            {
                item.MenuLink = "javascript:;";
            }


            return View();
        }

          <dl class="layui-nav-child">
                                <dd class="layui-nav-itemed">
                                    <a href = "javascript:;" > 系统设置 </ a >
                                    < dl class="layui-nav-child">
                                        <dd><a lay-href="set/system/website.html">网站设置</a></dd>
                                        <dd><a lay-href="set/system/email.html">邮件服务</a></dd>
                                    </dl>
                                </dd>
        public IActionResult Class()
        {
            return View();
        }
        //拼接左侧菜单
        public IActionResult LeftMenu(int parentID)
        {
            
            return View();
        }

    }
}
