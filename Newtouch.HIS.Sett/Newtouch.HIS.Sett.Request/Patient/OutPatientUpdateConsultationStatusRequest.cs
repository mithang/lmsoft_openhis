using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Patient
{
    /// <summary>
    /// Cập nhật ngoại trú bệnh nhân(Đ ăng ký khám ngoại trú)khám bệnh trạng thái
    /// </summary>
    public class OutPatientUpdateConsultationStatusRequest : RequestBase
    {
        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        [Required]
        public string outpatientNo { get; set; }

        /// <summary>
        /// Khám bệnh dấu hiệu 
        /// </summary>
        [Required]
        public string jiuzhenbz { get; set; }

        /// <summary>
        /// 看诊bác sĩ
        /// </summary>
        public string jzys { get; set; }
    }
}
