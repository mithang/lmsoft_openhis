using Microsoft.AspNetCore.Mvc;
using Newtouch.CIS.API.Envelope;
using Newtouch.CIS.API.Requests;
using Newtouch.CIS.API.Services;
using Newtouch.Domain.IDomainServices;

namespace Newtouch.CIS.API.Controllers
{
    /// <summary>
    /// Port của ReviewController (api/Review).
    /// SỬA BUG GỐC: _doctorserviceDmnService ở bản Framework không bao giờ được
    /// gán (ctor chỉ nhận IComponentContext) — bản Core inject qua DI nên
    /// GetBaShData chạy thật (gọi duyệt bệnh án thông minh; tên method gốc
    /// GetBashData được giữ nguyên).
    /// </summary>
    [ApiController]
    [Route("api/Review")]
    [DefaultAuthorize]
    public class ReviewController : ApiControllerBase
    {
        private readonly IDoctorserviceDmnService _doctorserviceDmnService;

        public ReviewController(TokenStore tokenStore, IDoctorserviceDmnService doctorserviceDmnService)
            : base(tokenStore)
        {
            _doctorserviceDmnService = doctorserviceDmnService;
        }

        /// <summary>Gọi duyệt bệnh án thông minh.</summary>
        [HttpPost("GetBaShData")]
        public IActionResult GetBaShData([FromBody] ReviewRequest par)
        {
            var response = CommonExecute<ReviewRequest, DefaultResponse>((req, resp) =>
            {
                _doctorserviceDmnService.GetBashData(par.orgId, par.zyh, par.rygh, par.username, par.GetMAC);
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(response);
        }
    }
}
