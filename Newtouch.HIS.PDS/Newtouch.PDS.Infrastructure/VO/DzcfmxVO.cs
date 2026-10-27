using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// Chi tiết đơn thuốc
    /// </summary>
    public class DzcfmxVO
    {

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

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
        public decimal sl { get; set; }

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
        public string shr { get; set; }
        public string shsj { get; set; }
        public string pc { get; set; }
    }
    public class Input_2203A
    {
        public string mdtrt_id { get; set; }
        public string psn_no { get; set; }
        public string med_type { get; set; }
        public string birctrl_type { get; set; }
        public string birctrl_matn_date { get; set; }
        public string matn_type { get; set; }
        public string dise_codg { get; set; }
        public string dise_name { get; set; }
        public string insuplc_admdvs { get; set; }
    }
}