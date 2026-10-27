using Microsoft.AspNetCore.Mvc;
using Newtouch.PDS.API.Envelope;
using Newtouch.PDS.API.Services;
using Newtouch.PDS.Requset.DeptPharmacyDrugs;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// Port của api/DeptPharmacyDrugs (thuốc nhà thuốc của bộ phận).
    /// Hành vi gốc được giữ nguyên vẹn, kể cả chuỗi trả về hard-code của action duy nhất
    /// (bản gốc cũng chỉ trả chuỗi đó, không truy vấn dữ liệu) — không tự bịa nghiệp vụ.
    /// </summary>
    [ApiController]
    [Route("api/DeptPharmacyDrugs")]
    public class DeptPharmacyDrugsController : ApiControllerBase
    {
        public DeptPharmacyDrugsController(TokenStore tokenStore)
            : base(tokenStore)
        {
        }

        [HttpPost("GetDeptPharmacyDrugs")]
        public IActionResult GetDeptPharmacyDrugs([FromBody] DeptPharmacyDrugsRequset par)
        {
            var response = CommonExecute<DeptPharmacyDrugsRequset, DefaultResponse>((req, resp) =>
            {
                resp.data = " GetDeptPharmacyDrugs 123123123";
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }
    }
}
