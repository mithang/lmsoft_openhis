using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thông tin đăng ký khám ngoại trú
    /// </summary>
    [NotMapped]
    public class OutPatientRegInfoVO : OutpatientRegistEntity
    {
        public string ksmc { get; set; }
        public string ysmc { get; set; }
        public string brxzmc { get; set; }
        public string py { get; set; }

    }

    /// <summary>
    /// Thông tin đăng ký khám ngoại trú
    /// </summary>
    public class OutPatientRegVO
    {
        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Có phải là quyết toán trước khi nợ phí hay không
        /// </summary>
        public bool iqQfyj { get; set; }

        /// <summary>
        /// Chỉ mục chính bệnh nhân
        /// </summary>
        public int? patid { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Mã cá nhân Bảo hiểm nông thôn mới
        /// </summary>
        public string xnhgrbm { get; set; }
    }
}
