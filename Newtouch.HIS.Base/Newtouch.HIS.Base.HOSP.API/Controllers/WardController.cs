using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Base.HOSP.API.Envelope;
using Newtouch.HIS.Base.HOSP.Request;
using Newtouch.HIS.Base.HOSP.API.Services;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>Liên quan khu bệnh</summary>
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

        /// <summary>Cập nhật giường có bị chiếm không</summary>
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
