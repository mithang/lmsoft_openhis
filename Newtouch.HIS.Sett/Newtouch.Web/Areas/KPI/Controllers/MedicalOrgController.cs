using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.KPI;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.KPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class MedicalOrgController : ControllerBase
    {
        private readonly IMonthProfitShareConfigDmnService _monthProfitShareConfigDmnService;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override ActionResult Index()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");

            return View();
        }

        /// <summary>
        /// Cấu hình
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfigIndex()
        {
            return View();
        }

        /// <summary>
        /// Lấy danh sách cấu hình list json
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetConfigGridJson(string keyword)
        {
            var list = _monthProfitShareConfigDmnService.GetMedicalOrgPSConfigList(this.OrganizeId, keyword);

            return Content(list.ToJson());

            var data = new List<MedicalOrgProfitShareConfigVO>();

            data.Add(new MedicalOrgProfitShareConfigVO()
            {
                Id = "2",
                sfdlmc = "检查费",
                bl = 50,
                bz = "检查费默认算法",
                CreatorCode = "bn001",
                CreateTime = new DateTime(2018, 1, 1),
                zt = "1"
            });

            data.Add(new MedicalOrgProfitShareConfigVO()
            {
                Id = "3",
                sfdlmc = "检查费",
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
                sfdlmc = "检查费",
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
                sfdlmc = "检查费",
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
                sfdlmc = "检查费",
                bl = 0,
                sfxmCode = "00000115",
                sfxmmc = "CT ANGIOGRAM",
                xmgdcb = 0,
                blhgdje = 100,
                bz = "ANGIOGRAM固定提成金额100",
                CreatorCode = "bn001",
                CreateTime = new DateTime(2018, 1, 1),
                LastModifierCode = "bn001",
                LastModifyTime = new DateTime(2018, 1, 5),
                zt = "1"
            });

            data.Add(new MedicalOrgProfitShareConfigVO()
            {
                Id = "9",
                sfdlmc = "康复费",
                bl = 60,
                bz = "康复费默认算法",
                CreatorCode = "bn001",
                CreateTime = new DateTime(2018, 1, 1),
                zt = "1"
            });

            return Content(data.ToJson());
        }

        /// <summary>
        /// Cấu hình đơn bảng
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfigForm()
        {
            return View();
        }

        /// <summary>
        /// Khi sửa thông tin，Đưa thông tin sang trang mới
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetConfigFormJson(string keyValue)
        {
            var vo = _monthProfitShareConfigDmnService.GetMedicalOrgPSConfig(keyValue);

            return Content(vo.ToJson());
        }

        /// <summary>
        /// 提交医疗机构提成配置
        /// </summary>
        /// <param name="entity"></param>
        public ActionResult SubmitMedicalOrgPSConfig(MedicalOrgMonthProfitShareConfigEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _monthProfitShareConfigDmnService.SubmitMedicalOrgPSConfig(entity, keyValue);

            return Success("操作成功。");
        }

        /// <summary>
        /// CheckBáo cáo có tạo đã không
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult CheckIsGenerated(int year, int month)
        {
            var flag = _monthProfitShareConfigDmnService.MedicalOrgPSCheckIsGenerated(this.OrganizeId, year, month);
            var res = new { flag = flag };
            return Content(res.ToJson());
        }

        /// <summary>
        /// Tạo báo cáo
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult DoGenerate(int year, int month, bool? isReGene)
        {
            _monthProfitShareConfigDmnService.DoGenerateMedicalOrgPS(this.OrganizeId, this.UserIdentity.UserCode, year, month, isReGene);
            return Success();
        }

    }
}