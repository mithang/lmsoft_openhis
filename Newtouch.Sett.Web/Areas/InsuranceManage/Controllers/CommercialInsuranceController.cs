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

namespace Newtouch.Sett.Web.Areas.InsuranceManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("InsuranceManage")]

    public class CommercialInsuranceController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ICommercialInsuranceRepo _commercialInsuranceRepo;
        private readonly ISysCommercialInsuranceDmnService _sysCommercialInsuranceDmnService;
        public CommercialInsuranceController(ICommercialInsuranceRepo commercialInsuranceRepo, ISysCommercialInsuranceDmnService sysCommercialInsuranceDmnService)
        {
            _commercialInsuranceRepo = commercialInsuranceRepo;
            _sysCommercialInsuranceDmnService = sysCommercialInsuranceDmnService;
        }


        public IActionResult GetFormJson(string keyValue)
        {
            var data = _commercialInsuranceRepo.GetForm(keyValue,OrganizeId);
            return Content(data.ToJson());
        }

        public IActionResult SubmitForm(SysCommercialInsuranceVO entity, string keyValue)
        {
            entity.OrganizeId = OrganizeId;
            entity.zt = "1";
            _sysCommercialInsuranceDmnService.SubmitForm(entity, keyValue, OrganizeId);
            return Success("Thao tác thành công。");
        }

        public IActionResult GetListJson(string engName, string code)
        {
            var data = _commercialInsuranceRepo.GetListJson(OrganizeId, code, engName);
            return Content(data.ToJson());
        }

        public IActionResult DeleteForm(string keyValue)
        {
            _commercialInsuranceRepo.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }
    }
}
