using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest.UA
{
    /// <summary>
    /// Đổi mã truy cập lấy thông tin danh tính người dùng
    /// </summary>
    public class UAGetUserInfoRequest : RequestBase
    {
        /// <summary>
        /// Mã truy cập
        /// </summary>
        public string access_token { get; set; }
    }
}
