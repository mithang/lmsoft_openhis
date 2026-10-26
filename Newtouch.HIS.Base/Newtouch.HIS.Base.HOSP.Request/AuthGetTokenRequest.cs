using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Base.HOSP.Request
{
    /// <summary>
    /// Dùng thông tin quan trọng (tên đăng nhập, mật khẩu, v.v.) để đổi lấy access token
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
