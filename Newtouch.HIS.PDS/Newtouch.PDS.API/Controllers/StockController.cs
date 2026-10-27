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
                resp.msg = _drugStorageApp.PrepareMedicine(req.yplist, req.OrganizeId, req.yhgh);
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }

        [HttpPost("WithdrawalPreparation")]
        public IActionResult WithdrawalPreparation([FromBody] WithdrawalPrepareDTO par)
        {
            var response = CommonExecute<WithdrawalPrepareDTO, DefaultResponse>((req, resp) =>
            {
                resp.msg = _drugStorageApp.WithdrawalPreparation(req.Djh, req.OrganizeId, req.yhgh, par.shzt);
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }

        [HttpPost("PrepareMedicineReturn")]
        public IActionResult PrepareMedicineReturn([FromBody] PrepareMedicineReturnDTO par)
        {
            var response = CommonExecute<PrepareMedicineReturnDTO, DefaultResponse>((req, resp) =>
            {
                resp.msg = _drugStorageApp.PrepareMedicineReturn(req.yplist, req.OrganizeId, req.yhgh);
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }

        [HttpPost("WithdrawalPreparationReturn")]
        public IActionResult WithdrawalPreparationReturn([FromBody] WithdrawalPrepareDTO par)
        {
            var response = CommonExecute<WithdrawalPrepareDTO, DefaultResponse>((req, resp) =>
            {
                resp.msg = _drugStorageApp.WithdrawalPreparationReturn(req.Djh, req.OrganizeId, req.yhgh, par.shzt);
                resp.code = ResponseResultCode.SUCCESS;
            }, par);
            return CreateResponse(par, response);
        }
    }
}
