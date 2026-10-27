using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Application.Interface;
using Newtouch.PDS.API.Envelope;
using Newtouch.PDS.API.Services;
using Newtouch.PDS.Requset.Zyypyz;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// Port của api/Zyypyz (y lệnh thuốc nội trú).
    /// SỬA BUG GỐC: _resourcesOperateApp ở bản Framework được khai báo nhưng không bao giờ
    /// được gán (ctor chỉ nhận IComponentContext) — gọi action gốc sẽ NullReference.
    /// Bản Core inject IResourcesOperateApp qua DI nên action chạy thật.
    /// </summary>
    [ApiController]
    [Route("api/Zyypyz")]
    public class ZyypyzController : ApiControllerBase
    {
        private readonly IResourcesOperateApp _resourcesOperateApp;

        public ZyypyzController(TokenStore tokenStore, IResourcesOperateApp resourcesOperateApp)
            : base(tokenStore)
        {
            _resourcesOperateApp = resourcesOperateApp;
        }

        [HttpPost("yzzx")]
        public IActionResult Yzzx([FromBody] ZyypyzzxRequest par)
        {
            var response = CommonExecute<ZyypyzzxRequest, DefaultResponse>((req, resp) =>
            {
                resp.data = _resourcesOperateApp.HospitalizatiionBook(req);
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }
    }
}
