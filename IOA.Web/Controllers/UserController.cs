using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOA.IRepository;
using IOA.Repository;
using IOA.Model;
using IOA.Common;
using Newtonsoft.Json;

namespace IOA.Web.Controllers
{
    public class UserController : Controller
    {
       
        //显示页面
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Show(int page = 1, int limit = 10)
        {
            string data = HttpClientHelper.GetAll(HttpType.HttpGet, "/UserAPI/Index?page="+page+"&limit="+limit);
            List<UserModel> jsonData = JsonConvert.DeserializeObject<List<UserModel>>(data);
            return Ok(new
            {
                data = jsonData,
                code = 0,
                count = jsonData.Count()
            });
        }

       
        //注册页面
        public IActionResult RegisterView()
        {
            return View();
        }
        //注册
        public IActionResult Register(UserModel userModel)
        {
            string userString = JsonConvert.SerializeObject(userModel);
            string data = HttpClientHelper.GetAll(HttpType.HttpPost, "/UserAPI/UserRegister", new { userModel = userString });
            if (Convert.ToInt32(data)>0)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }

        }

        //点击修改反填用户信息
        public IActionResult UserFill(int ids = 0)
        {
            ViewBag.xiala = ids;
            return View();
        }
        //反填方法
        public IActionResult Assignment(int id)
        {
            string data = HttpClientHelper.GetAll(HttpType.HttpGet, "/UserAPI/Assignment?id="+id);
            UserModel userData = JsonConvert.DeserializeObject<UserModel>(data);
            return Ok(new
            {
                data = userData
            });
        }

        ////修改
        //public int Upd(UserModel m)
        //{
        //    string sql = $"update UserModel set UserName='{m.UserName}',UserPwd= '{m.UserPwd}',UserSex= '{m.UserSex}', UserCard='{m.UserCard}',UserPhone= '{m.UserPhone}',UserNational= '{m.UserNational}',UserEmail= '{m.UserEmail}',UserMajor= '{m.UserMajor}',UserJoinInDate= '{m.UserJoinInDate}',UserDimissionDate= '{m.UserDimissionDate}',UserDimissionCause= '{m.UserDimissionCause}',UserDeleteMark= {m.UserDeleteMark},UserIsAdmin= {m.UserIsAdmin},UserCreateName= '{m.UserCreateName}', UserCreateDate='{m.UserCreateDate}' where UserId ={m.UserId})";
        //    int query = _userRepositroy.ZSG(sql);
        //    return query;
        //}
    }
}
