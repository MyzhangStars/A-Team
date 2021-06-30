//using StackExchange.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Net.Http.Json;

namespace IOA.Common
{
    public class RedisHelper : IDisposable
    {
        //连接字符串
        public string _connectionString= ConfigHelperRedis._connectionString;
        //实例名称
        private string _instanceName=ConfigHelperRedis._instanceName;
        //默认数据库
        private int _defaultDB=ConfigHelperRedis._db;
        private ConcurrentDictionary<string, ConnectionMultiplexer> _connections;
        public RedisHelper(/*string connectionString, string instanceName, int defaultDB = 0*/)
        {
            //_connectionString = connectionString;
            //_instanceName = instanceName;
            //_defaultDB = defaultDB;
            _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        }

        /// <summary>
        /// 获取ConnectionMultiplexer
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetConnect()
        {
            return _connections.GetOrAdd(_instanceName, p => ConnectionMultiplexer.Connect(_connectionString));
        }


        /// <summary>
        /// 缓存数据库
        /// </summary>
        public IDatabase CacheRedis => GetConnect().GetDatabase(_defaultDB);

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datetime"></param>
        public  void SetExpire(string key, DateTime datetime)
        {
            CacheRedis.KeyExpire(key, datetime);
        }

        public IServer GetServer(string configName = null, int endPointsIndex = 0)
        {
            var confOption = ConfigurationOptions.Parse(_connectionString);
            return GetConnect().GetServer(confOption.EndPoints[endPointsIndex]);
        }

        public ISubscriber GetSubscriber(string configName = null)
        {
            return GetConnect().GetSubscriber();
        }

        public void Dispose()
        {
            if (_connections != null && _connections.Count > 0)
            {
                foreach (var item in _connections.Values)
                {
                    item.Close();
                }
            }
        }


    }
}
