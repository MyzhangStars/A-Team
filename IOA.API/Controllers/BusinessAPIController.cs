using IOA.IRepository;
using IOA.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOA.API.Controllers
{
    [ApiController]
    [Route("BusinessAPI")]
    public class BusinessAPIController : Controller
    {
        public readonly IBusinessRepositroy _ibusinessRepositroy;
        public BusinessAPIController(IBusinessRepositroy businessRepositroy)
        {
            _ibusinessRepositroy = businessRepositroy;
        }
        [Route(nameof(Index))]
        public object Index(BusinessModel b)
        {
            string sql = $"update BusinessModel set BusinessLogo='q', BusinessName='{b.BusinessName}',BusinessEngName='{b.BusinessEngName}',BusinessType='{b.BusinessType}',BusinessSite='{b.BusinessSite}',BusinessCoding='{b.BusinessCoding}',BusinessPhone='{b.BusinessPhone}',BusinessEmail='{b.BusinessEmail}',BusinessFax='{b.BusinessFax}',BusinessWebsite='{b.BusinessWebsite}',BusinessIndustry='{b.BusinessIndustry}',BusinessLinkMan='{b.BusinessLinkMan}',BusinessLinkWay='{b.BusinessLinkWay}' where BusinessId=1";
            int i = _ibusinessRepositroy.ZSG(sql,null);
            return i;
        }
    }
}
