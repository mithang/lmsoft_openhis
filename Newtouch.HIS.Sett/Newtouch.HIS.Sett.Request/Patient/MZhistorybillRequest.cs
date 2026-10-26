using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Patient
{
    public class MZhistorybillRequest : BookingReqBaseDto
    {
        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// Thời gian bắt đ ầu
        /// </summary>
        public DateTime kssj { get; set; }
        /// <summary>
        /// Thời gian kết thúc
        /// </summary>
        public DateTime jssj { get; set; }
    }

    public class MZhistorybillMXRequest : BookingReqBaseDto
    {
        public string jsnm { get; set; }
    }
}
