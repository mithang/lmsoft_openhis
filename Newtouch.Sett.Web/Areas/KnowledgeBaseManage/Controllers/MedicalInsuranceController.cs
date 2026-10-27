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

namespace Newtouch.Sett.Web.Areas.KnowledgeBaseManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("KnowledgeBaseManage")]

    public class MedicalInsuranceController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IMedicalInsuranceDmnService _medicalInsuranceDmnService;
        private readonly ISysMedicalInsuranceFilingRepo _sysMedicalInsuranceFilingRepo;
        public MedicalInsuranceController(IMedicalInsuranceDmnService medicalInsuranceDmnService, ISysMedicalInsuranceFilingRepo sysMedicalInsuranceFilingRepo)
        {
            _medicalInsuranceDmnService = medicalInsuranceDmnService;
            _sysMedicalInsuranceFilingRepo = sysMedicalInsuranceFilingRepo;
        }


        // GET: KnowledgeBaseManage/MedicalInsurance
        public override IActionResult Index()
        {
            return View();
        }

        public override IActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 获取医保备案列表
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <returns></returns>
        public IActionResult SelectMedicalInsuranceFilingList(Pagination pagination, string keyword)
        {
            var list = new
            {
                rows = _medicalInsuranceDmnService.SelectMedicalInsuranceFilingList(pagination, keyword, this.OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ybbabId"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysMedicalInsuranceFilingEntity entity, string ybbabId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _sysMedicalInsuranceFilingRepo.SubmitForm(entity, ybbabId);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="ybbabId"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string ybbabId)
        {
            Pagination pagination = new Pagination();
            pagination.sidx = "CreateTime desc";
            pagination.rows = 1;
            pagination.page = 1;
            var entity = _medicalInsuranceDmnService.SelectMedicalInsuranceFilingList(pagination, null, this.OrganizeId, ybbabId).FirstOrDefault();
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ybbabId"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string ybbabId)
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            _sysMedicalInsuranceFilingRepo.DeleteForm(ybbabId, orgId);
            return Success("Thao tác thành công。");
        }
    }
}
