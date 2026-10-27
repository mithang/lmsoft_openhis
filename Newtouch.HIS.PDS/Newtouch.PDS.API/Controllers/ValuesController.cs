using Microsoft.AspNetCore.Mvc;
using Newtouch.PDS.API.Envelope;
using Newtouch.PDS.API.Services;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// Scaffold mẫu của template Web API gốc (api/values) — các action chỉ trả chuỗi mẫu
    /// "value1"/"value2"/"value", không có nghiệp vụ. Theo quy ước đã dùng ở MRMS.API:
    /// bản Core trả lỗi tường minh NOT_IMPLEMENTED thay vì trả giá trị giả.
    /// </summary>
    [ApiController]
    [Route("api/values")]
    public class ValuesController : ApiControllerBase
    {
        public ValuesController(TokenStore tokenStore)
            : base(tokenStore)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return CreateResponse(new DefaultResponse
            {
                code = ResponseResultCode.FAIL,
                sub_code = "NOT_IMPLEMENTED",
                sub_msg = "Action mẫu của template chưa được triển khai (GET api/values gốc chỉ trả chuỗi mẫu, không có nghiệp vụ).",
            });
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return CreateResponse(new DefaultResponse
            {
                code = ResponseResultCode.FAIL,
                sub_code = "NOT_IMPLEMENTED",
                sub_msg = "Action mẫu của template chưa được triển khai (GET api/values/{id} gốc chỉ trả chuỗi mẫu, không có nghiệp vụ).",
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return CreateResponse(new DefaultResponse
            {
                code = ResponseResultCode.FAIL,
                sub_code = "NOT_IMPLEMENTED",
                sub_msg = "Action mẫu của template chưa được triển khai (POST api/values gốc không có nghiệp vụ).",
            });
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return CreateResponse(new DefaultResponse
            {
                code = ResponseResultCode.FAIL,
                sub_code = "NOT_IMPLEMENTED",
                sub_msg = "Action mẫu của template chưa được triển khai (PUT api/values/{id} gốc không có nghiệp vụ).",
            });
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return CreateResponse(new DefaultResponse
            {
                code = ResponseResultCode.FAIL,
                sub_code = "NOT_IMPLEMENTED",
                sub_msg = "Action mẫu của template chưa được triển khai (DELETE api/values/{id} gốc không có nghiệp vụ).",
            });
        }
    }
}
