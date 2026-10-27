using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.API.Controllers
{
    /// <summary>
    /// Port của TestsController gốc — controller scaffold mẫu KHÔNG dùng envelope
    /// (gốc kế thừa ApiController thuần, không phải ApiControllerBase). Giữ nguyên
    /// giá trị scaffold value1/value2 như bản gốc (tiền lệ: Base HOSP.API giữ
    /// nguyên GET mẫu value1/value2) thay vì bịa nghiệp vụ hay trả 404.
    /// </summary>
    [ApiController]
    [Route("api/Tests")]
    public class TestsController : ControllerBase
    {
        // GET api/Tests
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/Tests/5
        [HttpGet("{id:int}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Tests
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/Tests/5
        [HttpPut("{id:int}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Tests/5
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
        }
    }
}
