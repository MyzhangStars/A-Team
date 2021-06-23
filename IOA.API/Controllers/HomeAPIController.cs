using IOA.Common;
using IOA.IRepository;
using IOA.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOA.API.Controllers
{
    //设置跨域处理的 代理  如果在控制器内的所以都有对应的跨域限制
    [EnableCors("any")]  //如果在方法头则方法限制
    public class HomeController : Controller
    {
        //实例化
        public readonly IHomeRepositroy _ihomeRepositroy;
        public HomeController(IHomeRepositroy ihomeRepositroy)
        {
            _ihomeRepositroy = ihomeRepositroy;
        }


        #region //点击全部应用 显示左边
        public IActionResult Index(int parentID)
        {
            StringBuilder leftData = new StringBuilder();
            //获取左侧菜单栏
            List<MenuModel> left = _ihomeRepositroy.leftData(parentID);
            //获取全部菜单
            List<MenuModel> leftNext = _ihomeRepositroy.Show("select * from MenuModel");

            //循环顶级菜单栏
            foreach (var item in left)
            {
                //拼接
                leftData.Append("<li data-name = 'home' class='layui-nav-item layui-nav-itemed'>");
                leftData.Append($"<a href = 'javascript:;'  lay-direction = '2' >");
                leftData.Append($"<cite>{item.MenuName}</cite></a>");
                //循环所有菜单栏
                foreach (var itemNext in leftNext)
                {
                    //判断菜单栏的子节点
                    if (itemNext.MenuParentID.Equals(item.MenuId))
                    {
                        //拼接
                        leftData.Append("<dl class='layui-nav-child'>");
                        leftData.Append("<dd class='layui-nav-itemed'>");
                        leftData.Append($"<a href ='javascript:;'>{itemNext.MenuName}</a>");
                        //判断菜单栏的子节点
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
            //把所有拼接放进viewbag 传给视图
            ViewBag.LeftMenu = leftData.ToString();
            return View();
        }
        #endregion

    }
}
