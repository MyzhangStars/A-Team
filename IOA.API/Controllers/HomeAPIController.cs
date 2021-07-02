using IOA.Common;
using IOA.IRepository;
using IOA.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System;

namespace IOA.API.Controllers
{
    [ApiController]
    [Route("HomeAPI")]
    //设置跨域处理的 代理  如果在控制器内的所以都有对应的跨域限制
    /*[EnableCors("any")]*/  //如果在方法头则方法限制
    public class HomeAPIController : Controller
    {
        RedisHelper redisHelper = new RedisHelper();
        //实例化
        public readonly IHomeRepositroy _ihomeRepositroy;
        //private readonly Logger _logger;
        private readonly ILogger<HomeAPIController> _Logger;
        public HomeAPIController(IHomeRepositroy ihomeRepositroy, ILogger<HomeAPIController> Logger)
        {
            _ihomeRepositroy = ihomeRepositroy;
            //_logger = logger;
            _Logger = Logger;
        }
        //添加Redis缓存

        //[HttpPost]
        //  public void AddRedis(RedisType redisType,string key,string value)
        //{
        //    //获取枚举类型对应的值
        //    string redisKey = redisType.ToString();
        //    //拼接Redis的Key名
        //    redisKey += ("_" + key);
        //    //写入Redis缓存
        //    redisHelper.CacheRedis.StringSet(redisKey, value);
        //}

        //public object Get(RedisType redisType,string key,string value)
        //{
        //    //value 转换成 JsonConvert.SerializeObject字符串
        //    AddRedis(redisType, key, value);

        ////获取枚举类型对应的值
        //string redisKey = Enum.GetName(typeof(RedisType), redisType);
        ////拼接Redis的Key名
        //redisKey += ("_" + key);
        //    //根据RedisKey读取Redis缓存数据
        //    var studentRedis = redisHelper.CacheRedis.StringGet(redisKey);

        ////删除缓存
        //redisHelper.CacheRedis.KeyDelete("key");

        //    //过期时间 以秒为单位 
        //    redisHelper.CacheRedis.KeyExpire("key", System.DateTime.Now.AddSeconds(20));  //20秒


        //    //判断是否从Redis中取到数据
        //    if (studentRedis == "")
        //    {
        //        //Redis没有，在数据库中查找
        //    }
        //    try
        //    {
        //        //转换为集合
        //        List<T> listStudent = JsonConvert.DeserializeObject<List<T>>(studentRedis);

        //        return JsonConvert.SerializeObject(listStudent);
        //    }
        //    catch (Exception)
        //    {
        //        //转换为对象
        //        T singleStudent = JsonConvert.DeserializeObject<T>(studentRedis);
        //        return JsonConvert.SerializeObject(singleStudent);
        //    }
        //}


        //[Route(nameof(Get))]
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    //_logger.Info("普通信息日志------");
        //    //_logger.Debug("调试日志--------");
        //    //_logger.Error("错误日志---------");
        //    //_logger.Fatal("异常日志---------");
        //    //_logger.Warn("警告日志---------");
        //    //_logger.Trace("跟踪日志--------");
        //    //_logger.Log(NLog.LogLevel.Warn, "Log日志---------");

        //    _Logger.LogInformation("你访问了首页");
        //    _Logger.LogWarning("警告信息");
        //    _Logger.LogError("错误信息");
        //    return new string[] { "value1", "value2" };

        //}

        #region //点击全部应用 显示左边
        [Route(nameof(Index))]
        [HttpGet]
        public string Index(int parentID, int userId = 1)
        {

      
        //int userId = Convert.ToInt32(HttpContext.Session.GetString("userID"));
        //ViewBag.userName = HttpContext.Session.GetString("userName");

        //获取全部菜单
        List<MenuModel> leftNext = _ihomeRepositroy.Show("select * from MenuModel");
            //获取左侧菜单栏
            List<MenuModel> left = _ihomeRepositroy.leftData(parentID, userId);
            StringBuilder leftData = new StringBuilder();
            //一级
            foreach (var item in left)
            {
                leftData.Append("<li data-name = 'home' class='layui-nav-item layui-nav-itemed'>");
                leftData.Append($"<a href = 'javascript:;'  lay-direction = '2' >");
                leftData.Append($"<cite>{item.MenuName}</cite></a>");
                //二级
                foreach (var itemNext in leftNext)
                {
                    if (itemNext.MenuParentID.Equals(item.MenuId))
                    {
                        leftData.Append("<dl class='layui-nav-child'>");
                        leftData.Append("<dd class='layui-nav-itemed'>");
                        leftData.Append($"<a href ='javascript:;'>{itemNext.MenuName}</a>");
                        //三级
                        foreach (var itemNext2 in leftNext)
                        {
                            if (itemNext2.MenuParentID.Equals(itemNext.MenuId))
                            {
                                leftData.Append("<dl class='layui-nav-child'>");
                                if (itemNext2.MenuLink == null || itemNext2.MenuLink == "")
                                {
                                    itemNext2.MenuLink = "javascript:;";
                                }
                                leftData.Append($"<dd><a href='{itemNext2.MenuLink}' target='ifr'>{itemNext2.MenuName}</a>");
                                //四级
                                foreach (var itemNext3 in leftNext)
                                {
                                    if (itemNext3.MenuParentID.Equals(itemNext2.MenuId))
                                    {
                                        leftData.Append("<dl class='layui-nav-child'>");
                                        if (itemNext3.MenuLink == null || itemNext3.MenuLink == "")
                                        {
                                            itemNext3.MenuLink = "javascript:;";
                                        }
                                        leftData.Append($"<dd><a href='{itemNext3.MenuLink}' target='ifr'>{itemNext3.MenuName}</a>");
                                        leftData.Append("</dd></dl>");
                                    }

                                }
                                leftData.Append("</dd></dl>");

                            }
                        }
                        leftData.Append("</dd></dl>");
                    }
                }
                leftData.Append("</li>");
            }

            return leftData.ToString();

        }
        #endregion



    }
}
