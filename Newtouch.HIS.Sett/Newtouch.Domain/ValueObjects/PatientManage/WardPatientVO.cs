using System;

namespace Newtouch.HIS.Domain.ValueObjects.PatientManage
{
    /// <summary>
    /// Ghi sổ chờ thực hiện Bệnh nhân VO
    /// </summary>
    public class PendingExecutionPatientVO
    {
        /// <summary>
        /// Khu bệnhCode
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// Bệnh nhânid
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Số lần còn lại của BHYT
        /// </summary>
        public int? ybsycs { get; set; }
        /// <summary>
        /// Trạng thái thực hiện
        /// </summary>
        public int? zxzt { get; set; }
        public string blh { get; set; }
        public string py { get; set; }
        public DateTime? ghrq { get; set; }
        public DateTime? CreateTime { get; set; }

    }
}
