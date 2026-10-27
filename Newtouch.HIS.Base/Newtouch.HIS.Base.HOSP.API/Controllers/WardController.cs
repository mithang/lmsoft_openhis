using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Base.HOSP.API.Envelope;
using Newtouch.HIS.Base.HOSP.Request;
using Newtouch.HIS.Base.HOSP.API.Services;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>病区相关</summary>
    [Route("api/Ward")]
    [ApiController]
    [DefaultAuthorize]
    public class WardController : ApiControllerBase
    {
        private readonly ISysWardBedRepo _sysWardBedRepo;

        public WardController(TokenStore tokenStore, ISysWardBedRepo sysWardBedRepo)
            : base(tokenStore)
        {
            _sysWardBedRepo = sysWardBedRepo;
        }

        /// <summary>更新床位是否占用</summary>
        [HttpPost("UpdateBedOccupy")]
        public ContentResult UpdateBedOccupy([FromBody] UpdateBedOccupyRequest request)
        {
            System.Action<UpdateBedOccupyRequest, DefaultResponse> ac = (req, resp) =>
            {
                _sysWardBedRepo.UpdateOccupyByCode(req.cwCode, UserIdentity.OrganizeId, req.isOccupy);

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = CommonExecute(ac, request);
            return CreateResponse(response);
        }
    }
}
