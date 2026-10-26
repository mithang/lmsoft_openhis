namespace Newtouch.CIS.Proxy.CMMPlatform.DTO
{
    /// <summary>
    /// Thân yêu cầu nhận thông tin
    /// </summary>
    public class Receive
    {
        /// <summary>
        /// Mã tổ chức
        /// Bắt buộc
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// Số thứ tự khám bệnh
        /// Bắt buộc
        /// </summary>
        public string serialNo { get; set; }
    }
}