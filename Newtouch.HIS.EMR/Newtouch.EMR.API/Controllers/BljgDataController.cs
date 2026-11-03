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
            // F-API-02 (cycle 167): KHÔNG gán par.Timestamp ở đây — với
            // SuppressModelStateInvalidFilter, body rỗng/sai định dạng cho par
            // null và deref ở đây ném NRE ngoài CommonExecute (HTTP 500). Gán
            // trong lambda để null rơi vào try/catch của CommonExecute và trả
            // envelope lỗi sạch như các action khác (req chính là par).
            Action<BljghReq, DefaultResponse> ac = (req, resp) =>
            {
                req.Timestamp = DateTime.Now;
                _bljsRepo.BljghDataDealwith(req);
                resp.data = null;
                resp.code = APIRequestHelper.ResponseResultCode.SUCCESS;
            };

            var response = CommonExecute(ac, par, ignoreTokenDecrypt: true);
            return CreateResponse(response);
        }
    }
}
