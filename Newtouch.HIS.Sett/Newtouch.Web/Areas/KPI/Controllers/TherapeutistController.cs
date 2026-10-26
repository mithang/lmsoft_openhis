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
    public class TherapeutistController : ControllerBase
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
            var list = _monthProfitShareConfigDmnService.GetTherapeutistPSConfigList(this.OrganizeId, keyword);

            return Content(list.ToJson());
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
            var vo = _monthProfitShareConfigDmnService.GetTherapeutistPSConfig(keyValue);

            return Content(vo.ToJson());
        }

        /// <summary>
        /// 提交治疗师提成配置
        /// </summary>
        /// <param name="entity"></param>
        public ActionResult SubmitTherapeutistPSConfig(TherapeutistMonthProfitShareConfigEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _monthProfitShareConfigDmnService.SubmitTherapeutistPSConfig(entity, keyValue);

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
            var flag = _monthProfitShareConfigDmnService.TherapeutistPSCheckIsGenerated(this.OrganizeId, year, month);
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
            _monthProfitShareConfigDmnService.DoGenerateTherapeutistPS(this.OrganizeId, this.UserIdentity.UserCode, year, month, isReGene);
            return Success();
        }

    }
}