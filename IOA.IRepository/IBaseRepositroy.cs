using System;
using System.Collections.Generic;

namespace IRepositroy
{
    public interface IBaseRepositroy<T> where T:class,new()
    {
        //显示
        List<T> Show(string sql,object param=null);
        //增删改
        int ZSG(string sql, object param = null);
    }
}
