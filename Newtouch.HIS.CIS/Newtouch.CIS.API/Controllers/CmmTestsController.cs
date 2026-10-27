using Microsoft.AspNetCore.Mvc;
using Newtouch.Application.Interface;
using Newtouch.Domain.DTO.OutputDto;
using Newtonsoft.Json;

namespace Newtouch.CIS.API.Controllers
{
    /// <summary>
    /// Port của CmmTestsController (api/cmmtests) — endpoint kiểm tra cấu trúc
    /// phòng khám YHCT. SỬA BUG GỐC: field _outpatientCmmManagerApp ở bản
    /// Framework không bao giờ được gán (ctor chỉ nhận IComponentContext, không
    /// resolve field) nên TCM_HIS_01 gốc sẽ NullReference — bản Core inject
    /// IOutpatientCmmManagerApp qua DI nên action chạy thật.
    ///
    /// Lưu ý trung thực với bản gốc: action này KHÔNG dùng envelope CommonExecute
    /// (gốc trả JsonResult trực tiếp, không kiểm tra token/Timestamp) nên bản
    /// port cũng trả JSON thô, không envelope — không tự ý thêm auth vào một
    /// endpoint kiểm tra vốn không có auth. Overload nhận TcmHis01Request của
    /// gốc đã bị comment-out nên không port.
    /// </summary>
    [ApiController]
    [Route("api/cmmtests")]
    public class CmmTestsController : ControllerBase
    {
        private readonly IOutpatientCmmManagerApp _outpatientCmmManagerApp;

        public CmmTestsController(IOutpatientCmmManagerApp outpatientCmmManagerApp)
        {
            _outpatientCmmManagerApp = outpatientCmmManagerApp;
        }

        [HttpPost("TCM_HIS_01")]
        public IActionResult TcmHis01([FromBody] TreatEntityObj jzxx, string organizeId)
        {
            var result = _outpatientCmmManagerApp.TcmHis01(jzxx, organizeId, "interfacetest");
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
    }
}
