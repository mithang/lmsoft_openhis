using Newtouch.Core.Common;
using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Patient
{
    /// <summary>
    /// Ngoại trú khám bệnhdanh sách
    /// </summary>
    public class OutPatientConsultationQueryRequest : BookingReqBaseDto
    {
        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Số CMND
        /// </summary>
        public string sfzh { get; set; }
        
    }
}
