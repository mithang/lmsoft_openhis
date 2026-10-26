using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// Bằng thông tin quan trọng (tên người dùng, mật khẩu v.v.) đổi lấy mã truy cập
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
