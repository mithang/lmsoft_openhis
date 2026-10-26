using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    public class NB_INTERFACE_MZSS_REP
    {
        /// <summary>
        /// Tổng số tiền hóa đơn
        /// </summary>
        public string fpze { get; set; }

        /// <summary>
        /// Tổng số tiền được thanh toán
        /// </summary>
        public string kbze { get; set; }

        /// <summary>
        /// Số tiền tự chi trả
        /// </summary>
        public string zfje { get; set; }

        /// <summary>
        /// Tổng số tiền tự trả
        /// </summary>
        public string zfze { get; set; }

        /// <summary>
        /// Tổng số tiền bồi thường một lần
        /// </summary>
        public string zbcze { get; set; }

        /// <summary>
        /// Tổng số tiền được thanh toán lần hai
        /// </summary>
        public string qkbze { get; set; }

        /// <summary>
        /// Tổng số tiền bồi thường lần hai
        /// </summary>
        public string qbcze { get; set; }

        /// <summary>
        /// Tổng số tiền cứu trợ
        /// </summary>
        public string jzze { get; set; }

        /// <summary>
        /// Số tiền cứu trợ dân chính
        /// </summary>
        public string mzjzje { get; set; }

        /// <summary>
        /// Tổng số tiền bồi thường
        /// </summary>
        public string bcze { get; set; }

    }
}
