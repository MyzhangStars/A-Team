using IOA.IRepository;
using IOA.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IOA.API.Controllers
{
    [ApiController]
    [Route("LoginAPI")]
    //设置跨域处理的 代理  如果在控制器内的所以都有对应的跨域限制
    //[EnableCors("any")]  //如果在方法头则方法限制
    public class LoginAPIController : Controller
    {
        private readonly ILogger<LoginAPIController> _logger;
        public readonly ILoginRepository _iloginRepository;
        //public readonly IHttpContextAccessor _ihttpContextAccessor;
        public LoginAPIController(ILogger<LoginAPIController> logger, ILoginRepository iloginRepository/*, IHttpContextAccessor httpContextAccessor*/)
        {
            _iloginRepository = iloginRepository;
            _logger = logger;
            //_ihttpContextAccessor = httpContextAccessor;
        }

        //#region  //生成验证码
        //[Route(nameof(VerificationCode))]
        //[HttpGet]
        //public string VerificationCode()
        //{
        //    SessionHelper session = new SessionHelper(_ihttpContextAccessor.HttpContext);
        //    string code = VerificationCodeHelper.CreateValidateCode(4);
        //    var file = VerificationCodeHelper.CreateValidateGraphic(code);
        //    session.SetSession("code", code);
        //    return file.ToString();
        //}
        //#endregion

        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    _logger.LogTrace("Trace");
        //    _logger.LogError("Error");
        //    return new string[] { "value1", "value2" };
        //}

        #region //登录方法
        [Route(nameof(Login))]
        [HttpGet]
        public object Login(string userName = null, string userPwd = null)
        {

            int num;
            int userid = 0;
            UserModel list = _iloginRepository.LookingFor(userName, userPwd);
            if (list != null)
            {
                num = 1;  //登录成功
                userid = list.UserId;
                //HttpContext.Session.SetString("userID", list.UserId.ToString());

            }
            else
            {
                num = 3; //用户名或密码错误
            }

            return new { num = num, userid = userid };
        }

        #endregion

        #region //注册
        [Route(nameof(Registered))]
        [HttpPost]
        public int Registered(string userModel)
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
            return i;
        }
        #endregion
    }
}
