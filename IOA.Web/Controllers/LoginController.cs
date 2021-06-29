using IOA.Common;
using IOA.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            string num = "";
            if (HttpContext.Session.GetString("code").ToString().ToLower().Equals(code.ToLower()))
            {
                num = HttpClientHelper.GetAll(HttpType.HttpGet, $"/LoginAPI/Login?userName={userName}&userPwd={userPwd}");
            }
            else
            {
                num = "2";  //验证码错误
            }
            return Ok(num);
        }

        #endregion
    }
}
