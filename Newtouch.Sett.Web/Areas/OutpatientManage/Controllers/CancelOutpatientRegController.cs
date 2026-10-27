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

    public class CancelOutpatientRegController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ICancelOutpatientRegDmnService _cancelOutpatientRegDmnService;

        public CancelOutpatientRegController(ICancelOutpatientRegDmnService cancelOutpatientRegDmnService)
        {
            _cancelOutpatientRegDmnService = cancelOutpatientRegDmnService;
        }

        // GET: OutpatientManage/CancelRegister
        public IActionResult CancelRegister()
        {
            return View();
        }

        /// <summary>
        /// tra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="blh"></param>
        /// <param name="xm"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public IActionResult SelectRegisterlist(Pagination pagination, string blh, string xm, DateTime? kssj, DateTime? jssj)
        {
            var list = new
            {
                rows = _cancelOutpatientRegDmnService.SelectRegisterlist(pagination, this.OrganizeId, blh, xm, kssj, jssj),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// hủyđăng ký khám
        /// </summary>
        /// <param name="ghnm"></param>
        /// <returns></returns>
        public IActionResult SaveCancelRegister(List<SaveCancelRegisterGhnmVO> list)
        {
            _cancelOutpatientRegDmnService.SaveCancelRegister(this.OrganizeId, list);
            return Success();
        }
    }
}
