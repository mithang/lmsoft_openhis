using System;

namespace Newtouch.Domain.DTO
{
    /// <summary>
    /// đã quyết toán Đơn thuốc chi tiết Tra cứu giao diện Quay lại báo văn
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
        /// Hóa đơn số
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Đơn thuốc trong mã
        /// </summary>
        public int cfnm { get; set; }

        /// <summary>
        /// Đơn thuốc chi tiết ID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// quyết toán trong mã
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// quyết toán chi tiết trong mã
        /// </summary>
        public int jsmxnm { get; set; }

        /// <summary>
        /// Thu phí Thời gian
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
        /// dùng lượng
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// dùng lượng Đơn vị
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// dùng lượng+dùng lượng Đơn vị
        /// </summary>
        public string ylstr { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Số lượng Đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Số lượng+Số lượng Đơn vị
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
        /// Tần suất tương ứng lần số
        /// </summary>
        public int? zxcs { get; set; }
    }
}
