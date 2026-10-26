using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.YibaoInterfaceManage
{
    public class YibaoDataVo
    {
        /// <summary>
        /// Khám bệnhID
        /// </summary>
        public string mdtrt_id { get; set; }
        /// <summary>
        /// Quyết toánID
        /// </summary>
        public string setl_id { get; set; }
        public string psn_no { get; set; }
        public string psn_name { get; set; }
        public DateTime? setl_time { get; set; }
        public string zymzh { get; set; }
        public decimal? medfee_sumamt { get; set; }
        /// <summary>
        /// Bản tin tham số đầu vào giao dịchID Dùng cho giao dịch đảo
        /// </summary>
        public string medins_setl_id { get; set; }
        public string cbdbm { get; set; }
        /// <summary>
        /// Mã giao dịch
        /// </summary>
        public string infno { get; set; }
        /// <summary>
        /// Thông tin lỗi giao dịch
        /// </summary>
        public string errormsg { get; set; }
        /// <summary>
        /// Tài khoản một bên
        /// </summary>
        public string dbz { get; set; }
        public int? jsnm { get; set; }

        public decimal? xjzf { get; set; }
    }
}
