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
    /// Authorize như thường. BA-002: cả 5 action nay mang [DefaultAuthorize] nên
    /// request không token hợp lệ không còn qua khi chỉ có Timestamp.
    /// GHI CHÚ 2: các action PrepareMedicine... gán resp.msg (không phải data) như gốc —
    /// ResponseBase của bản port có trường msg chính vì vậy.
    /// BA-002 (F-API-01): cả 5 action gắn [DefaultAuthorize] — trước đây request
    /// không token vẫn qua khi có Timestamp (lỗ hổng anon 5337). Caller thật duy
    /// nhất là CIS (SiteYfykAPIHelper, đã chuyển sang gửi token: xem các call-site
    /// đã bỏ autoAppendToken:false) với token JWT app của AuthCenter 8015 qua
    /// header Bearer; ApiControllerBase.Authorize nay đọc header đó và kiểm chứng
    /// JWT bằng cache Redis DB1 của AuthCenter (TokenStore.GetIdentityFromAuthCenterJwt),
    /// token GUID nội bộ (api/Auth/GetToken) vẫn tra DB0. TRIỂN KHAI NGUYÊN TỬ:
    /// phải deploy PDS.API + CIS (+ Sett cho ResourcesOperate) cùng một đợt — chỉ
    /// deploy PDS.API mà CIS chưa gửi token sẽ phá cả 10 luồng (TOKEN_IS_REQUIRED);
    /// chỉ deploy CIS thì vô hại (PDS.API cũ bỏ qua header). Không còn phương án
    /// localhost-trust: cổng 5337 vẫn bind 127.0.0.1 nhưng đó chỉ là lớp mạng,
    /// không thay cho auth.
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
        [DefaultAuthorize]
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
        [DefaultAuthorize]
        public IActionResult PrepareMedicine([FromBody] PrepareMedicineDTO par)
        {
            var response = CommonExecute<PrepareMedicineDTO, DefaultResponse>((req, resp) =>
            {
                ApplyStockResult(resp, _drugStorageApp.PrepareMedicine(req.yplist, req.OrganizeId, req.yhgh));
            }, par);
            return CreateResponse(par, response);
        }

        [HttpPost("WithdrawalPreparation")]
        [DefaultAuthorize]
        public IActionResult WithdrawalPreparation([FromBody] WithdrawalPrepareDTO par)
        {
            var response = CommonExecute<WithdrawalPrepareDTO, DefaultResponse>((req, resp) =>
            {
                ApplyStockResult(resp, _drugStorageApp.WithdrawalPreparation(req.Djh, req.OrganizeId, req.yhgh, par.shzt));
            }, par);
            return CreateResponse(par, response);
        }

        [HttpPost("PrepareMedicineReturn")]
        [DefaultAuthorize]
        public IActionResult PrepareMedicineReturn([FromBody] PrepareMedicineReturnDTO par)
        {
            var response = CommonExecute<PrepareMedicineReturnDTO, DefaultResponse>((req, resp) =>
            {
                ApplyStockResult(resp, _drugStorageApp.PrepareMedicineReturn(req.yplist, req.OrganizeId, req.yhgh));
            }, par);
            return CreateResponse(par, response);
        }

        [HttpPost("WithdrawalPreparationReturn")]
        [DefaultAuthorize]
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
