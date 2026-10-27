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

namespace Newtouch.Sett.Web.Areas.KPI.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("KPI")]

    public class TherapeutistController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IMonthProfitShareConfigDmnService _monthProfitShareConfigDmnService;
        public TherapeutistController(IMonthProfitShareConfigDmnService monthProfitShareConfigDmnService)
        {
            _monthProfitShareConfigDmnService = monthProfitShareConfigDmnService;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IActionResult Index()
        {
            ViewBag.ReportServerHOST = "";

            return View();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <returns></returns>
        public IActionResult ConfigIndex()
        {
            return View();
        }

        /// <summary>
        /// 获取配置列表 list json
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetConfigGridJson(string keyword)
        {
            var list = _monthProfitShareConfigDmnService.GetTherapeutistPSConfigList(this.OrganizeId, keyword);

            return Content(list.ToJson());
        }

        /// <summary>
        /// 配置表单
        /// </summary>
        /// <returns></returns>
        public IActionResult ConfigForm()
        {
            return View();
        }

        /// <summary>
        /// 修改信息时，把信息带到新页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetConfigFormJson(string keyValue)
        {
            var vo = _monthProfitShareConfigDmnService.GetTherapeutistPSConfig(keyValue);

            return Content(vo.ToJson());
        }

        /// <summary>
        /// 提交治疗师提成配置
        /// </summary>
        /// <param name="entity"></param>
        public IActionResult SubmitTherapeutistPSConfig(TherapeutistMonthProfitShareConfigEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _monthProfitShareConfigDmnService.SubmitTherapeutistPSConfig(entity, keyValue);

            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// Check报表是否已经生成过
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>

        public IActionResult CheckIsGenerated(int year, int month)
        {
            var flag = _monthProfitShareConfigDmnService.TherapeutistPSCheckIsGenerated(this.OrganizeId, year, month);
            var res = new { flag = flag };
            return Content(res.ToJson());
        }

        /// <summary>
        /// 生成报表
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>

        public IActionResult DoGenerate(int year, int month, bool? isReGene)
        {
            _monthProfitShareConfigDmnService.DoGenerateTherapeutistPS(this.OrganizeId, this.UserIdentity.UserCode, year, month, isReGene);
            return Success();
        }

    }
}
