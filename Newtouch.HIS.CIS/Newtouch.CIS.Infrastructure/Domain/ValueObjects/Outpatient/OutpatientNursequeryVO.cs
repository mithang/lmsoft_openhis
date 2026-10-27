using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class OutpatientNursequeryVO
    {
        /// <summary>
        /// Đơn thuốcchi tiếtid
        /// </summary>
        public string cfmxId { get; set; }
        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// Bệnh nhânTên
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Bệnh nhânGiới tính 1:Nam 2：Nữ
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypcode { get; set; }
        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// Số nhóm
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// mởlậpThời gian
        /// </summary>
        public DateTime? kssj { get; set; }

        /// <summary>
        /// Đơn thuốcchi tiếtnội dung
        /// </summary>
        public string cfmxnr { get; set; }

        /// <summary>
        /// Cách dùngMã
        /// </summary>
        public string yfcode { get; set; }
        /// <summary>
        /// Tên cách dùng
        /// </summary>
        public string ypyfmc { get; set; }
        public string lrjg { get; set; }
        public string cfid { get; set; }
        public string cfh { get; set; }
        public int? cflx { get; set; }
        public string barcode { get; set; }
        public string yscode { get; set; }
        public string ysmc { get; set; }
        public string xb { get; set; }
        public string CreatorName { get; set; }
        public DateTime? CreateTime { get; set; }
        public string LastModifierName { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string cancel { get; set; }
        public string iscancel { get; set; }
        public string gmxxid { get; set; }
        public decimal je { get; set; }
    }
}
