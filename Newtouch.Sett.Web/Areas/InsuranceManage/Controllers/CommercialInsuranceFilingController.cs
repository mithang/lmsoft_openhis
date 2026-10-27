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

    public class CommercialInsuranceFilingController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysCommercialInsuranceDmnService _sysCommercialInsuranceDmnService;
        private readonly ISysCommercialInsuranceFilingRepo _sysCommercialInsuranceFilingRepo;
        public CommercialInsuranceFilingController(ISysCommercialInsuranceDmnService sysCommercialInsuranceDmnService, ISysCommercialInsuranceFilingRepo sysCommercialInsuranceFilingRepo)
        {
            _sysCommercialInsuranceDmnService = sysCommercialInsuranceDmnService;
            _sysCommercialInsuranceFilingRepo = sysCommercialInsuranceFilingRepo;
        }


        /// <summary>
        /// 获取商保备案列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult SelectCommercialInsuranceFilingList(string keyword)
        {
            var orgId = OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var data = _sysCommercialInsuranceDmnService.SelectCommercialInsuranceFilingList(keyword, orgId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sbbabId"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysCommercialInsuranceFilingEntity entity, string sbbabId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = OrganizeId;
            _sysCommercialInsuranceFilingRepo.SubmitForm(entity, sbbabId);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="sbbabId"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string sbbabId)
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var entity = _sysCommercialInsuranceDmnService.SelectCommercialInsuranceFilingList(null, orgId, sbbabId).FirstOrDefault();
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sbbabId"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string sbbabId)
        {
            var orgId = OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            _sysCommercialInsuranceFilingRepo.DeleteForm(sbbabId, orgId);
            return Success("Thao tác thành công。");
        }
    }
}
