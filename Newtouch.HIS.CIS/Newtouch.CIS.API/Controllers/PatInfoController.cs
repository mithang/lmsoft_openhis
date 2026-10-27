using Microsoft.AspNetCore.Mvc;
using Newtouch.CIS.API.Envelope;
using Newtouch.CIS.API.Requests;
using Newtouch.CIS.API.Services;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;

namespace Newtouch.CIS.API.Controllers
{
    /// <summary>
    /// Port của PatInfoController (api/PatInfo).
    /// SỬA BUG GỐC: cả 2 field (_iInpatientPatientInfoRepo, _IBaseDataDmnService)
    /// ở bản Framework đều không bao giờ được gán (ctor chỉ nhận IComponentContext)
    /// — bản Core inject cả 2 qua DI nên các action chạy thật.
    /// UpdateInpatientBasicInfo + UpdatebrxzInfo GHI dữ liệu — code chạy thật nhưng
    /// KHÔNG live-test ghi trong task này; InpatientBaseInfo là đọc (live-test được).
    /// </summary>
    [ApiController]
    [Route("api/PatInfo")]
    [DefaultAuthorize]
    public class PatInfoController : ApiControllerBase
    {
        private readonly IInpatientPatientInfoRepo _iInpatientPatientInfoRepo;
        private readonly IBaseDataDmnService _IBaseDataDmnService;

        public PatInfoController(TokenStore tokenStore, IInpatientPatientInfoRepo iInpatientPatientInfoRepo,
            IBaseDataDmnService iBaseDataDmnService)
            : base(tokenStore)
        {
            _iInpatientPatientInfoRepo = iInpatientPatientInfoRepo;
            _IBaseDataDmnService = iBaseDataDmnService;
        }

        [HttpPost("UpdateInpatientBasicInfo")]
        public IActionResult UpdateInpatientBasicInfo([FromBody] PatInfoRequest par)
        {
            var response = CommonExecute<PatInfoRequest, DefaultResponse>((req, resp) =>
            {
                _iInpatientPatientInfoRepo.UpdateInpatientStatus(UserIdentity.OrganizeId, req.zyh, req.zybz, req.ryzd, req.ryzdmc, req.brxz, req.brxzmc, req.lxr, req.lxrgx, req.lxrdh);
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }

        [HttpPost("UpdatebrxzInfo")]
        public IActionResult UpdatebrxzInfo([FromBody] UpdatebrxzRequest par)
        {
            var response = CommonExecute<UpdatebrxzRequest, DefaultResponse>((req, resp) =>
            {
                _IBaseDataDmnService.UpdatebrxzInfo(UserIdentity.OrganizeId, req.mzh, req.zyh, req.brxzCode, req.brxzmc);
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }

        /// <summary>Lấy thông tin cơ bản bệnh nhân nội trú.</summary>
        [HttpPost("InpatientBaseInfo")]
        public IActionResult InpatientBaseInfo([FromBody] InpatientBaseInfoRequest par)
        {
            var response = CommonExecute<InpatientBaseInfoRequest, DefaultResponse>((req, resp) =>
            {
                resp.data = _iInpatientPatientInfoRepo.SelectData(par.zyh, par.xm, par.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }
    }
}
