using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    public class NB_INTERFACE_MZJS_REQ
    {
        /// <summary>
        /// Mã thẻ
        /// </summary>
        public string kbh { get; set; }

        /// <summary>
        /// Số giao dịch ngoại trú(Số giao dịch duy nhất đại diện cho một hóa đơn thu phí ngoại trú))
        /// </summary>
        public string mzlsh { get; set; }

        /// <summary>
        /// Mã bệnh viện
        /// </summary>
        public string yydm { get; set; }

        /// <summary>
        /// Mã nhân viên thao tác
        /// </summary>
        public string czydm { get; set; }

        /// <summary>
        /// Tên nhân viên thao tác
        /// </summary>
        public string czymc { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }
    }
}
