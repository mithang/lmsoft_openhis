using Microsoft.AspNetCore.Mvc;
using Newtouch.OR.API.Envelope;
using Newtouch.OR.API.Requests;
using Newtouch.OR.API.Services;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;

namespace Newtouch.OR.API.Controllers
{
    /// <summary>
    /// Port của ORPatientController (api/ORPatient). [DefaultAuthorize] bắt buộc identity:
    /// không token → TOKEN_IS_REQUIRED, token không resolve được → TOKEN_IS_EXPIRED.
    /// SỬA BUG GỐC: _opRegisterDmnService ở bản Framework cũng không được gán
    /// (cùng mẫu ctor chỉ nhận IComponentContext) — bản Core inject qua DI.
    /// </summary>
    [ApiController]
    [Route("api/ORPatient")]
    [DefaultAuthorize]
    public class ORPatientController : ApiControllerBase
    {
        private readonly IOPRegisterDmnService _opRegisterDmnService;

        public ORPatientController(TokenStore tokenStore, IOPRegisterDmnService opRegisterDmnService)
            : base(tokenStore)
        {
            _opRegisterDmnService = opRegisterDmnService;
        }

        /// <summary>Danh sách đăng ký phẫu thuật của bệnh nhân (bảng OR_Registration).</summary>
        [HttpPost("ORPatList")]
        public IActionResult ORPatList([FromBody] QueryRequest par)
        {
            var response = CommonExecute<QueryRequest, DefaultResponse>((req, resp) =>
            {
                var list = _opRegisterDmnService.GetPatOpRegistList(req.ksrq, req.jsrq, req.zyh, req.djzt, req.OrganizeId);
                resp.data = list;
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(response);
        }
    }
}
