using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest.UA
{
    /// <summary>
    /// lấy Truy cậpmã thông báođổilấy Người dùngthânphầnthông tin
    /// </summary>
    public class UAGetUserInfoRequest : RequestBase
    {
        /// <summary>
        /// Truy cậpmã thông báo
        /// </summary>
        public string access_token { get; set; }
    }
}
