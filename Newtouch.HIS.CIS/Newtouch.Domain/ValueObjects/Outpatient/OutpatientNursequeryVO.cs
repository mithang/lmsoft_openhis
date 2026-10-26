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
        /// Chi tiết đơn thuốcid
        /// </summary>
        public string cfmxId { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Giới tính bệnh nhân 1:Nam 2:Nữ
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
        /// Thời gian kê
        /// </summary>
        public DateTime? kssj { get; set; }

        /// <summary>
        /// Nội dung chi tiết đơn thuốc
        /// </summary>
        public string cfmxnr { get; set; }

        /// <summary>
        /// Mã cách dùng
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
