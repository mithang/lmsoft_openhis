using Newtouch.HIS.Base.HOSP.API.Envelope;

namespace Newtouch.HIS.Base.HOSP.Request
{
    public class AuthGetTokenRequest : RequestBase
    {
        public string UserId { get; set; }
        public string Account { get; set; }
        public string OrganizeId { get; set; }
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
