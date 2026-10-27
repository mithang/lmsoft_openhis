using System;

namespace Newtouch.Domain.DTO
{
    /// <summary>
    /// đãquyết toánĐơn thuốcchi tiếtTra cứugiao diệnQuay lạibáovăn
    /// </summary>
    public class OutpatientSettledRpQueryResponseDTO
    {
        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Hóa đơnsố
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Đơn thuốctrongmã
        /// </summary>
        public int cfnm { get; set; }

        /// <summary>
        /// Đơn thuốcchi tiếtID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// quyết toántrongmã
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// quyết toánchi tiếttrongmã
        /// </summary>
        public int jsmxnm { get; set; }

        /// <summary>
        /// Thu phíThời gian
        /// </summary>
        public DateTime sfsj { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// dùnglượng
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// dùnglượngĐơn vị
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// dùnglượng+dùnglượngĐơn vị
        /// </summary>
        public string ylstr { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Số lượngĐơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Số lượng+Số lượngĐơn vị
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// Liều lượng
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// Đơn vị liều
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// Liều lượng+Đơn vị liều
        /// </summary>
        public string jlstr { get; set; }
        public string yfCode { get; set; }
        public string pcCode { get; set; }
        public int? zcs { get; set; }
        /// <summary>
        /// Tần suấttương ứng lầnsố
        /// </summary>
        public int? zxcs { get; set; }
    }
}
