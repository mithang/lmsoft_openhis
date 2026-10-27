using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest
{
    /// <summary>
    /// lấy关phímthông tin（Người dùngtên、Mật khẩuđợi）đổilấyTruy cậpmã thông báo
    /// </summary>
    public class AuthGetTokenRequest : RequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }
    }

}
