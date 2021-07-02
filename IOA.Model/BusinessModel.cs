using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOA.Model
{
    public class BusinessModel          //公司信息
    {
        public int BusinessId { get; set; }
        public string BusinessLogo { get; set; }
        public string BusinessName { get; set; }
        public string BusinessEngName { get; set; }
        public string BusinessType { get; set; }
        public string BusinessSite { get; set; }
        public string BusinessCoding { get; set; }
        public string BusinessPhone { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessFax { get; set; }
        public string BusinessWebsite { get; set; }
        public string BusinessIndustry { get; set; }
        public string BusinessLinkMan { get; set; }
        public string BusinessLinkWay { get; set; }
    }
}
