using IOA.Common;
using IOA.IRepository;
using IOA.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace IOA.API.Controllers
{
    [ApiController]
    [Route("UserAPI")]
    public class UserAPIController : Controller
    {
        public readonly IUserRepositroy _iuserRepositroy;

        public UserAPIController(IUserRepositroy iuserRepositroy)
        {
            _iuserRepositroy = iuserRepositroy;
        }

        //显示员工管理
        [Route(nameof(Index))]
        [HttpGet]
        public List<UserModel> Index(int page, int limit)
        {
            string sql = "select * from UserModel";
            List<UserModel> data = _iuserRepositroy.Show(sql);
            data = data.Skip((page - 1) * limit).Take(limit).ToList();
            return data;
        }

        //注册
        [Route(nameof(UserRegister))]
        [HttpPost]
        public object UserRegister(UserModel userModel)
        {
            //string userString = JsonConvert.SerializeObject(param);
            //UserModel userModel = JsonConvert.DeserializeObject<UserModel>(userString);
            string sql = $"insert into UserModel(UserName,UserPwd,UserSex,UserCard,UserPhone,UserNational,UserEmail,UserMajor,UserJoinInDate,UserIsAdmin) values(@UserName,@UserPwd,UserSex,@UserCard,@UserPhone,@UserNational,@UserEmail,@UserMajor,@UserJoinInDate,@UserIsAdmin))";
            int i = _iuserRepositroy.ZSG(sql, new { @UserName = userModel.UserName, @UserPwd = userModel.UserPwd, UserSex = userModel.UserSex, @UserCard = userModel.UserCard, @UserPhone = userModel.UserPhone, @UserNational = userModel, @UserEmail = userModel.UserEmail, @UserMajor = userModel.UserMajor, @UserJoinInDate = userModel.UserJoinInDate, @UserIsAdmin = userModel.UserIsAdmin });
            return i;
        }
        //用户反填
        [Route(nameof(Assignment))]
        [HttpGet]
        public object Assignment(int id)
        {
            UserModel data = DapperHelper<UserModel>.QueryFirst("select * from UserModel where UserId=@id", new { @id = id });
            return data;
        }

    }
}
