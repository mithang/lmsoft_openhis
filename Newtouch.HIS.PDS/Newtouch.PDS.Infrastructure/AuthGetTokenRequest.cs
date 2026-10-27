using FrameworkBase.MultiOrg.DmnService;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset
{
    /// <summary>
    /// Bằng thông tin quan trọng（Tên người dùng、Mật khẩu v.v.）Đổi lấy mã truy cập
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
        /// Mật khẩu dạng md5 (như web login) — bổ sung giai đoạn GetToken chạy
        /// thật; identity lấy từ kết quả CheckPwd, các trường UserId/
        /// OrganizeId/TopOrganizeId do client gửi không còn dùng để dựng
        /// identity. KHÔNG BAO GIỜ log trường này.
        /// </summary>
        public string Password { get; set; }
    }
}
