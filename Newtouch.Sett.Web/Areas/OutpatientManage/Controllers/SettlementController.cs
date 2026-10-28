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

    public class SettlementController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IOutPatChargeDmnService _outChargeDmnService;
        private readonly IOutPatientChargeQueryDmnService _outPatienChargeQueryDmnService;

        public SettlementController(IOutPatChargeDmnService outChargeDmnService, IOutPatientChargeQueryDmnService outPatienChargeQueryDmnService)
        {
            _outChargeDmnService = outChargeDmnService;
            _outPatienChargeQueryDmnService = outPatienChargeQueryDmnService;
        }

        #region Tra cứu quyết toán
        public IActionResult SettQuery()
        {
            return View();
        }

        public IActionResult GetSettlementGridJson(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, bool? containsTF = true)
        {
            // FIX-1 (Sett B3): the settlement service is a stub that returns
            // null, so list.Select(...) threw "Value cannot be null (Parameter
            // 'source')" on every load. Guard to an empty grid in the exact
            // jqGrid shape instead of erroring; no data is invented — a real
            // service is still needed before rows can ever appear.
            IList<OutpatientSettlementVO> list = _outChargeDmnService.GetSettlementList(pagination, this.OrganizeId, keyword, kssj, jssj, containsTF.Value)
                ?? new List<OutpatientSettlementVO>();
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };

            var jsnms = string.Join(",", list.Select(p => p.jsnm).Distinct());
            if (!string.IsNullOrWhiteSpace(jsnms))
            {
                var zffsList = _outPatienChargeQueryDmnService.GetSettZffsResultList(this.OrganizeId, jsnms)
                    ?? new List<SettZffsDto>();
                foreach (var js in list)
                {
                    js.zffsmcstr = string.Join(",", zffsList.Where(p => p.jsnm == js.jsnm).Select(p => p.xjzffsmc));
                }
            }

            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public IActionResult SettlementExportExcel(string keyword, DateTime? kssj, DateTime? jssj, int colStanWidth, bool? containsTF = true)
        { return Content("Xuất Excel chưa được triển khai trong bản .NET Core này (giai đoạn 1 chỉ xác minh đọc)."); }


        #endregion
    }
}
