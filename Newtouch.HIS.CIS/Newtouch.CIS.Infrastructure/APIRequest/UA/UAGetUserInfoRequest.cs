using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest.UA
{
    /// <summary>
    /// lấy Truy cập mã thông báo đổi lấy Người dùng thân phần thông tin
    /// </summary>
    public class UAGetUserInfoRequest : RequestBase
    {
        /// <summary>
        /// Truy cập mã thông báo
        /// </summary>
        public string access_token { get; set; }
    }
}
