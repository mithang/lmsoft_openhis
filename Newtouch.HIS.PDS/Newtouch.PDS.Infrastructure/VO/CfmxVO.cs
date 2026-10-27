using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// Chi tiết đơn thuốc
    /// </summary>
    public class CfmxVO
    {
        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        public string cfnm { get; set; }
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
        /// Số lượng（Đơn vị bộ phận）
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Số lượng+Đơn vị
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// Đơn vị bộ phận
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Đơn giá（Đơn vị bộ phận）
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
        /// Dặn dò của bác sĩ
        /// </summary>
        public string yszt { get; set; }

        /// <summary>
        /// Tên bác sĩ
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// Thời gian thu phí
        /// </summary>
        public DateTime sfsj { get; set; }

        /// <summary>
        /// Nhóm thuốc
        /// </summary>
        public string czh { get; set; }
        /// <summary>
        /// Mã BHYT quốc gia
        /// </summary>
        public string gjybdm { get; set; }
        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }
        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }
    }
}