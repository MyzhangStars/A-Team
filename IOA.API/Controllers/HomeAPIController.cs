using IOA.Common;
using IOA.IRepository;
using IOA.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOA.API.Controllers
{
    public class HomeAPIController : Controller
    {
        public readonly IHomeRepositroy _ihomeRepositroy;
        public HomeAPIController(IHomeRepositroy ihomeRepositroy)
        {
            _ihomeRepositroy = ihomeRepositroy;
        }
        ////拼接头部
        //public string  HeadMenu()
        //{
        //    //查询用户的角色
        //    string role= "select UserModel.UserId,UserModel.UserName,RoleModel.RoleName from UserRole join UserModel on UserModel.UserId=UserRole.UserId join RoleModel on RoleModel.RoleId=UserRole.RoleId";
        //    List<RoleModel> roleModel = DapperHelper<RoleModel>.Query(role,null);
        //    //循环判断 给该角色的头部
        //    foreach (var item in roleModel)
        //    {
        //        if (item.RoleName=="管理员")
        //        {
        //            StringBuilder joinHead = new StringBuilder();
        //            joinHead.Append("<li class='layui-nav-item' lay-unselect>");
        //            joinHead.Append("<a href = 'javascript:;' layadmin-event='refresh' title='个人中心'>");
        //            joinHead.Append("< li class='layui - nav - item' lay-unselect>");
        //            joinHead.Append("<i class='layui - icon layui - icon - refresh - 3'></i>");
        //            joinHead.Append("</a>");
        //            joinHead.Append("</li>");
        //            joinHead.Append("<li class='layui-nav-item' lay-unselect>");
        //            joinHead.Append("<a href = 'javascript:;' layadmin-event='refresh' title='个人中心'>");
        //            joinHead.Append("< li class='layui - nav - item' lay-unselect>");
        //            joinHead.Append("<i class='layui - icon layui - icon - refresh - 3'></i>");
        //            joinHead.Append("</a>");
        //            joinHead.Append("</li>");
        //            return joinHead.ToString();
        //        }
        //    }
        //    string someMenu = "select MenuModel.MenuId,MenuModel.MenuName,UserModel.UserName from RoleMenu join MenuModel on MenuModel.MenuID=RoleMenu.MenuID join UserRole on RoleMenu.RoleID=UserRole.RoleID join UserModel on UserModel.UserID=UserRole.UserID" +
        //        " where UserModel.UserID=@userID";
        //    List<MenuModel> menuModel = _ihomeRepositroy.Show(someMenu,new { @userID =Convert.ToInt32( HttpContext.Session.GetString("userID")) });
        //    foreach (var item in menuModel)
        //    {
        //        item.MenuName==""

        //    }
        //    if ()
        //    {

        //    }

            

           
        //}

        //拼接左侧菜单
        public  IActionResult LeftMenu(int parentID)
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
                leftData.Append("<li class='layui-nav-item' lay-unselect>");
                leftData.Append($"<a lay-href='app/message/index.html' layadmin-event='message' lay-text='{item.MenuName}'>");
                leftData.Append("<span class='layui-badge-dot'></span>");
                leftData.Append("</a> </li>");

            }

            ViewBag.LefuMenu = leftData.ToString();
            return View();
        }
    }
}
