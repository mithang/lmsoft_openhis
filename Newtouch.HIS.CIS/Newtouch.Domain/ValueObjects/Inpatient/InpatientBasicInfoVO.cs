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
        /// Tính chất tài khoản
        /// </summary>
        public int? zhxz { get; set; }
        /// <summary>
        /// Số dư tiền tạm ứng
        /// </summary>
        public string zhye { get; set; }
        /// <summary>
        /// Tổng chi phí phát sinh
        /// </summary>
        public decimal? zje { get; set; }
        /// <summary>
        /// Chi phí đã quyết toán
        /// </summary>
        public decimal? yjfy { get; set; }
        /// <summary>
        /// Mức cảnh báo tiền tạm ứng
        /// </summary>
        public decimal? bje { get; set; }
        /// <summary>
        /// Thanh toán tiền mặt BHYT
        /// </summary>
        public decimal? ybxjzf { get; set; }
        public List<InpatientBedUseInfoVO> patTransList { get; set; }

    }
}
