using IOA.Common;
using IOA.IRepository;
using IOA.Model;
using Repositroy;

namespace IOA.Repository
{
    public class LoginRepository : BaseRepositroy<UserModel>, ILoginRepository
    {
        //数据库查登录名称 和密码
        public UserModel LookingFor(string userName, string userPwd)
        {
            string sql = "select * from UserModel where UserName=@userName and UserPwd=@UserPwd";
            UserModel data = DapperHelper<UserModel>.QuerySingle(sql, new { @userName = userName, userPwd = userPwd });
            return data;
        }
    }
}
