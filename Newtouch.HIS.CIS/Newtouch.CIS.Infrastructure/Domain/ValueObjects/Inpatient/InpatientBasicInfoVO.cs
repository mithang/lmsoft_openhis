using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class InpatientBasicInfoVO
    {
        public string zyh { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string nl { get; set; }
        public string zhcode { get; set; }
        public string ryrq { get; set; }
        public string cyrq { get; set; }
        public int? zybz { get; set; }
        /// <summary>
        /// Tài khoản Tính chất
        /// </summary>
        public int? zhxz { get; set; }
        /// <summary>
        /// tạm ứng kim dư mức
        /// </summary>
        public string zhye { get; set; }
        /// <summary>
        /// phát sinh của tổng Chi phí
        /// </summary>
        public decimal? zje { get; set; }
        /// <summary>
        /// đã kết Chi phí
        /// </summary>
        public decimal? yjfy { get; set; }
        /// <summary>
        /// tạm ứng kim báo cảnh báo mức
        /// </summary>
        public decimal? bje { get; set; }
        /// <summary>
        /// Bảo hiểm y tế hiện kim chi trả
        /// </summary>
        public decimal? ybxjzf { get; set; }
        public List<InpatientBedUseInfoVO> patTransList { get; set; }

    }
}
