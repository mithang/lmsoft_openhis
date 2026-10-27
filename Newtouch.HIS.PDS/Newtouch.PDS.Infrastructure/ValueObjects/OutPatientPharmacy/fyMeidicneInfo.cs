using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thông tin thuốc phát thuốc
    /// </summary>
    public class fyMeidicneInfo
    {
        /// <summary>
        /// Số lô chi tiết đơn thuốcId
        /// </summary>
        public string cfmxphId { get; set; }

        /// <summary>
        /// Mã nội bộ đơn thuốc
        /// </summary>
        public string cfnm { get; set; }
        
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }
        
        /// <summary>
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Số lượng đơn vị
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// Liều lượng
        /// </summary>
        public decimal jl { get; set; }

        /// <summary>
        /// Đơn vị liều lượng
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// Cách dùng
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// Dặn dò của bác sĩ(Ghi chú)
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// Nhóm thuốc
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// 1-Phát thuốc 2-Trả thuốc
        /// </summary>
        public string operateType { get; set; }

        /// <summary>
        /// Thời gian thao tác
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
