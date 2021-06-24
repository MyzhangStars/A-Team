using IOA.Common;
using IOA.IRepository;
using IOA.Model;
using IOA.Web.LoginFilter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IOA.Web.Controllers
{
    public class LoginController : Controller
    {
        public readonly ILoginRepository _iloginRepository;
        public LoginController(ILoginRepository iloginRepository)
        {
            _iloginRepository = iloginRepository;
        }

        //登录视图
        public IActionResult Index()
        {
            return View();

        }

        #region  //生成验证码
        public IActionResult VerificationCode()
        {
            string code = VerificationCodeHelper.CreateValidateCode(4);
            HttpContext.Session.SetString("code", code);
            var file = VerificationCodeHelper.CreateValidateGraphic(code);
            return File(file, @"image/Png");
        }
        #endregion

        #region //登录方法
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
                    HttpContext.Session.SetString("userName", list.UserName.ToString());
                    HttpContext.Session.SetString("userPwd", list.UserPwd.ToString());

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
    }
}
