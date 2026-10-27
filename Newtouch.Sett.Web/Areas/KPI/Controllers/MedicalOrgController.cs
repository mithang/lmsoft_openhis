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

    public class MedicalOrgController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IMonthProfitShareConfigDmnService _monthProfitShareConfigDmnService;
        public MedicalOrgController(IMonthProfitShareConfigDmnService monthProfitShareConfigDmnService)
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
            var list = _monthProfitShareConfigDmnService.GetMedicalOrgPSConfigList(this.OrganizeId, keyword);

            return Content(list.ToJson());

            var data = new List<MedicalOrgProfitShareConfigVO>();

            data.Add(new MedicalOrgProfitShareConfigVO()
            {
                Id = "2",
                sfdlmc = "kiểm tra phí",
                bl = 50,
                bz = "Thuật toán mặc định phí kiểm tra",
                CreatorCode = "bn001",
                CreateTime = new DateTime(2018, 1, 1),
                zt = "1"
            });

            data.Add(new MedicalOrgProfitShareConfigVO()
            {
                Id = "3",
                sfdlmc = "kiểm tra phí",
                bl = 50,
                sfxmCode = "00000170",
                sfxmmc = "MRI KNEE",
                xmgdcb = 300,
                bz = "",
                CreatorCode = "bn001",
                CreateTime = new DateTime(2018, 1, 1),
                zt = "1"
            });

            data.Add(new MedicalOrgProfitShareConfigVO()
            {
                Id = "4",
                sfdlmc = "kiểm tra phí",
                bl = 50,
                sfxmCode = "00000176",
                sfxmmc = "CT",
                xmgdcb = 150,
                bz = "",
                CreatorCode = "bn001",
                CreateTime = new DateTime(2018, 1, 1),
                zt = "1"
            });

            data.Add(new MedicalOrgProfitShareConfigVO()
            {
                Id = "5",
                sfdlmc = "kiểm tra phí",
                bl = 50,
                sfxmCode = "00000177",
                sfxmmc = "X-RAY",
                xmgdcb = 160,
                bz = "",
                CreatorCode = "bn001",
                CreateTime = new DateTime(2018, 1, 1),
                zt = "1"
            });

            data.Add(new MedicalOrgProfitShareConfigVO()
            {
                Id = "6",
                sfdlmc = "kiểm tra phí",
                bl = 0,
                sfxmCode = "00000115",
                sfxmmc = "CT ANGIOGRAM",
                xmgdcb = 0,
                blhgdje = 100,
                bz = "ANGIOGRAM tiền hoa hồng cố định 100",
                CreatorCode = "bn001",
                CreateTime = new DateTime(2018, 1, 1),
                LastModifierCode = "bn001",
                LastModifyTime = new DateTime(2018, 1, 5),
                zt = "1"
            });

            data.Add(new MedicalOrgProfitShareConfigVO()
            {
                Id = "9",
                sfdlmc = "Phí phục hồi chức năng",
                bl = 60,
                bz = "Thuật toán mặc định phí phục hồi chức năng",
                CreatorCode = "bn001",
                CreateTime = new DateTime(2018, 1, 1),
                zt = "1"
            });

            return Content(data.ToJson());
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
            var vo = _monthProfitShareConfigDmnService.GetMedicalOrgPSConfig(keyValue);

            return Content(vo.ToJson());
        }

        /// <summary>
        /// 提交医疗机构提成配置
        /// </summary>
        /// <param name="entity"></param>
        public IActionResult SubmitMedicalOrgPSConfig(MedicalOrgMonthProfitShareConfigEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _monthProfitShareConfigDmnService.SubmitMedicalOrgPSConfig(entity, keyValue);

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
            var flag = _monthProfitShareConfigDmnService.MedicalOrgPSCheckIsGenerated(this.OrganizeId, year, month);
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
            _monthProfitShareConfigDmnService.DoGenerateMedicalOrgPS(this.OrganizeId, this.UserIdentity.UserCode, year, month, isReGene);
            return Success();
        }

    }
}
