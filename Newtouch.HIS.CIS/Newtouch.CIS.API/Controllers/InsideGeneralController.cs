using Microsoft.AspNetCore.Mvc;
using Newtouch.Application.Interface;
using Newtouch.CIS.API.Envelope;
using Newtouch.CIS.API.Requests;
using Newtouch.CIS.API.Services;

namespace Newtouch.CIS.API.Controllers
{
    /// <summary>
    /// Port của InsideGeneralController (api/InsideGeneral) — cổng gọi tổng quát
    /// từ MQ. SỬA BUG GỐC: _insideGeneralApp ở bản Framework không bao giờ được
    /// gán (ctor chỉ nhận IComponentContext) — bản Core inject qua DI nên
    /// MqGeneralTask chạy thật. Controller gốc KHÔNG gắn [DefaultAuthorize] nên
    /// bản port cũng không bắt token (trung thực với gốc).
    /// </summary>
    [ApiController]
    [Route("api/InsideGeneral")]
    public class InsideGeneralController : ApiControllerBase
    {
        private readonly IInsideGeneralApp _insideGeneralApp;

        public InsideGeneralController(TokenStore tokenStore, IInsideGeneralApp insideGeneralApp)
            : base(tokenStore)
        {
            _insideGeneralApp = insideGeneralApp;
        }

        [HttpPost("MqGeneralTask")]
        public IActionResult MqGeneralTask([FromBody] MqGeneralTaskRequestDto requestDto)
        {
            var response = CommonExecute<MqGeneralTaskRequestDto, DefaultResponse>((req, resp) =>
            {
                // Map sang DTO của Infrastructure (PublicPortal chỉ dùng body).
                _insideGeneralApp.PublicPortal(new Newtouch.CIS.APIRequest.Dto.MqGeneralTaskRequestDto { body = req.body });
                resp.code = ResponseResultCode.SUCCESS;
            }, requestDto);
            return CreateResponse(response);
        }
    }
}
