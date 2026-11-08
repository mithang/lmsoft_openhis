
using Newtouch.HIS.API.Common;
namespace Newtouch.EMR.APIRequest
{
    /// <summary>
    /// Bằng thông tin quan trọng(Tên đăng nhập,mật khẩu, v.v.)Lấy token truy cập
    /// </summary>
    public class AuthGetTokenRequest : RequestBase
    {
        public string UserId { get; set; }

        public string Account { get; set; }

        // OrganizeId đã có trong RequestBase (EMR.Infrastructure/APIRequest/ApiCommon.cs).
        // F14: bổ sung UserId/TopOrganizeId để GetTokenbyProxy đọc được
        // (theo đúng shape HOSP/CIS/PDS) — GetToken không dùng 2 trường này.
        public string TopOrganizeId { get; set; }

        /// <summary>Mật khẩu dạng md5 (như web login), KHÔNG BAO GIỜ log.</summary>
        public string Password { get; set; }

        // FIX-2: TokenType nay lay tu RequestBase (khoi phuc theo goc
        // FrameworkBase.API); khong khai rieng nua de tranh hide warning.
    }
}
