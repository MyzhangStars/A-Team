using IOA.Model;
using IRepositroy;

namespace IOA.IRepository
{
    public interface ILoginRepository : IBaseRepositroy<UserModel>
    {
        //数据库查登录名称 和密码
        UserModel LookingFor(string userName, string userPwd);

    }
}
