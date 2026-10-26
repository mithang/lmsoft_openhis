using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    /// <summary>
    /// Cập nhật dấu hiệu phát thuốc yêu cầu 阐述
    /// </summary>
    public class UpdatefyztRequest : RequestBase
    {
        /// <summary>
        /// Mã nội bộ đ ơn thuốc
        /// </summary>
        public string cfnm { get; set; }

        /// <summary>
        /// 配药 nhân sự 
        /// </summary>
        public string user_code { get; set; }

        /// <summary>
        /// 领药窗口
        /// </summary>
        public string lyck { get; set; }
    }
}
