using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>系统收费大类 (scaffold gốc — giữ nguyên hành vi value1/value2).</summary>
    [Route("api/ChargeCategory")]
    [ApiController]
    public class ChargeCategoryController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id:int}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id:int}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
        }
    }
}
