using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Application.Implementation;
using Newtouch.PDS.API.Envelope;
using Newtouch.PDS.API.Services;
using Newtouch.PDS.Requset.ResourcesOperate;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// Port của api/test (endpoint kiểm thử của bản gốc).
    /// TestMultithreadTrans / TestEFDbTransaction được nối dây qua ITestMultithreadTransApp
    /// như gốc (lớp đã port sẵn trong Infrastructure) nhưng KHÔNG được gọi thử live:
    /// chúng chạy transaction với dữ liệu thử nghiệm — thuộc nhóm write-path, theo chính
    /// sách no-write thì chỉ biên dịch và nối dây, không thực thi trên DB demo.
    /// TestToken trả SUCCESS vô điều kiện như gốc — gọi thử live được.
    /// </summary>
    [ApiController]
    [Route("api/test")]
    public class TestController : ApiControllerBase
    {
        public TestController(TokenStore tokenStore)
            : base(tokenStore)
        {
        }

        // Gốc dùng `new TestMultithreadTransApp()` trực tiếp (không qua interface —
        // interface port sẵn chỉ khai báo testEFDbTransaction, còn test1 chỉ có trên
        // lớp cụ thể). AppBase ctor resolve field qua ServiceLocator nên `new` vẫn
        // hoạt động khi Program.cs đã gán Provider, giống PDS Web.
        [HttpGet("TestMultithreadTrans")]
        public IActionResult TestMultithreadTrans()
        {
            return Content(new TestMultithreadTransApp().test1(), "text/plain");
        }

        [HttpGet("TestEFDbTransaction")]
        public IActionResult TestEFDbTransaction()
        {
            return Content(new TestMultithreadTransApp().testEFDbTransaction(), "text/plain");
        }

        // Gốc KHÔNG gọi CommonExecute ở action này: nó tạo DefaultResponse, gán SUCCESS
        // và trả luôn, bất kể request có Timestamp hay field bắt buộc hay không.
        // Bản port giữ nguyên hành vi đó (không kiểm tra token/Timestamp/Validate).
        [HttpPost("TestToken")]
        public IActionResult TestToken([FromBody] OutpatientCommitRequestDTO request)
        {
            var response = new DefaultResponse
            {
                code = ResponseResultCode.SUCCESS,
            };
            return CreateResponse(response);
        }
    }
}
