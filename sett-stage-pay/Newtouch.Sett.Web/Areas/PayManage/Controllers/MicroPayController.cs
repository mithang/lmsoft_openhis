using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Web.Controllers;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;

namespace Newtouch.Sett.Web.Areas.PayManage.Controllers
{
    /// <summary>
    /// Micro-payment (barcode / scan-to-pay) query pages, ported from the
    /// Framework MicroPayController. Unlike the Framework original, whose
    /// readonly service fields had NO constructor (CS0649 — they would stay
    /// null), this port injects every used service explicitly and registers
    /// them in Program.cs DI.
    /// Live gateway actions (MicroPaySubmit, Opr_TradeRefund) are explicit
    /// error stubs: the Alipay/WeChat SDK (NewtouchPay.dll, .NET Framework
    /// only) and the NxPay aggregator endpoint are not available in this
    /// .NET 6 port, so they never return a fake success and never write.
    /// </summary>
    [Area("PayManage")]
    public class MicroPayController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IOrderPayInfoRepo _OrderPayInfoRepo;
        private readonly IPayDmnService _PayDmnService;
        private readonly IPayApp _PayApp;

        public MicroPayController(IOrderPayInfoRepo orderPayInfoRepo, IPayDmnService payDmnService, IPayApp payApp)
        {
            _OrderPayInfoRepo = orderPayInfoRepo;
            _PayDmnService = payDmnService;
            _PayApp = payApp;
        }

        public IActionResult MicroPay()
        {
            return View();
        }

        public IActionResult MicroPayQuery()
        {
            return View();
        }

        public IActionResult TradeInfo()
        {
            return View();
        }

        public IActionResult TradeRefund()
        {
            return View();
        }

        /// <summary>
        /// STUB — barcode payment submission. The Framework version posted to
        /// the NxPay aggregator (qr-barcode-pay-request) or called the
        /// Alipay/WeChat SDK (TradeHelper.Pay) and then wrote Order_PayInfo.
        /// Neither gateway is ported here, so this returns an explicit error
        /// JSON and performs no payment and no database write.
        /// </summary>
        public IActionResult MicroPaySubmit(string out_trade_no, string subject, string total_amount, string auth_code, string djjesszffs, string patid)
        {
            return Error("Thanh toán thất bại: thanh toán qua mã vạch (Alipay/WeChat/NxPay) chưa khả dụng trong bản port .NET 6 này; không thực hiện thanh toán và không ghi dữ liệu.");
        }

        public IActionResult MicroTradeQuery(Pagination pagination, string ksrq, string jsrq, int? payType, int? payStatus, string keywords)
        {
            var data = new
            {
                rows = _PayDmnService.TradePayLsit(pagination, ksrq, jsrq, payType, payStatus, keywords, this.OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public IActionResult TradeRefundList(string outTradeNo, string tradeNo)
        {
            var data = new
            {
                rows = _PayDmnService.TradeRefundList(outTradeNo, tradeNo)
            };
            return Content(data.ToJson());
        }

        public IActionResult Opr_TradeRefund(string outTradeNo, decimal refundAmount, string Reason, string OutRequestNo = null)
        {
            bool? isTradeRefundError = null;
            if (!string.IsNullOrWhiteSpace(outTradeNo) && refundAmount > 0)
            {
                string errorMsg;
                var refundReuslt = _PayApp.TradeRefund(outTradeNo, refundAmount, Reason, OutRequestNo, out errorMsg);
                isTradeRefundError = refundReuslt == (int)Newtouch.Sett.Infrastructure.EnumRefundStatus.Failed || refundReuslt == (int)Newtouch.Sett.Infrastructure.EnumRefundStatus.UnKnown;
                if (isTradeRefundError == true)
                {
                    return Error("Hoàn tiền thất bại, vui lòng thử lại");
                }
                else
                {
                    return Success("Hoàn tiền thành công");
                }
            }
            return Error("Yêu cầu thất bại, vui lòng kiểm tra dữ liệu");
        }

        public IActionResult GetTradeInfo(string outTradeNo)
        {
            var tradeEty = _PayDmnService.TradePayInfobyNo(outTradeNo, "");
            // NOTE: the Framework version enriched TradeStatus via a live
            // gateway query (TradeHelper.TradeQuery, Alipay/WeChat SDK). That
            // gateway is not ported, so the stored record is returned as-is;
            // TradeStatus stays empty when no gateway status is available.
            if (tradeEty == null)
            {
                return Content("null");
            }
            return Content(tradeEty.ToJson());
        }
    }
}
