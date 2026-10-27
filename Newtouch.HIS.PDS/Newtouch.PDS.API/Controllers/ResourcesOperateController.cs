using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Application.Interface;
using Newtouch.PDS.API.Envelope;
using Newtouch.PDS.API.Services;
using Newtouch.PDS.Requset.ResourcesOperate;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// Port của api/ResourcesOperate (thao tác tài nguyên: đặt trước / hủy / commit...).
    /// SỬA BUG GỐC: _resourcesOperateApp ở bản Framework được khai báo nhưng không bao giờ
    /// được gán (ctor chỉ nhận IComponentContext) — mọi action gốc đều sẽ NullReference.
    /// Bản Core inject IResourcesOperateApp qua DI nên các action chạy thật.
    /// OutpatientPartReturnBeforeDispensingMedicine mang [IgnoreTokenDecrypt] như gốc;
    /// ở bản port toàn bộ token đều dùng thô nên attribute chỉ còn ý nghĩa ghi chú.
    /// </summary>
    [ApiController]
    [Route("api/ResourcesOperate")]
    public class ResourcesOperateController : ApiControllerBase
    {
        private readonly IResourcesOperateApp _resourcesOperateApp;

        public ResourcesOperateController(TokenStore tokenStore, IResourcesOperateApp resourcesOperateApp)
            : base(tokenStore)
        {
            _resourcesOperateApp = resourcesOperateApp;
        }

        [HttpPost("OutpatientBook")]
        public IActionResult OutpatientBook([FromBody] OutpatientBookRequestDTO par)
        {
            var response = CommonExecute<OutpatientBookRequestDTO, DefaultResponse>((req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientBook(req);
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }

        [HttpPost("OutpatientBookModify")]
        public IActionResult OutpatientBookModify([FromBody] OutpatientBookModifyRequestDTO par)
        {
            var response = CommonExecute<OutpatientBookModifyRequestDTO, DefaultResponse>((req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientBookModify(req);
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }

        [HttpPost("OutpatientCancelPartBook")]
        public IActionResult OutpatientCancelPartBook([FromBody] OutpatientCancelPartBookRequestDTO request)
        {
            var response = CommonExecute<OutpatientCancelPartBookRequestDTO, DefaultResponse>((req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientCancelBook(req);
                resp.code = ResponseResultCode.SUCCESS;
            }, request);
            return CreateResponse(request, response);
        }

        [HttpPost("OutpatientCancelAllBook")]
        public IActionResult OutpatientCancelAllBook([FromBody] OutpatientCancelAllBookRequestDTO request)
        {
            var response = CommonExecute<OutpatientCancelAllBookRequestDTO, DefaultResponse>((req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientCancelBook(req);
                resp.code = ResponseResultCode.SUCCESS;
            }, request);
            return CreateResponse(request, response);
        }

        [HttpPost("OutpatientCancelDjYpReturn")]
        public IActionResult OutpatientCancelDjYpReturn([FromBody] OutpatientCancelAllBookRequestDTO request)
        {
            var response = CommonExecute<OutpatientCancelAllBookRequestDTO, DefaultResponse>((req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientCancelDjYpReturn(req);
                resp.code = ResponseResultCode.SUCCESS;
            }, request);
            return CreateResponse(request, response);
        }

        [HttpPost("OutpatientCommit")]
        public IActionResult OutpatientCommit([FromBody] OutpatientCommitRequestDTO request)
        {
            var response = CommonExecute<OutpatientCommitRequestDTO, DefaultResponse>((req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientCommit(req);
                resp.code = ResponseResultCode.SUCCESS;
            }, request);
            return CreateResponse(request, response);
        }

        [HttpPost("OutpatientPartReturnBeforeDispensingMedicine")]
        [IgnoreTokenDecrypt]
        public IActionResult OutpatientPartReturnBeforeDispensingMedicine([FromBody] OutpatientPartReturnBeforeDispensingMedicineRequestDTO request)
        {
            var response = CommonExecute<OutpatientPartReturnBeforeDispensingMedicineRequestDTO, DefaultResponse>((req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientPartReturnBeforeDispensingMedicine(req);
                resp.code = ResponseResultCode.SUCCESS;
            }, request);
            return CreateResponse(request, response);
        }

        [HttpPost("OutpatientCancelArrangement")]
        public IActionResult OutpatientCancelArrangement([FromBody] OutpatientCancelArrangementRequestDTO request)
        {
            var response = CommonExecute<OutpatientCancelArrangementRequestDTO, DefaultResponse>((req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientCancelArrangement(req);
                resp.code = ResponseResultCode.SUCCESS;
            }, request);
            return CreateResponse(request, response);
        }

        [HttpPost("HospitalizatiionReturnDispensingMedicine")]
        public IActionResult HospitalizatiionReturnDispensingMedicine([FromBody] HospitalizatiionReturnDispensingMedicineRequestDTO request)
        {
            var response = CommonExecute<HospitalizatiionReturnDispensingMedicineRequestDTO, DefaultResponse>((req, resp) =>
            {
                resp.data = _resourcesOperateApp.HospitalizatiionReturnDispensingMedicine(req);
                resp.code = ResponseResultCode.SUCCESS;
            }, request);
            return CreateResponse(request, response);
        }
    }
}
