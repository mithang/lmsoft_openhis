using Microsoft.AspNetCore.Mvc;
using Newtouch.CIS.API.Envelope;
using Newtouch.CIS.API.Services;

namespace Newtouch.CIS.API.Controllers
{
    /// <summary>
    /// Port của NewtouchCIS.API DefaultController (api/Default).
    /// SỬA BUG GỐC: _sysUserRepo ở bản Framework không bao giờ được gán
    /// (ctor chỉ nhận IComponentContext) — nhưng action scaffold gốc cũng không
    /// dùng tới field này nên bản Core bỏ field, không inject thừa.
    /// Các action scaffold mẫu của template WebAPI gốc (Get/Post/Put/Delete chỉ
    /// trả "value"/rỗng, không có nghiệp vụ) — bản Core để stub lỗi tường minh
    /// thay vì trả giá trị giả, đúng quy tắc "không thành công giả".
    /// </summary>
    [ApiController]
    [Route("api/Default")]
    public class DefaultController : ApiControllerBase
    {
        public DefaultController(TokenStore tokenStore)
            : base(tokenStore)
        {
        }

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
