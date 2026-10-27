using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.SystemManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("SystemManage")]

    public class FinancialInvoiceController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IFinancialInvoiceRepo _financialInvoiceRepo;
        private readonly ISysFinancialDmnService _sysFinancialDmnService;
        public FinancialInvoiceController(IFinancialInvoiceRepo financialInvoiceRepo, ISysFinancialDmnService sysFinancialDmnService)
        {
            _financialInvoiceRepo = financialInvoiceRepo;
            _sysFinancialDmnService = sysFinancialDmnService;
        }


        public IActionResult FinanicalInvoiceIndex()
        {
            return View();
        }

        public IActionResult InvoiceQuery()
        {
            return View();
        }

        //[HandlerAuthorize]
        public override IActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetFinancialInvoiceList(string keyword)
        {
            var data = _financialInvoiceRepo.GetFinancialInvoiceList(keyword, this.OrganizeId);
            return Content(data.ToJson());
        }
        public IActionResult GetCwfpList(string keyword)
        {
            var data = _sysFinancialDmnService.GetCwfpList(keyword, this.OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string fpdm)
        {
            var entity = _financialInvoiceRepo.GetFinancialInvoiceEntity(fpdm, this.OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(FinancialInvoiceEntity financialInvoiceEntity, string fpdm)
        {
            financialInvoiceEntity.zt = financialInvoiceEntity.zt == "true" ? "1" : "0";
            financialInvoiceEntity.OrganizeId = this.OrganizeId;
            _financialInvoiceRepo.VlidateRepeat(financialInvoiceEntity, OrganizeId,fpdm);
            _financialInvoiceRepo.SubmitForm(financialInvoiceEntity, fpdm);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string fpdm)
        {
            _financialInvoiceRepo.DeleteForm(fpdm, this.OrganizeId);
            return Success("Thao tác thành công。");
        }
    }
}
