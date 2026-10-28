using Microsoft.AspNetCore.Mvc;
using Newtouch.CIS.API.Envelope;
using Newtouch.CIS.API.Requests;
using Newtouch.CIS.API.Services;
using Newtouch.Domain.IDomainServices;

namespace Newtouch.CIS.API.Controllers
{
    /// <summary>
    /// Port của PatientTreatmentInfoController (api/PatientSeekingInfo).
    /// SỬA BUG GỐC: _treatmentDmnService ở bản Framework không bao giờ được gán
    /// (ctor chỉ nhận IComponentContext) — bản Core inject qua DI nên action tra
    /// cứu chạy thật. Lưu ý: tên controller là PatientTreatmentInfo nhưng route
    /// gốc là api/PatientSeekingInfo — giữ nguyên route gốc.
    /// </summary>
    [ApiController]
    [Route("api/PatientSeekingInfo")]
    public class PatientTreatmentInfoController : ApiControllerBase
    {
        private readonly ITreatmentDmnService _treatmentDmnService;

        public PatientTreatmentInfoController(TokenStore tokenStore, ITreatmentDmnService treatmentDmnService)
            : base(tokenStore)
        {
            _treatmentDmnService = treatmentDmnService;
        }

        /// <summary>Tra cứu thông tin khám bệnh của bệnh nhân.</summary>
        [HttpPost("PatientTreatmentInfoQuery")]
        [DefaultAuthorize]
        public IActionResult PatientTreatmentInfoQuery([FromBody] PatientTreatmentInfoQueryRequestDTO request)
        {
            var response = CommonExecute<PatientTreatmentInfoQueryRequestDTO, DefaultResponse>((req, resp) =>
            {
                resp.data = _treatmentDmnService.SelectTreatmentEntities(req.mzh, req.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            }, request);
            return CreateResponse(response);
        }
    }
}
