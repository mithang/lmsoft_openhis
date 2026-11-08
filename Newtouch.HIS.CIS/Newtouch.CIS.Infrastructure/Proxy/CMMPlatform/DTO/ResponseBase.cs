using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO
{
    /// <summary>
    /// công cộng hưởng ứng báo văn
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// hưởng ứng thể đầu bộ
        /// </summary>
        public Header Header { get; set; }

        /// <summary>
        /// hưởng ứng thể kết nếu
        /// </summary>
        public Result Result { get; set; }
    }
}