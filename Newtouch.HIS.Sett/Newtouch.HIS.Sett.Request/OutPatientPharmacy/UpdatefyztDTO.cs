using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    /// <summary>
    /// Mô tả yêu cầu cập nhật dấu hiệu phát thuốc
    /// </summary>
    public class UpdatefyztRequest : RequestBase
    {
        /// <summary>
        /// Mã nội bộ đ ơn thuốc
        /// </summary>
        public string cfnm { get; set; }

        /// <summary>
        /// Nhân sự cấp phát thuốc 
        /// </summary>
        public string user_code { get; set; }

        /// <summary>
        /// Cửa sổ nhận thuốc
        /// </summary>
        public string lyck { get; set; }
    }
}
