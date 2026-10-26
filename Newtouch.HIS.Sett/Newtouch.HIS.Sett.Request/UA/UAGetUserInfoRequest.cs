using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request.UA
{
    /// <summary>
    /// Bằng mã truy cập đổi lấy thông tin thân phận người dùng
    /// </summary>
    public class UAGetUserInfoRequest : RequestBase
    {
        /// <summary>
        /// Mã truy cập
        /// </summary>
        public string access_token { get; set; }
    }
}
