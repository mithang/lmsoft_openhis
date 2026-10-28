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
    /// QA PHASE 2 (API-03): chỉ 4 action gắn [DefaultAuthorize] (CancelPartBook,
    /// CancelAllBook, CancelDjYpReturn, CancelArrangement — caller mang token được
    /// hoặc không có caller tokenless). 5 action còn lại KHÔNG gắn vì caller thật
    /// gọi tokenless có chủ đích: Book/BookModify (CIS MedicalRecordDmnService,
    /// autoAppendToken: false), OutpatientCommit + PartReturnBeforeDispensing
    /// (Sett Web Task.Run/refund thread, autoAppendToken: false),
    /// HospitalizatiionReturnDispensingMedicine (CIS InpatientMedReturnDmnService,
    /// autoAppendToken: false). Gắn auth vào 5 action này sẽ phá luồng thật;
    /// chúng phải được chặn ở tầng mạng (chỉ host CIS/Sett được tới PDS API),
    /// xem báo cáo qa-phase2/api-auth.md.
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
        [DefaultAuthorize]
        public IActionResult OutpatientCancelPartBook([FromBody] OutpatientCancelPartBookRequestDTO request)
        {
            var response = CommonExecute<OutpatientCancelPartBookRequestDTO, DefaultResponse>((req, resp) =>
            {
                // QA PHASE 2 (API-07b): Items null/rỗng trước đây rơi vào app
                // (Items.Select) gây ArgumentNullException → 20000. Chặn sớm
                // bằng FAIL tiếng Việt; caller hợp lệ luôn gửi Items thật.
                if (req.Items == null || req.Items.Count == 0)
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "FAILED";
                    resp.sub_msg = "Danh sách hủy một phần không được để trống";
                    return;
                }
                resp.data = _resourcesOperateApp.OutpatientCancelBook(req);
                resp.code = ResponseResultCode.SUCCESS;
            }, request);
            return CreateResponse(request, response);
        }

        [HttpPost("OutpatientCancelAllBook")]
        [DefaultAuthorize]
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
        [DefaultAuthorize]
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
        [DefaultAuthorize]
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
