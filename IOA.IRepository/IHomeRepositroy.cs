using IOA.Model;
using IRepositroy;
using System.Collections.Generic;

namespace IOA.IRepository
{
    public interface IHomeRepositroy : IBaseRepositroy<MenuModel>
    {

        //获取用户获取左侧菜单
        List<MenuModel> leftData(int parentID = 0, int? userId = 1);

    }
}
