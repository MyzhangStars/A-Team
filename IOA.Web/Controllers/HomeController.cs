using IOA.Common;
using IOA.IRepository;
using IOA.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text;

namespace IOA.Web.Controllers
{
    public class HomeController : Controller
    {
        public readonly IHomeRepositroy _ihomeRepositroy;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHomeRepositroy ihomeRepositroy)
        {
            _ihomeRepositroy = ihomeRepositroy;
            _logger = logger;
        }
        public IActionResult LoggerTest()
        {
            _logger.LogInformation("你访问了首页");
            _logger.LogWarning("警告信息");
            _logger.LogError("错误信息");
            return Content("success");
        }
        #region //foreach拼接菜单栏
        public IActionResult Index(int parentID, int userId)
        {

            #region MyRegion
            //int userId= Convert.ToInt32(HttpContext.Session.GetString("userID"));
            //  ViewBag.userName = HttpContext.Session.GetString("userName");
            //  //获取全部菜单
            //  List<MenuModel> leftNext = _ihomeRepositroy.Show("select * from MenuModel");
            //  //获取左侧菜单栏
            //  List<MenuModel> left = _ihomeRepositroy.leftData(userId, parentID);
            //  StringBuilder leftData = new StringBuilder();
            //  foreach (var item in left)
            //  {
            //      leftData.Append("<li data-name = 'home' class='layui-nav-item layui-nav-itemed'>");
            //      leftData.Append($"<a href = 'javascript:;'  lay-direction = '2' >");
            //      leftData.Append($"<cite>{item.MenuName}</cite></a>");
            //      foreach (var itemNext in leftNext)
            //      {
            //          if (itemNext.MenuParentID.Equals(item.MenuId))
            //          {
            //              leftData.Append("<dl class='layui-nav-child'>");
            //              leftData.Append("<dd class='layui-nav-itemed'>");
            //              leftData.Append($"<a href ='javascript:;'>{itemNext.MenuName}</a>");
            //              foreach (var itemNext2 in leftNext)
            //              {
            //                  if (itemNext2.MenuParentID.Equals(itemNext.MenuId))
            //                  {
            //                      leftData.Append("<dl class='layui-nav-child'>");
            //                      if (itemNext2.MenuLink == null || itemNext2.MenuLink == "")
            //                      {
            //                          itemNext2.MenuLink = "javascript:;";
            //                      }
            //                      leftData.Append($"<dd><a lay-href='{itemNext2.MenuLink}'>{itemNext2.MenuName}</a>");
            //                      foreach (var itemNext3 in leftNext)
            //                      {
            //                          if (itemNext3.MenuParentID.Equals(itemNext2.MenuId))
            //                          {
            //                              leftData.Append("<dl class='layui-nav-child'>");
            //                              if (itemNext3.MenuLink == null || itemNext3.MenuLink == "")
            //                              {
            //                                  itemNext3.MenuLink = "javascript:;";
            //                              }
            //                              leftData.Append($"<dd><a lay-href='{itemNext3.MenuLink}'>{itemNext3.MenuName}</a>");
            //                              leftData.Append("</dd></dl>");
            //                          }

            //                      }
            //                      leftData.Append("</dd></dl>");

            //                  }
            //              }
            //              leftData.Append("</dd></dl>");
            //          }
            //      }
            //      leftData.Append("</li>");
            //  }


            //  ViewBag.LeftMenu = leftData.ToString();
            //  return View();
            #endregion
            ViewBag.userId = userId;
            ViewBag.LeftMenu = HttpClientHelper.GetAll(HttpType.HttpGet, $"/HomeAPI/Index?parentID={parentID}&userId={userId}");
            return View();
        }

        #endregion

        //首页
        public IActionResult HomePage()
        {
            return View();
        }
        #region //递归拼接菜单栏


        public IActionResult Index2(int parentID)
        {
            //获取左侧菜单栏
            List<MenuModel> left = _ihomeRepositroy.leftData(parentID);
            StringBuilder leftData = new StringBuilder();
            foreach (var item in left)
            {
                leftData.Append("<li data-name = 'home' class='layui-nav-item layui-nav-itemed'>");
                leftData.Append($"<a href = 'javascript:;'  lay-direction = '2' >");
                leftData.Append($"<cite>{item.MenuName}</cite></a>");
                LeftNext(leftData, item.MenuId);
                leftData.Append("</li>");
            }
            ViewBag.LeftMenu = leftData.ToString();
            return View();

        }
        public void LeftNext(StringBuilder leftData, int parentID)
        {
            List<MenuModel> left = _ihomeRepositroy.leftData(parentID);
            StringBuilder Data = new StringBuilder();
            foreach (var item in left)
            {
                StringBuilder NextData = new StringBuilder();
                NextData.Append("<dl class='layui-nav-child'>");
                if (item.MenuLink == null || item.MenuLink == "")
                {
                    item.MenuLink = "javascript:;";
                }
                NextData.Append($"<dd><a lay-href='{item.MenuLink}'>{item.MenuName}</a>");
                LeftNext(NextData, item.MenuId);
                NextData.Append("</dd></dl>");
                Data.Append(NextData);
            }
            leftData.Append(Data);
        }

        #endregion
    }
}
