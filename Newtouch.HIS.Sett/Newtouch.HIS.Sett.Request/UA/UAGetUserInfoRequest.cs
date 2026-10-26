using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request.UA
{
    /// <summary>
    /// 以 truy cập 令牌 đ ổi lấy ngư ời dùng thân phận thông tin
    /// </summary>
    public class UAGetUserInfoRequest : RequestBase
    {
        /// <summary>
        /// truy cập 令牌
        /// </summary>
        public string access_token { get; set; }
    }
}
