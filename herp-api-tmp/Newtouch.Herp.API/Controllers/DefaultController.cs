using System;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Herp.API.Common;
using Newtouch.Herp.API.Requests;

namespace Newtouch.Herp.API.Controllers
{
    /// <summary>
    /// Port of Newtouch.Herp.API.Controllers.DefaultController (route api/Default).
    /// Query: the original returns _sysConfigRepo.IQueryable().ToList() — every
    /// system-config row. That repository (FrameworkBase.MultiOrg ISysConfigRepo)
    /// belongs to the Base module, not Herp, and is not ported in this copy;
    /// per the porting rules the endpoint is therefore an explicit
    /// "blocked on integration" error, never fake rows. The legacy scaffold
    /// actions (Get/Post/Put/Delete from the conventional api/{controller}/{id}
    /// route) are reproduced with their original behaviour.
    /// </summary>
    [Route("api/Default")]
    public class DefaultController : ApiControllerBase<DefaultController>
    {
        public DefaultController(IUserIdentityResolver<Identity> userIdentityResolver)
            : base(userIdentityResolver)
        {
        }

        [HttpPost]
        [Route("Query")]
        public IActionResult Query([FromBody] DefaultQueryRequest request)
        {
            Action<DefaultQueryRequest, DefaultResponse> action = (req, resp) =>
            {
                resp.code = ResponseResultCode.FAIL;
                resp.sub_code = "BLOCKED_ON_INTEGRATION";
                resp.sub_msg = "Truy vấn cấu hình hệ thống bị chặn chờ tích hợp: dữ liệu thuộc kho SysConfig "
                    + "của module Base, chưa được tích hợp vào máy chủ API Herp này; không trả về dữ liệu giả.";
            };

            var response = CommonExecute(action, request, nameof(Query));
            return CreateResponse(request, response);
        }

        // GET: api/Default/5 (legacy scaffold from the original conventional route)
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return Content("value");
        }

        // POST: api/Default (legacy scaffold; the original ignored the body)
        [HttpPost]
        public IActionResult Post()
        {
            return NoContent();
        }

        // PUT: api/Default/5 (legacy scaffold)
        [HttpPut("{id:int}")]
        public IActionResult Put(int id)
        {
            return NoContent();
        }

        // DELETE: api/Default/5 (legacy scaffold)
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}
