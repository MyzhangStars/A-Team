using IOA.Common;
using IOA.IRepository;
using IOA.Model;
using Repositroy;
using System.Collections.Generic;
using System.Text;

namespace IOA.Repository
{
    public class HomeRepositroy : BaseRepositroy<MenuModel>, IHomeRepositroy
    {
        ////获取头部方法
        //public List<MenuModel> headData(string sql, object param)
        //{
        //    //string sql = "select MenuModel.MenuName,UserModel.UserName from RoleMenu join MenuModel on MenuModel.MenuID=RoleMenu.MenuID join UserRole on RoleMenu.RoleID=UserRole.RoleID join UserModel on UserModel.UserID=UserRole.UserID where MenuModel.MenuParentID=0  and UserModel.UserID=1";

        //}

        public List<MenuModel> leftData(int parentID, int? userId = 1)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select MenuModel.MenuId,MenuModel.MenuName,UserModel.UserName from RoleMenu join MenuModel on MenuModel.MenuID=RoleMenu.MenuID join UserRole on RoleMenu.RoleID=UserRole.RoleID join UserModel on UserModel.UserID=UserRole.UserID");
            stringBuilder.Append(" where MenuModel.MenuParentID=@parentID and UserModel.UserID=@userID  and RoleMenu.RoleMenuState=1");
            if (parentID == 1)
            {
                stringBuilder.Append(" and MenuModel.MenuName='个人中心'");
            }
            if (parentID == 4)
            {
                stringBuilder.Append(" and MenuModel.MenuName='任务管理'");
            }

            List<MenuModel> leftData = DapperHelper<MenuModel>.Query(stringBuilder.ToString(), new { @parentID = parentID, @userID = userId });
            return leftData;
        }

    }
}
