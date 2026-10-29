using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.API.Models;
using Newtouch.Sett.API.Services;

namespace Newtouch.Sett.API.Controllers
{
    /// <summary>
    /// Nhóm D (T-24): api/Patient/* — route mà CIS gọi qua SiteSettAPIHelper.
    /// Không dùng [ApiController] (như 3 nhóm kia) để body lỗi vẫn trả
    /// envelope HTTP 200 thay vì 400. Không yêu cầu xác thực: cùng mẫu
    /// tokenless với Booking/PatientAnon/OrderCenter (host không có auth
    /// middleware; CIS có thể đính kèm Bearer app token, được bỏ qua).
    /// </summary>
    [Route("api/Patient")]
    public class PatientController : ControllerBase
    {
        private readonly PatientQueryService _svc;

        public PatientController(PatientQueryService svc) { _svc = svc; }

        [HttpPost("OutPatientRegistrationQuery")]
        public ResponseBaseOld OutPatientRegistrationQuery([FromBody] OutPatientRegistrationQueryRequest req)
        {
            if (req == null) return ResponseBaseOld.Fail("Request rỗng");
            try
            {
                return _svc.Query(req);
            }
            catch (Exception ex)
            {
                return ResponseBaseOld.Error(ex.Message);
            }
        }
    }
}
