using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Application.Implementation;
using Newtouch.PDS.API.Envelope;
using Newtouch.PDS.API.Services;
using Newtouch.PDS.Requset.ClinicTfOperate;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// Port của api/ClinicTfOperate (hoàn phí ngoại trú cập nhật đơn thuốc chờ phát).
    /// Gốc dùng `new ClinicTfOperateApp(par).Process()`; bản port giữ nguyên cách gọi đó.
    /// ProcessorBase kế thừa AppBase nên các field repo của App được resolve qua
    /// ServiceLocator (Provider do Program.cs gán, giống PDS Web).
    /// </summary>
    [ApiController]
    [Route("api/ClinicTfOperate")]
    public class ClinicTfOperateController : ApiControllerBase
    {
        public ClinicTfOperateController(TokenStore tokenStore)
            : base(tokenStore)
        {
        }

        [HttpPost("UpdateCfByTf")]
        public IActionResult UpdateCfByTf([FromBody] ClinicTfOperateRequest par)
        {
            var response = CommonExecute<ClinicTfOperateRequest, DefaultResponse>((req, resp) =>
            {
                resp.data = new ClinicTfOperateApp(par).Process();
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }
    }
}
