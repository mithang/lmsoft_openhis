using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.PDS.API.Envelope;
using Newtouch.PDS.API.Services;
using Newtouch.PDS.Requset.PharmacyDepartment;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// Port của api/PharmacyDepartment (bộ phận kho).
    /// SỬA BUG GỐC: cả hai field _sysPharmacyDepartmentDmnService và
    /// _sysPharmacyDepartmentApp ở bản Framework đều được khai báo nhưng không bao giờ
    /// được gán (ctor chỉ nhận IComponentContext) — mọi action gốc đều sẽ NullReference.
    /// Bản Core inject cả hai qua DI nên các action chạy thật.
    /// </summary>
    [ApiController]
    [Route("api/PharmacyDepartment")]
    public class PharmacyDepartmentController : ApiControllerBase
    {
        private readonly ISysPharmacyDepartmentDmnService _sysPharmacyDepartmentDmnService;
        private readonly ISysPharmacyDepartmentApp _sysPharmacyDepartmentApp;

        public PharmacyDepartmentController(TokenStore tokenStore,
            ISysPharmacyDepartmentDmnService sysPharmacyDepartmentDmnService,
            ISysPharmacyDepartmentApp sysPharmacyDepartmentApp)
            : base(tokenStore)
        {
            _sysPharmacyDepartmentDmnService = sysPharmacyDepartmentDmnService;
            _sysPharmacyDepartmentApp = sysPharmacyDepartmentApp;
        }

        [HttpPost("EmpowermentPharmacyDepartmentQuery")]
        public IActionResult EmpowermentPharmacyDepartmentQuery([FromBody] SysPharmacyDepartmentRequestDto par)
        {
            var response = CommonExecute<SysPharmacyDepartmentRequestDto, DefaultResponse>((req, resp) =>
            {
                resp.data = _sysPharmacyDepartmentDmnService.SelectEmpowermentYfbmByYp(req.ypId, req.organizeId);
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }

        [HttpPost("EmpowermentPharmacyDepartment")]
        public IActionResult EmpowermentPharmacyDepartment([FromBody] EmpowermentPharmacyDepartmentRequestDto par)
        {
            var response = CommonExecute<EmpowermentPharmacyDepartmentRequestDto, DefaultResponse>((req, resp) =>
            {
                resp.data = _sysPharmacyDepartmentApp.EmpowermentPharmacyDepartment(req);
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }

        [HttpPost("EmpowermentPharmacyDepartmentAndRemoveOld")]
        public IActionResult EmpowermentPharmacyDepartmentAndRemoveOld([FromBody] EmpowermentPharmacyDepartmentAndRemoveOldRequestDto par)
        {
            var response = CommonExecute<EmpowermentPharmacyDepartmentAndRemoveOldRequestDto, DefaultResponse>((req, resp) =>
            {
                resp.data = _sysPharmacyDepartmentApp.EmpowermentPharmacyDepartmentAndRemoveOld(req);
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }
    }
}
