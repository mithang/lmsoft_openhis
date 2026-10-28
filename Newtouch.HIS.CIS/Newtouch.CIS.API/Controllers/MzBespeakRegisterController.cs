using Microsoft.AspNetCore.Mvc;
using Newtouch.CIS.API.Envelope;
using Newtouch.CIS.API.Requests;
using Newtouch.CIS.API.Services;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;

namespace Newtouch.CIS.API.Controllers
{
    /// <summary>
    /// Port của MzBespeakRegisterController (api/MzBespeakRegister).
    /// SỬA BUG GỐC: cả 3 field (_mzyyghRepo, _mzyyghDmnService,
    /// _sysBespeakRegisterDmnService) ở bản Framework đều không bao giờ được gán
    /// (ctor chỉ nhận IComponentContext) — bản Core inject cả 3 qua DI nên các
    /// action chạy thật. Controller gốc KHÔNG gắn [DefaultAuthorize] nhưng mọi
    /// action đều đọc UserIdentity.OrganizeId: gọi không kèm token sẽ rơi về
    /// envelope lỗi hệ thống (bản gốc sẽ NullReference 500) — hành vi này được
    /// giữ nguyên, chỉ khác là lỗi trả về dạng envelope thay vì crash.
    /// 3 action tra cứu là đọc; KeepAnAppointment GHI dữ liệu.
    /// QA PHASE 2 (API-02/API-04): cả 4 action gắn [DefaultAuthorize] — caller
    /// hợp pháp là Sett (SiteCISAPIHelper mặc định tự kèm token); luồng đặt hẹn
    /// công khai không token không tồn tại trong cây port (site đặt hẹn của Sett
    /// đã bị stub hoá, callsite KeepAnAppointment gốc trong Sett đã bị comment).
    /// Trước đây KeepAnAppointment không token với mzyyghId thật sẽ ghi thật vì
    /// repo call không dùng UserIdentity (khác Prescription: NRE ở OrganizeId).
    /// </summary>
    [ApiController]
    [Route("api/MzBespeakRegister")]
    public class MzBespeakRegisterController : ApiControllerBase
    {
        private readonly IMzyyghRepo _mzyyghRepo;
        private readonly IMzyyghDmnService _mzyyghDmnService;
        private readonly ISysBespeakRegisterDmnService _sysBespeakRegisterDmnService;

        public MzBespeakRegisterController(TokenStore tokenStore, IMzyyghRepo mzyyghRepo,
            IMzyyghDmnService mzyyghDmnService, ISysBespeakRegisterDmnService sysBespeakRegisterDmnService)
            : base(tokenStore)
        {
            _mzyyghRepo = mzyyghRepo;
            _mzyyghDmnService = mzyyghDmnService;
            _sysBespeakRegisterDmnService = sysBespeakRegisterDmnService;
        }

        [HttpPost("MzBespeakRegisterQuery")]
        [DefaultAuthorize]
        public IActionResult MzBespeakRegisterQuery([FromBody] MzBespeakRegisterQueryRequestDTO request)
        {
            var response = CommonExecute<MzBespeakRegisterQueryRequestDTO, DefaultResponse>((req, resp) =>
            {
                resp.data = _mzyyghDmnService.MzBespeakRegisterQuery(req.blh, req.kh, req.zjh, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            }, request);
            return CreateResponse(response);
        }

        [HttpPost("MzBespeakRegisterSchedulingQuery")]
        [DefaultAuthorize]
        public IActionResult MzBespeakRegisterSchedulingQuery([FromBody] SysBespeakRegisterQueryRequestDTO request)
        {
            var response = CommonExecute<SysBespeakRegisterQueryRequestDTO, DefaultResponse>((req, resp) =>
            {
                resp.data = _sysBespeakRegisterDmnService.SelectSysBespeakregister(req.ksCode, req.ysgh, UserIdentity.OrganizeId, req.regDate, req.regTime);
                resp.code = ResponseResultCode.SUCCESS;
            }, request);
            return CreateResponse(response);
        }

        [HttpPost("MzAlreadyBespeakRegisterCountQuery")]
        [DefaultAuthorize]
        public IActionResult MzAlreadyBespeakRegisterCountQuery([FromBody] MzAlreadyBespeakRegisterCountQueryRequestDTO request)
        {
            var response = CommonExecute<MzAlreadyBespeakRegisterCountQueryRequestDTO, DefaultResponse>((req, resp) =>
            {
                resp.data = _mzyyghDmnService.SelectAlreadyBespeakRegisterCount(req.ksCode, req.ysgh, req.regDate, req.regTime, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            }, request);
            return CreateResponse(response);
        }

        [HttpPost("KeepAnAppointment")]
        [DefaultAuthorize]
        public IActionResult KeepAnAppointment([FromBody] KeepAnAppointmentRequestDTO request)
        {
            var response = CommonExecute<KeepAnAppointmentRequestDTO, DefaultResponse>((req, resp) =>
            {
                resp.data = _mzyyghRepo.KeepAnAppointment(req.mzyyghId, req.arrivalDate, req.arrivalOpereater);
                resp.code = ResponseResultCode.SUCCESS;
            }, request);
            return CreateResponse(response);
        }
    }
}
