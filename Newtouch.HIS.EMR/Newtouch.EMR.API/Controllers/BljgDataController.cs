using Microsoft.AspNetCore.Mvc;
using Newtouch.EMR.API.Models;
using Newtouch.EMR.APIRequest.Bljgh.Request;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Infrastructure;
using StackExchange.Redis;
using System;

namespace Newtouch.EMR.API.Controllers
{
    [ApiController]
    [Route("api/BljgData")]
    public class BljgDataController : ApiControllerBase<BljgDataController>
    {
        private readonly Ibl_ElementDomainRepo _bljsRepo;
        private readonly Ibl_ElementDomain_DetailRepo _bljsmxRepo;
        private readonly Ibl_bllxRepo _bllxRepo;

        public BljgDataController(IConnectionMultiplexer redis,
            Ibl_ElementDomainRepo bljsRepo,
            Ibl_ElementDomain_DetailRepo bljsmxRepo,
            Ibl_bllxRepo bllxRepo) : base(redis)
        {
            _bljsRepo = bljsRepo;
            _bljsmxRepo = bljsmxRepo;
            _bllxRepo = bllxRepo;
        }

        [HttpPost]
        [Route("BljghDataDealwith")]
        [IgnoreTokenDecrypt]
        public ContentResult BljghDataDealwith([FromBody] BljghReq par)
        {
            par.Timestamp = DateTime.Now;
            Action<BljghReq, DefaultResponse> ac = (req, resp) =>
            {
                _bljsRepo.BljghDataDealwith(par);
                resp.data = null;
                resp.code = APIRequestHelper.ResponseResultCode.SUCCESS;
            };

            var response = CommonExecute(ac, par, ignoreTokenDecrypt: true);
            return CreateResponse(response);
        }
    }
}
