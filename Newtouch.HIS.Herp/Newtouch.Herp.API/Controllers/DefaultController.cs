using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Herp.API.Common;
using Newtouch.Herp.API.Requests;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.API.Controllers
{
    /// <summary>
    /// Port of Newtouch.Herp.API.Controllers.DefaultController (route api/Default).
    /// Query: the original returns _sysConfigRepo.IQueryable().ToList() — every
    /// system-config row. The repository is now resolved: the Herp API's only
    /// context in the original was DefaultDbContext = NewtouchHIS_herp, and
    /// CSDL Base has no Sys_Config table at all, so the thin Herp-local
    /// ISysConfigRepo (Sys_Config in NewtouchHIS_herp) is the faithful source
    /// — not a Base-module integration. The legacy scaffold actions
    /// (Get/Post/Put/Delete from the conventional api/{controller}/{id} route)
    /// are reproduced with their original behaviour.
    /// </summary>
    [Route("api/Default")]
    public class DefaultController : ApiControllerBase<DefaultController>
    {
        private readonly ISysConfigRepo _sysConfigRepo;

        public DefaultController(IUserIdentityResolver<Identity> userIdentityResolver,
            ISysConfigRepo sysConfigRepo)
            : base(userIdentityResolver)
        {
            _sysConfigRepo = sysConfigRepo;
        }

        [HttpPost]
        [Route("Query")]
        public IActionResult Query([FromBody] DefaultQueryRequest request)
        {
            Action<DefaultQueryRequest, DefaultResponse> action = (req, resp) =>
            {
                var data = _sysConfigRepo.IQueryable().ToList();
                resp.data = data;

                resp.code = ResponseResultCode.SUCCESS;
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
