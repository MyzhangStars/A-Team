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
    public class HomeController : Controller
    {
        public readonly IHomeRepositroy _ihomeRepositroy;
        public HomeController(IHomeRepositroy ihomeRepositroy)
        {
            _ihomeRepositroy = ihomeRepositroy;
        }
        //拼接头部
        public string  HeadMenu()
        {
            string someMenu = "select MenuModel.MenuId,MenuModel.MenuName,UserModel.UserName from RoleMenu join MenuModel on MenuModel.MenuID=RoleMenu.MenuID join UserRole on RoleMenu.RoleID=UserRole.RoleID join UserModel on UserModel.UserID=UserRole.UserID" +
                " where UserModel.UserID=@userID";
            List<MenuModel> menuModel = _ihomeRepositroy.Show(someMenu,new { @userID =Convert.ToInt32( HttpContext.Session.GetString("userID")) });
            foreach (var item in menuModel)
            {
                item.MenuName==""
            }
            if ()
            {

            }

            StringBuilder joinHead = new StringBuilder();
            joinHead.Append("<li class='layui-nav-item' lay-unselect>");
            joinHead.Append("<a href = 'javascript:;' layadmin-event='refresh' title=''>");
            joinHead.Append("< li class='layui - nav - item' lay-unselect>");
            joinHead.Append("<i class='layui - icon layui - icon - refresh - 3'></i>");
            joinHead.Append("</a>");
            joinHead.Append("</li>");
            return joinHead.ToString();
        }
    }
}
