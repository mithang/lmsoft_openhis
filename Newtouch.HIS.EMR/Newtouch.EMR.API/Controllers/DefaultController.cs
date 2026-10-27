using Microsoft.AspNetCore.Mvc;
using Newtouch.EMR.API.Models;
using Newtouch.EMR.APIRequest;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Infrastructure;
using Newtouch.HIS.API.Common;
using StackExchange.Redis;
using System;
using System.Linq;

namespace Newtouch.EMR.API.Controllers
{
    [ApiController]
    [Route("api/Default")]
    public class DefaultController : ApiControllerBase<DefaultController>
    {
        private readonly ISysConfigRepo _sysConfigRepo;

        public DefaultController(IConnectionMultiplexer redis, ISysConfigRepo sysConfigRepo) : base(redis)
        {
            _sysConfigRepo = sysConfigRepo;
        }

        [HttpPost]
        [Route("Query")]
        public ContentResult Query([FromBody] DefaultQueryRequest par)
        {
            Action<DefaultQueryRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _sysConfigRepo.IQueryable().ToList();
                resp.data = data;
                resp.code = APIRequestHelper.ResponseResultCode.SUCCESS;
            };

            var response = CommonExecute(ac, par);
            return CreateResponse(response);
        }

        // GET: api/Default/5 (template stubs preserved from the original)
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Default
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Default/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
