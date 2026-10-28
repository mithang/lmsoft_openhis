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
    /// QA PHASE 2 (API-03/API-04): KHÔNG gắn [DefaultAuthorize] ở controller này
    /// (quyết định hoãn, không phải bỏ qua): caller duy nhất là Base
    /// SysPharmacyDepartmentApp qua SitePdsApiHelper.Request — bản port của helper
    /// này POST trần KHÔNG kèm token (GetToken() có sẵn nhưng Request không
    /// gọi). Gắn auth ngay sẽ phá luồng cấp quyền kho của Base Web. Cần sửa
    /// helper trước (Request kèm GetToken(), trừ nhánh GetToken tự nó), rồi mới
    /// gắn auth + kiểm chứng luồng Base Web → PDS API trên STG. Tới lúc đó,
    /// bề mặt này phải chặn ở tầng mạng (chỉ host Base được tới PDS API).
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
