using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Application.Interface;
using Newtouch.PDS.API.Envelope;
using Newtouch.PDS.API.Services;
using Newtouch.PDS.Requset;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// Port của api/Outpatient (quản lý ngoại trú).
    /// SỬA BUG GỐC: _patientBaseInfoApp ở bản Framework được khai báo nhưng không bao giờ
    /// được gán (ctor chỉ nhận IComponentContext, không resolve field — cùng mẫu lỗi như
    /// DefaultController của MRMS.API và BespeakRegisterApp của CIS) — gọi action gốc sẽ
    /// NullReference. Bản Core inject IPatientBaseInfoApp qua DI nên action chạy thật.
    /// </summary>
    [ApiController]
    [Route("api/Outpatient")]
    public class OutpatientController : ApiControllerBase
    {
        private readonly IPatientBaseInfoApp _patientBaseInfoApp;

        public OutpatientController(TokenStore tokenStore, IPatientBaseInfoApp patientBaseInfoApp)
            : base(tokenStore)
        {
            _patientBaseInfoApp = patientBaseInfoApp;
        }

        [HttpPost("SupplementPatientBaseInfo")]
        public IActionResult SupplementPatientBaseInfo([FromBody] SupplementPatientBaseInfoRequest par)
        {
            var response = CommonExecute<SupplementPatientBaseInfoRequest, DefaultResponse>((req, resp) =>
            {
                resp.data = _patientBaseInfoApp.SupplementMzPatientBaseInfo(req);
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }
    }
}
