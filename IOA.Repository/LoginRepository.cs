using IOA.Common;
using IOA.IRepository;
using IOA.Model;
using Repositroy;
using System;
using System.Collections.Generic;

namespace IOA.Repository
{
    public class LoginRepository : BaseRepositroy<UserModel>, ILoginRepository
    {
        //数据库查登录名称 和密码
        public List<UserModel> LookingFor(string userName, string userPwd)
        {
            string sql = "select * from UserModel where UserName=@userName and UserPwd=@UserPwd";
            List<UserModel> data = DapperHelper<UserModel>.Query(sql, new { @userName = userName, userPwd = userPwd });
            return data;
        }

        UserModel ILoginRepository.LookingFor(string userName, string userPwd)
        {
            throw new NotImplementedException();
        }
    }
}
