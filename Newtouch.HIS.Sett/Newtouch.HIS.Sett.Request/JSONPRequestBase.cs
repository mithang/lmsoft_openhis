using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class JSONPRequestBase : RequestBase
    {
        /// <summary>
        /// jsonp lần 调 hàm 
        /// </summary>
        //[Required] // hỗ trợ jsonp
        public string jsonpCallback { get; set; }
    }
}
