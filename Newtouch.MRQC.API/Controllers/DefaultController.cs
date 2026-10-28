using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.MRQC.API.Envelope;
using Newtouch.MRQC.API.Requests;
using Newtouch.MRQC.API.Services;

namespace Newtouch.MRQC.API.Controllers
{
    /// <summary>
    /// Port của Newtouch.MRQC.API DefaultController (api/Default).
    /// SỬA BUG GỐC: _sysConfigRepo ở bản Framework không bao giờ được gán
    /// (ctor chỉ nhận IComponentContext, không resolve field — cùng lỗi với MRMS.API)
    /// — bản Core inject ISysConfigRepo qua DI nên Query chạy thật thay vì NullReference.
    /// </summary>
    [ApiController]
    [Route("api/Default")]
    public class DefaultController : ApiControllerBase
    {
        private readonly ISysConfigRepo _sysConfigRepo;

        public DefaultController(TokenStore tokenStore, ISysConfigRepo sysConfigRepo)
            : base(tokenStore)
        {
            _sysConfigRepo = sysConfigRepo;
        }

        /// <summary>Trả toàn bộ Sys_Config (đúng hành vi gốc: không lọc theo keyword).</summary>
        [HttpPost("Query")]
        [DefaultAuthorize]
        public IActionResult Query([FromBody] DefaultQueryRequest par)
        {
            var response = CommonExecute<DefaultQueryRequest, DefaultResponse>((req, resp) =>
            {
                var data = _sysConfigRepo.IQueryable().ToList();
                resp.data = data;
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }

        // Các action scaffold mẫu của template WebAPI gốc (Get/Post/Put/Delete chỉ
        // trả "value"/rỗng, không có nghiệp vụ) — bản Core để stub lỗi tường minh
        // thay vì trả giá trị giả, đúng quy tắc "không thành công giả, không 404 khó hiểu".

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return CreateResponse(new DefaultResponse
            {
                code = ResponseResultCode.FAIL,
                sub_code = "NOT_IMPLEMENTED",
                sub_msg = "Chức năng mẫu của template chưa được triển khai (GET api/Default/{id} gốc chỉ trả chuỗi mẫu, không có nghiệp vụ).",
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return CreateResponse(new DefaultResponse
            {
                code = ResponseResultCode.FAIL,
                sub_code = "NOT_IMPLEMENTED",
                sub_msg = "Chức năng mẫu của template chưa được triển khai (POST api/Default gốc không có nghiệp vụ).",
            });
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return CreateResponse(new DefaultResponse
            {
                code = ResponseResultCode.FAIL,
                sub_code = "NOT_IMPLEMENTED",
                sub_msg = "Chức năng mẫu của template chưa được triển khai (PUT api/Default/{id} gốc không có nghiệp vụ).",
            });
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return CreateResponse(new DefaultResponse
            {
                code = ResponseResultCode.FAIL,
                sub_code = "NOT_IMPLEMENTED",
                sub_msg = "Chức năng mẫu của template chưa được triển khai (DELETE api/Default/{id} gốc không có nghiệp vụ).",
            });
        }
    }
}
