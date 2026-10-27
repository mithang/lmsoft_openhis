using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;









namespace Newtouch.Sett.Web.Areas.OutpatientManage.Controllers
{   
    /// <summary>
    /// 
    /// </summary>
    [Area("OutpatientManage")]

    public class PayController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IOrderPayInfoRepo _OrderPayInfoRepo;

        public PayController(IOrderPayInfoRepo OrderPayInfoRepo)
        {
            _OrderPayInfoRepo = OrderPayInfoRepo;
        }

        /// <summary>
        /// thanh toán bằng mã thanh toán
        /// </summary>
        /// <returns></returns>
        public IActionResult MicroPay()
        {
            return View();
        }

        public IActionResult MicroPayQuery()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="subject"></param>
        /// <param name="total_amount"></param>
        /// <param name="auth_code"></param>
        /// <returns></returns>
        public IActionResult MicroPaySubmit(string out_trade_no, string subject, string total_amount, string auth_code)
        {
            // STUB: NewtouchPay SDK (Alipay/WeChat micropay) is not available in the .NET Core port.
            return Error("Thanh toán bằng mã quét chưa được triển khai trong bản .NET Core này.");
        }



    }
}
