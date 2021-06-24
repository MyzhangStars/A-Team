using IOA.Common;
using IOA.IRepository;
using IOA.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace IOA.API.Controllers
{
    [ApiController]
    [Route("LoginAPI")]
    //设置跨域处理的 代理  如果在控制器内的所以都有对应的跨域限制
    [EnableCors("any")]  //如果在方法头则方法限制
    public class LoginAPIController : Controller
    {
        public readonly ILoginRepository _iloginRepository;
        public LoginAPIController(ILoginRepository iloginRepository)
        {
            _iloginRepository = iloginRepository;
        }

        #region  //生成验证码
        [HttpGet]
        public void VerificationCode()
        {
            string code = VerificationCodeHelper.CreateValidateCode(4);
            var file = VerificationCodeHelper.CreateValidateGraphic(code);
            HttpContext.Session.SetString("code", code);
           var a= File(file, @"/image/jpg");
        }
        #endregion

        #region //登录方法
        [Route(nameof(Login))]
        public IActionResult Login(string userName = null, string userPwd = null, string code = null)
        {
            int num;
            if (HttpContext.Session.GetString("code").ToString().ToLower().Equals(code.ToLower()))
            {
                UserModel list = _iloginRepository.LookingFor(userName, userPwd);
                if (list != null)
                {
                    num = 1;  //登录成功
                    HttpContext.Session.SetString("userID", list.UserId.ToString());
                }
                else
                {
                    num = 3; //用户名或密码错误
                }
            }
            else
            {
                num = 2;  //验证码错误
            }
            return Ok(num);
        }

        #endregion

        #region //注册
        [Route(nameof(Registered))]
        public IActionResult Registered(string userModel)
        {
            UserModel list = JsonConvert.DeserializeObject<UserModel>(userModel);
            string sql = "insert into UserModel values('@userName','@userPwd','@userSex','@userCard','@userPhone','@userNational','@userEmail','@userMajor','@userJoinInDate','@userDimissionDate','@userDimissionCause','@userDeleteMark','@userIsAdmin','@userCreateName','@userCreateDate')";
            int i = _iloginRepository.ZSG(sql, new
            {
                @userName = list.UserName,
                @userPwd = list.UserPwd,
                @userSex = list.UserSex,
                @userCard = list.UserCard,
                @userPhone = list.UserPhone,
                @userNational = list.UserNational,
                @userEmail = list.UserEmail,
                @userMajor = list.UserMajor,
                @userJoinInDate = list.UserJoinInDate,
                @userDimissionDate = list.UserDimissionDate,
                @userDimissionCause = list.UserDimissionCause,
                @userDeleteMark = list.UserDeleteMark,
                @userIsAdmin = list.UserIsAdmin,
                @userCreateName = list.UserCreateName,
                @userCreateDate = list.UserCreateDate
            });
            return Ok(i);
        }
        #endregion
    }
}
