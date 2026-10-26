using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset
{
    /// <summary>
    /// Đổi thông tin quan trọng (tên đăng nhập, mật khẩu, v.v.) lấy mã truy cập
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
