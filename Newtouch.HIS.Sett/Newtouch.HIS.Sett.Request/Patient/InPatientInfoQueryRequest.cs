using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Patient
{
    /// <summary>
    /// Thông tin bệnh nhân nhập viện
    /// </summary>
    public class InPatientInfoQueryRequest : BookingReqBaseDto
    {
        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Số CMND
        /// </summary>
        public string sfzh { get; set; }

        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// ID tổ chức
        /// </summary>
        //public string OrganizeId { get; set; }
    }
}
