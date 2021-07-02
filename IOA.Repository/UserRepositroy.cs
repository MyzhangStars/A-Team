using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOA.Model;
using IOA.Common;
using IOA.IRepository;
using Repositroy;

namespace IOA.Repository
{
    public class UserRepositroy : BaseRepositroy<UserModel>,IUserRepositroy
    {
    }
}
