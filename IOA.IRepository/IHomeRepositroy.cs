﻿using IOA.Model;
using IRepositroy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOA.IRepository
{
    public interface IHomeRepositroy: IBaseRepositroy<MenuModel>
    {
        ////获取头部方法
        //List<MenuModel> headData(string sql, object param);
        //获取用户获取左侧菜单
        List<MenuModel> leftData(int parentID = 0);

    }
}
