using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class JSONPRequestBase : RequestBase
    {
        /// <summary>
        /// Hàm gọi lại jsonp 
        /// </summary>
        //[Required] // hỗ trợ jsonp
        public string jsonpCallback { get; set; }
    }
}
