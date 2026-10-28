using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Application.Interface;
using Newtouch.PDS.API.Envelope;
using Newtouch.PDS.API.Services;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.Stock;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// Port của api/Stock (tra cứu tồn kho, xếp thuốc, hủy xếp thuốc...).
    /// SỬA BUG GỐC: _drugStorageApp ở bản Framework được khai báo nhưng không bao giờ
    /// được gán (ctor chỉ nhận IComponentContext) — mọi action gốc đều sẽ NullReference.
    /// Bản Core inject IDrugStorageApp qua DI nên các action chạy thật.
    /// GHI CHÚ: các action gốc mang [HandlerAuthorizeIgnore] — đó là attribute của MVC
    /// (Web.Core), không có tác dụng trong pipeline Web API; CommonExecute vẫn chạy
    /// Authorize như thường. Không action nào mang [DefaultAuthorize] nên request không
    /// token vẫn qua khi có Timestamp, đúng hành vi gốc.
    /// GHI CHÚ 2: các action PrepareMedicine... gán resp.msg (không phải data) như gốc —
    /// ResponseBase của bản port có trường msg chính vì vậy.
    /// QA PHASE 2 (API-03/API-07a): (a) KHÔNG gắn [DefaultAuthorize] ở controller
    /// này — caller thật là CIS Web/DomainServices gọi với autoAppendToken: false
    /// (xếp/hủy xếp thuốc từ luồng duyệt y lệnh, không mang token user); gắn auth
    /// sẽ phá luồng đó. Bề mặt này phải chặn ở tầng mạng (chỉ host CIS được tới
    /// PDS API), xem báo cáo qa-phase2/api-auth.md. (b) Domain service nuốt
    /// exception và trả ex.Message làm chuỗi kết quả ("" = thành công) — trước
    /// đây controller gán code SUCCESS vô điều kiện nên NRE trả về 10000 kèm
    /// text exception; nay chuỗi khác rỗng → code FAIL (NRE → thông điệp chung).
    /// </summary>
    [ApiController]
    [Route("api/Stock")]
    public class StockController : ApiControllerBase
    {
        private readonly IDrugStorageApp _drugStorageApp;

        public StockController(TokenStore tokenStore, IDrugStorageApp drugStorageApp)
            : base(tokenStore)
        {
            _drugStorageApp = drugStorageApp;
        }

        [HttpPost("query")]
        public IActionResult Query([FromBody] StockQueryRequestDTO par)
        {
            var response = CommonExecute<StockQueryRequestDTO, DefaultResponse>((req, resp) =>
            {
                resp.data = _drugStorageApp.StockQuery(req.yplist, req.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }

        [HttpPost("PrepareMedicine")]
        public IActionResult PrepareMedicine([FromBody] PrepareMedicineDTO par)
        {
            var response = CommonExecute<PrepareMedicineDTO, DefaultResponse>((req, resp) =>
            {
                ApplyStockResult(resp, _drugStorageApp.PrepareMedicine(req.yplist, req.OrganizeId, req.yhgh));
            }, par);
            return CreateResponse(par, response);
        }

        [HttpPost("WithdrawalPreparation")]
        public IActionResult WithdrawalPreparation([FromBody] WithdrawalPrepareDTO par)
        {
            var response = CommonExecute<WithdrawalPrepareDTO, DefaultResponse>((req, resp) =>
            {
                ApplyStockResult(resp, _drugStorageApp.WithdrawalPreparation(req.Djh, req.OrganizeId, req.yhgh, par.shzt));
            }, par);
            return CreateResponse(par, response);
        }

        [HttpPost("PrepareMedicineReturn")]
        public IActionResult PrepareMedicineReturn([FromBody] PrepareMedicineReturnDTO par)
        {
            var response = CommonExecute<PrepareMedicineReturnDTO, DefaultResponse>((req, resp) =>
            {
                ApplyStockResult(resp, _drugStorageApp.PrepareMedicineReturn(req.yplist, req.OrganizeId, req.yhgh));
            }, par);
            return CreateResponse(par, response);
        }

        [HttpPost("WithdrawalPreparationReturn")]
        public IActionResult WithdrawalPreparationReturn([FromBody] WithdrawalPrepareDTO par)
        {
            var response = CommonExecute<WithdrawalPrepareDTO, DefaultResponse>((req, resp) =>
            {
                ApplyStockResult(resp, _drugStorageApp.WithdrawalPreparationReturn(req.Djh, req.OrganizeId, req.yhgh, par.shzt));
            }, par);
            return CreateResponse(par, response);
        }

        /// <summary>Quy ước của domain service: "" = thành công, chuỗi khác rỗng
        /// = thông điệp lỗi (exception đã bị nuốt ở tầng service). Map đúng sang
        /// code FAIL; riêng text NullReference (lỗi lập trình/dữ liệu null, vd.
        /// yplist null) thay bằng thông điệp chung, không rò tên exception.</summary>
        private static void ApplyStockResult(DefaultResponse resp, string result)
        {
            if (string.IsNullOrWhiteSpace(result))
            {
                resp.msg = "";
                resp.code = ResponseResultCode.SUCCESS;
                return;
            }
            resp.msg = result.Contains("Object reference not set")
                ? "Thao tác kho thất bại, vui lòng kiểm tra dữ liệu và thử lại"
                : result;
            resp.sub_code = "FAILED";
            resp.code = ResponseResultCode.FAIL;
        }
    }
}
