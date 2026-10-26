using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    /// <summary>
    /// Đ ã quyết toán tính chi tiết đ ơn thuốctra cứugiao diện bản tin yêu cầu
    /// </summary>
    public class OutpatientSettledRpQueryRequestDTO: RequestBase
    {

        /// <summary>
        /// Quyết toán thời gian bắt đ ầu
        /// </summary>
        public DateTime kssj { get; set; }

        /// <summary>
        /// Quyết toánThời gian kết thúc
        /// </summary>
        public DateTime jssj { get; set; }

        /// <summary>
        /// Số hóa đ ơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// cách dùng 
        /// </summary>
        public string yfCode { get; set; }

    }
}
