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

        /// <summary>
        /// Mật khẩu dạng md5 (như web login) — bổ sung để thống nhất shape
        /// AuthGetTokenRequest với các API khác (biến thể này hiện không được
        /// controller CIS.API nào dùng; controller dùng biến thể trong
        /// Newtouch.CIS.API.Requests). KHÔNG BAO GIỜ log trường này.
        /// </summary>
        public string Password { get; set; }
    }

}
