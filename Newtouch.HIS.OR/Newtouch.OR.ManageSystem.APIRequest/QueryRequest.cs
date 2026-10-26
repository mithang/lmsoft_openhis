using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.HIS.API.Common;

namespace Newtouch.OR.ManageSystem.APIRequest
{
    public class QueryRequest: RequestBase
    {
        public string OrganizeId { get; set; }
        public string ksrq { get; set; }
        public string jsrq { get; set; }
        public string zyh { get; set; }
        /// <summary>
        /// 1 Chờ đăng ký 2Đã đăng ký 3 Hủy đăng ký
        /// </summary>
        public string djzt { get; set; }

    }
}
