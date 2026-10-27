using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO
{
    /// <summary>
    /// công cộnghưởngứngbáovăn
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// hưởngứngthểđầubộ
        /// </summary>
        public Header Header { get; set; }

        /// <summary>
        /// hưởngứngthểkếtnếu
        /// </summary>
        public Result Result { get; set; }
    }
}