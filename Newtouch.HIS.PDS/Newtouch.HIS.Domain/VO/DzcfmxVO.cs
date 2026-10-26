using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// chi tiết đơn thuốc
    /// </summary>
    public class DzcfmxVO
    {

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

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
        public decimal sl { get; set; }

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