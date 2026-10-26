namespace Newtouch.CIS.Proxy.CMMPlatform.DTO
{
    /// <summary>
    /// Bản tin phản hồi chung
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// Tiêu đề thân phản hồi
        /// </summary>
        public Header Header { get; set; }

        /// <summary>
        /// Kết quả thân phản hồi
        /// </summary>
        public Result Result { get; set; }
    }
}