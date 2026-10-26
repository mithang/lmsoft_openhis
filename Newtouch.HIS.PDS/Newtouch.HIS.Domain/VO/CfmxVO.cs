using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// chi tiết đơn thuốc
    /// </summary>
    public class CfmxVO
    {
        /// <summary>
        /// số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        public string cfnm { get; set; }
        public string ypCode { get; set; }
        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// số lượng (đơn vị bộ phận)
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// số lượng+đơn vị
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// đơn vị bộ phận
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// đơn giá (đơn vị bộ phận)
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// nhà sản xuất
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// liều dùng
        /// </summary>
        public decimal jl { get; set; }

        /// <summary>
        /// đơn vị liều dùng
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// cách dùng
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// lời dặn của bác sĩ
        /// </summary>
        public string yszt { get; set; }

        /// <summary>
        /// bác sĩ tên
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// thời gian thu phí
        /// </summary>
        public DateTime sfsj { get; set; }

        /// <summary>
        /// nhóm thuốc
        /// </summary>
        public string czh { get; set; }
        /// <summary>
        /// mã BHYT quốc gia
        /// </summary>
        public string gjybdm { get; set; }
        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }
        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }
    }
}