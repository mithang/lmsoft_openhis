using FrameworkBase.MultiOrg.DmnService;
using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.PDS.Requset.ClinicTfOperate
{
    public class ClinicTfOperateRequest : RequestBase
    {
        /// <summary>
        /// Danh sách đơn thuốc
        /// </summary>
        public List<CfDetial> cfhs { get; set; }
    }

    /// <summary>
    /// Thông tin đơn trả phí
    /// </summary>
    public class CfDetial
    {
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
    }
}
