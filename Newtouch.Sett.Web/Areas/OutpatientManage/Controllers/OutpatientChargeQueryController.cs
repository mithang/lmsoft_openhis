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

    public class OutpatientChargeQueryController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        // GET: BusinessManage/OutPatienChargeQuery
        private readonly IOutPatienChargeQueryApp _outPatienChargeQueryApp;
        private readonly IOutPatientChargeQueryDmnService _outPatienChargeQueryDmnService;

        public OutpatientChargeQueryController(IOutPatienChargeQueryApp outPatienChargeQueryApp, IOutPatientChargeQueryDmnService outPatienChargeQueryDmnService)
        {
            _outPatienChargeQueryApp = outPatienChargeQueryApp;
            _outPatienChargeQueryDmnService = outPatienChargeQueryDmnService;
        }

        /// <summary>
        /// thu phí ngoại trútra cứu
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientChargeQuery()
        {
            //ViewBag.ReportServerHOST = "";
            //ViewBag.OrgId = this.OrganizeId;

            //return View();
            ViewBag.ReportServerHOST = "";
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
            return View();
        }

        public IActionResult OutpatientSettQuery()
        {
            return View();
        }

        /// <summary>
        /// thu phí đăng ký khámtra cứu theo điều kiện
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <param name="xm"></param>
        /// <param name="pagination"></param>
        /// <param name="createTimestart"></param>
        /// <param name="createTimeEnd"></param>
        /// <param name="syy"></param>
        /// <returns></returns>
                public IActionResult SelectRegChargeQuery(Pagination pagination, DateTime? createTimestart, DateTime? createTimeEnd, DateTime? sfrqTimestart, DateTime? sfrqTimeEnd, string kh = "", string fph = "", string jsfph = "", string xm = "", string syy = "", string zxlsh = "")
        {
            IList<OutPatientRegChargeMVO> list;
            var chargeQueryList = new
            {
                rows = (list = _outPatienChargeQueryDmnService.RegChargeQuery(pagination, kh, fph, jsfph, xm, syy, createTimestart, createTimeEnd, sfrqTimestart, sfrqTimeEnd, zxlsh)),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };

            var jsnms = string.Join(",", list.Select(p => p.jsnm).Distinct());
            if (!string.IsNullOrWhiteSpace(jsnms))
            {
                var zffsList = _outPatienChargeQueryDmnService.GetSettZffsResultList(this.OrganizeId, jsnms);
                foreach (var js in list)
                {
                    js.zffsmcstr = string.Join(",", zffsList.Where(p => p.jsnm == js.jsnm).Select(p => p.xjzffsmc));
                }
            }

            return Content(chargeQueryList.ToJson());
        }

        /// <summary>
        /// chờtải lêncủangoại trúbệnh nhân tự chi trả khámquyết toánthông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="xm"></param>
        /// <param name="createTimestart"></param>
        /// <param name="createTimeEnd"></param>
        /// <returns></returns>
                public IActionResult SelectZFRegChargeQuery(Pagination pagination, DateTime? createTimestart, DateTime? createTimeEnd, string xm = "")
        {
            IList<OutPatientRegChargeMVO> list;
            var chargeQueryList = new
            {
                rows = (list = _outPatienChargeQueryDmnService.ZFRegChargeQuery(pagination, xm, createTimestart, createTimeEnd)),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };

            var jsnms = string.Join(",", list.Select(p => p.jsnm).Distinct());
            if (!string.IsNullOrWhiteSpace(jsnms))
            {
                var zffsList = _outPatienChargeQueryDmnService.GetSettZffsResultList(this.OrganizeId, jsnms);
                foreach (var js in list)
                {
                    js.zffsmcstr = string.Join(",", zffsList.Where(p => p.jsnm == js.jsnm).Select(p => p.xjzffsmc));
                }
            }

            return Content(chargeQueryList.ToJson());
        }

        /// <summary>
        /// thu phí đăng ký khámtra cứu chi tiết
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
                public IActionResult GetRecordsByJsnm(string jsnm)
        {
            return Success(null, _outPatienChargeQueryApp.ChargeRecordsQuery(jsnm));
        }

        /// <summary>
        /// in lại/in bổ sung hóa đơn
        /// </summary>
        /// <returns></returns>
        public IActionResult OutpatientReprintOrSupplementPrint()
        {
            return View();
        }

        /// <summary>
        /// in lại/in bổ sung hóa đơn mz_js List
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="jsnm"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
                public IActionResult LoadMzjsRecords(Pagination pagination, string jsnm, DateTime? startDate, DateTime? endDate, string kh, string yfph)
        {
            var chargeQueryList = new
            {
                rows = _outPatienChargeQueryApp.LoadMzjsRecords(pagination, jsnm, startDate, endDate, kh, yfph),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(chargeQueryList.ToJson());
        }
        /// <summary>
        /// in lại/in bổ sung hóa đơn mz_jsmx List
        /// </summary>
        /// <param name="jsnmStr"></param>
        /// <returns></returns>
                public IActionResult LoadMzjsMXRecords(string jsnmStr)
        {
            OutPatientReprintOrSuppPrintSettleDetailDto settleDetailDto = _outPatienChargeQueryApp.LoadMzjsMXRecords(jsnmStr);
            return Success(null, settleDetailDto);

        }

        /// <summary>
        /// in chi tiết
        /// </summary>
        /// <param name="jsnmStr"></param>
        /// <returns></returns>
                public IActionResult PrintMxData(string jsnmStr)
        {
            _outPatienChargeQueryApp.PrintMxData(jsnmStr);
            return Success();
        }

        /// <summary>
        /// kiểm tralàkhôngcócó thể incủadữ liệu
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
                public IActionResult CheckPintInfo(string jsnm)
        {
            _outPatienChargeQueryApp.CheckPintInfo(jsnm);
            return Success();
        }

        /// <summary>
        /// in hóa đơn
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="pageFph"></param>
        /// <returns></returns>
                public IActionResult PrintInvoice(string jsnm, bool isGh)
        {
            _outPatienChargeQueryApp.PrintInvoice(jsnm, isGh);
            return Success();
        }

        /// <summary>
        /// in bổ sung
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
                public IActionResult SupplementPrint(string jsnm, bool isGh)
        {
            _outPatienChargeQueryApp.SupplementPrint(jsnm, isGh);
            return Success();
        }

        /// <summary>
        /// in lại
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="pageFph"></param>
        /// <returns></returns>
                public IActionResult RePrint(string jsnm, string pageFph, bool isGh)
        {
            _outPatienChargeQueryApp.RePrint(jsnm, pageFph, isGh);
            return Success();
        }


    }
}
