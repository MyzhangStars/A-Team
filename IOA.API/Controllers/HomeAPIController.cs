﻿using IOA.Common;
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
    [ApiController]
    [Route("HomeAPI")]
    //设置跨域处理的 代理  如果在控制器内的所以都有对应的跨域限制
    /*[EnableCors("any")]*/  //如果在方法头则方法限制
    public class HomeAPIController : Controller
    {
        //实例化
        public readonly IHomeRepositroy _ihomeRepositroy;
        public HomeAPIController(IHomeRepositroy ihomeRepositroy)
        {
            _ihomeRepositroy = ihomeRepositroy;
        }

        #region //点击全部应用 显示左边
        [Route(nameof(Index))]
        [HttpGet]
        public string Index(int parentID)
        {
            //int userId = Convert.ToInt32(HttpContext.Session.GetString("userID"));
            //ViewBag.userName = HttpContext.Session.GetString("userName");
            //获取全部菜单
            List<MenuModel> leftNext = _ihomeRepositroy.Show("select * from MenuModel");
            //获取左侧菜单栏
            List<MenuModel> left = _ihomeRepositroy.leftData(1,parentID);
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
                                leftData.Append($"<dd><a lay-href='{itemNext2.MenuLink}'>{itemNext2.MenuName}</a>");
                                foreach (var itemNext3 in leftNext)
                                {
                                    if (itemNext3.MenuParentID.Equals(itemNext2.MenuId))
                                    {
                                        leftData.Append("<dl class='layui-nav-child'>");
                                        if (itemNext3.MenuLink == null || itemNext3.MenuLink == "")
                                        {
                                            itemNext3.MenuLink = "javascript:;";
                                        }
                                        leftData.Append($"<dd><a lay-href='{itemNext3.MenuLink}'>{itemNext3.MenuName}</a>");
                                        leftData.Append("</dd></dl>");
                                    }

                                }
                                leftData.Append("</dd></dl>");

                            }
                        }
                        leftData.Append("</dd></dl>");
                    }
                }
                leftData.Append("</li>");
            }
            
            return  leftData.ToString();
        }
        #endregion

    }
}
