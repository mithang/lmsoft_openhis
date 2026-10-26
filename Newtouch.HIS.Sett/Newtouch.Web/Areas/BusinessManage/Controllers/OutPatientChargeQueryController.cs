
using Newtouch.HIS.Application.Interface.BusinessManage;
using Newtouch.Tools;
using System;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.BusinessManage.Controllers
{
    public class OutPatientChargeQueryController : ControllerBase
    {
        // GET: BusinessManage/OutPatienChargeQuery
        private readonly IOutPatienChargeQueryApp _outPatienChargeQueryApp;
        public OutPatientChargeQueryController(IOutPatienChargeQueryApp outPatienChargeQueryApp)
        {
            this._outPatienChargeQueryApp = outPatienChargeQueryApp;
        }

        /// <summary>
        /// Đăng ký khám tra cứu thu phí
        /// </summary>
        /// <returns></returns>
        public ActionResult OutPatientChargeQuery()
        {
            return View();
        }
        /// <summary>
        /// Đăng ký khám tra cứu điều kiện thu phí
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <param name="xm"></param>
        /// <param name="CreatorCode"></param>
        /// <param name="CreateTimestart"></param>
        /// <param name="CreateTimeEnd"></param>
        /// <returns></returns>
        public ActionResult SelectRegChargeQuery(string kh = "", string fph="", string xm="", string syy = "", string CreateTimestart="", string CreateTimeEnd="")
        {
            var chargeQueryList = _outPatienChargeQueryApp.SelectRegChargeQuery(kh, fph, xm, syy, CreateTimestart, CreateTimeEnd);
            return Content(chargeQueryList.ToJson());
        }
        /// <summary>
        /// Đăng ký khám chi tiết tra cứu thu phí
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public ActionResult GetRecordsByJsnm(string jsnm)
        {
            var regChargeDetailList = _outPatienChargeQueryApp.GetRecordsByJsnm(jsnm);
            return Success("", regChargeDetailList);
        }


    }
}