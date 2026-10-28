using Microsoft.AspNetCore.Mvc;
using Newtouch.CIS.API.Envelope;
using Newtouch.CIS.API.Requests;
using Newtouch.CIS.API.Services;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.CIS.API.Controllers
{
    /// <summary>
    /// Port của PrescriptionController (api/Prescription).
    /// SỬA BUG GỐC: _IPrescriptionRepo ở bản Framework không bao giờ được gán
    /// (ctor chỉ nhận IComponentContext) — bản Core inject qua DI nên cả 3 action
    /// cập nhật trạng thái đều chạy thật. Cả 3 action đều GHI dữ liệu — code chạy
    /// thật nhưng KHÔNG live-test ghi trong task này, chỉ live-test envelope/token.
    /// QA PHASE 2 (API-01): UpdateChargeStatus và UpdateChargeTbz gắn
    /// [DefaultAuthorize] — caller hợp pháp là Sett Web, gọi qua
    /// SiteCISAPIHelper.Request mặc định (tự kèm token của user đang đăng nhập).
    /// UpdateChargeStatusApi KHÔNG gắn auth (cố tình giữ nguyên): kênh nội bộ
    /// tokenless theo thiết kế gốc (tự null Token/TokenType, dùng OrganizeId/AppId
    /// do request gửi — caller gốc là OrderDmnService của Sett); kênh này phải
    /// được chặn ở tầng mạng (chỉ host Sett được tới CIS API), xem báo cáo
    /// qa-phase2/api-auth.md. Trong cây port hiện tại, caller tokenless cũ
    /// (Sett.API BookingRegister) không còn tồn tại.
    /// </summary>
    [ApiController]
    [Route("api/Prescription")]
    public class PrescriptionController : ApiControllerBase
    {
        private readonly IPrescriptionRepo _IPrescriptionRepo;

        public PrescriptionController(TokenStore tokenStore, IPrescriptionRepo iPrescriptionRepo)
            : base(tokenStore)
        {
            _IPrescriptionRepo = iPrescriptionRepo;
        }

        /// <summary>Cập nhật dấu hiệu thu phí đơn thuốc.</summary>
        [HttpPost("UpdateChargeStatus")]
        [DefaultAuthorize]
        public IActionResult UpdateChargeStatus([FromBody] PrescriptionChargeRequest par)
        {
            var response = CommonExecute<PrescriptionChargeRequest, DefaultResponse>((req, resp) =>
            {
                if (par.cfList != null)
                {
                    foreach (var item in par.cfList)
                    {
                        _IPrescriptionRepo.UpdateChargeStatus(UserIdentity.OrganizeId, item.cfh, par.sfbz, UserIdentity.Account);
                    }
                }
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(response);
        }

        /// <summary>Cập nhật dấu hiệu thu phí đơn thuốc (kênh API, không qua giải mã token).</summary>
        [HttpPost("UpdateChargeStatusApi")]
        [IgnoreTokenDecrypt]
        public IActionResult UpdateChargeStatusApi([FromBody] PrescriptionChargeRequest par)
        {
            par.TokenType = null;
            par.Token = null;
            var response = CommonExecute<PrescriptionChargeRequest, DefaultResponse>((req, resp) =>
            {
                if (par.cfList != null)
                {
                    foreach (var item in par.cfList)
                    {
                        _IPrescriptionRepo.UpdateChargeStatus(par.OrganizeId, item.cfh, par.sfbz, par.AppId);
                    }
                }
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(response);
        }

        /// <summary>Cập nhật dấu hiệu trả phí đơn thuốc.</summary>
        [HttpPost("UpdateChargeTbz")]
        [DefaultAuthorize]
        public IActionResult UpdateChargeTbz([FromBody] PrescriptionReturnChargeRequest par)
        {
            var response = CommonExecute<PrescriptionReturnChargeRequest, DefaultResponse>((req, resp) =>
            {
                if (par.cfList != null)
                {
                    foreach (var item in par.cfList)
                    {
                        _IPrescriptionRepo.UpdateChargeTbz(UserIdentity.OrganizeId, item, par.tbz, UserIdentity.Account);
                    }
                }
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(response);
        }
    }
}
