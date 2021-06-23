﻿using IOA.Common;
using IOA.IRepository;
using IOA.Model;
using IRepositroy;
using Repositroy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOA.Repository
{
    public class HomeRepositroy : BaseRepositroy<MenuModel>, IHomeRepositroy
    {
        ////获取头部方法
        //public List<MenuModel> headData(string sql, object param)
        //{
        //    //string sql = "select MenuModel.MenuName,UserModel.UserName from RoleMenu join MenuModel on MenuModel.MenuID=RoleMenu.MenuID join UserRole on RoleMenu.RoleID=UserRole.RoleID join UserModel on UserModel.UserID=UserRole.UserID where MenuModel.MenuParentID=0  and UserModel.UserID=1";
            
        //}

        public List<MenuModel> leftData( int parentID=0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select MenuModel.MenuId,MenuModel.MenuName,UserModel.UserName from RoleMenu join MenuModel on MenuModel.MenuID=RoleMenu.MenuID join UserRole on RoleMenu.RoleID=UserRole.RoleID join UserModel on UserModel.UserID=UserRole.UserID");
            stringBuilder.Append(" where MenuModel.MenuParentID=@parentID and UserModel.UserID=@userID");

            List<MenuModel> leftData = DapperHelper<MenuModel>.Query(stringBuilder.ToString(), new { @parentID = parentID, @userID = 1 });
            return leftData;
        }

    }
}