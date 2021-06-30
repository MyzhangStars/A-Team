using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOA.Common
{
  public static  class ConfigHelperRedis
    {
        private static string connectionString; //链接字符串
        public static string _connectionString//链接字符串
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        private static string instanceName; //实例化名称
        public static string _instanceName//实例化名称
        {
            get { return instanceName; }
            set { instanceName = value; }
        }

        private static int db; //默认数据库
        public static int _db//默认数据库
        {
            get { return db; }
            set { db = value; }
        }
    }
}
