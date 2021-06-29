using IOA.IRepository;
using IOA.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IOA.API.Controllers
{
    [ApiController]
    [Route("RoleAPI")]
    public class RoleAPIController : Controller
    {
        public readonly IRoleRepositroy _iroleRepositroy;

        public RoleAPIController(IRoleRepositroy roleRepositroy)
        {
            _iroleRepositroy = roleRepositroy;
        }
        [Route(nameof(Index))]
        [HttpGet]
        public List<RoleModel> Index()
        {
            List<RoleModel> data = _iroleRepositroy.Show("select RoleModel.RoleId,RoleModel.RoleName,COUNT(*) as RoleCount,RoleModel.RoleMsg from UserRole join UserModel on UserModel.UserId=UserRole.UserId join RoleModel on RoleModel.RoleId=UserRole.RoleId group by RoleModel.RoleId,RoleModel.RoleName,RoleModel.RoleMsg");
            return data;
        }

    }
}
