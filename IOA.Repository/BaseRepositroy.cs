using IRepositroy;
using System;
using System.Collections.Generic;
using IOA.Common;

namespace Repositroy
{
    public class BaseRepositroy<T> : IBaseRepositroy<T> where T : class, new()
    {
        //显示
        public List<T> Show(string sql, object param = null)
        {
            List<T> data = DapperHelper<T>.Query(sql, param);

            return data;
        }

        //增删改
        public int ZSG(string sql, object param = null)
        {
            int i = DapperHelper<T>.Execute(sql, param);
            return i;
        }
    }
}
