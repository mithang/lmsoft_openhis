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

    [Area("SystemManage")]

    public class FinancialReceiptController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysFinancialDmnService _sysFinancialDmnService;
        private readonly IFinanceReceiptRepo _financeReceiptRepo;
        public FinancialReceiptController(ISysFinancialDmnService sysFinancialDmnService, IFinanceReceiptRepo financeReceiptRepo)
        {
            _sysFinancialDmnService = sysFinancialDmnService;
            _financeReceiptRepo = financeReceiptRepo;
        }

        /// <summary>
        /// Lấy danh sách hiệu lực
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetFinancialInvoiceList(string keyword)
        {
            var data = _sysFinancialDmnService.GetFinancialInvoiceList(keyword, this.OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string Id)
        {
            var entity = _financeReceiptRepo.GetFinancialInvoiceEntity(Id, this.OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="Entity"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(FinanceReceiptEntity Entity, string Id)
        {
            Entity.zt = Entity.zt == "true" ? "1" : "0";
            Entity.OrganizeId = this.OrganizeId;
            _financeReceiptRepo.VlidateRepeat(Entity, OrganizeId,Id);
            _financeReceiptRepo.SubmitForm(Entity, Id);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string Id)
        {
            _financeReceiptRepo.DeleteForm(Id, this.OrganizeId);
            return Success("Thao tác thành công。");
        }
    }
}
