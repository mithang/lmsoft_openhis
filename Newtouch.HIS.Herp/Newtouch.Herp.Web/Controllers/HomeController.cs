using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Controllers
{
    /// <summary>
    /// Trang chủ
    /// </summary>
    public class HomeController : FrameworkBase.MultiOrg.Web.Controllers.HomeController
    {
        private readonly IHomeApp _homeApp;
        private readonly IStorageManageDmnService _storageManageDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;

        /// <summary>
        /// about
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Chuyển kho
        /// </summary>
        /// <returns></returns>
        public ActionResult KfChange()
        {
            ViewBag.KfList = Constants.CurrentKf.kfList;
            return View();
        }

        /// <summary>
        /// Chuyển kho
        /// </summary>
        /// <param name="kfId">Kho ID</param>
        /// <param name="kfName">Tên kho</param>
        /// <param name="kfLeve">Cấp kho</param>
        /// <returns></returns>
        public ActionResult SwithKf(string kfId, string kfName, int kfLeve)
        {
            var opr = Constants.CurrentKf;
            opr.currentKfId = kfId;
            opr.currentKfName = kfName;
            opr.currentKfLevel = kfLeve;
            Constants.SetCurrentKf(opr.userId, opr);
            return Success("切换成功");
        }

        #region Việc cần làm trang chủ

        /// <summary>
        /// Lấy chờ xử lý html
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNeedDealDiv()
        {
            var result = _homeApp.AssembleNeedDealHtml();
            return Success("", result);
        }

        /// <summary>
        /// Lấy tổng Số cần xử lý
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPendingCount()
        {
            return Content(_homeApp.GetPendingCount().ToJson());
        }

        #endregion

        #region Thống kê

        /// <summary>
        /// Thống kê nhập-xuất-tồn
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPsiByKf()
        {
            var result = new MonthlySummaryDTO[3];
            result[0] = _homeApp.GetPsiCountVoByKf(_storageManageDmnService.GetCkCountByKf(Constants.CurrentKf.currentKfId, OrganizeId));
            result[1] = _homeApp.GetPsiCountVoByKf(_storageManageDmnService.GetRkCountByKf(Constants.CurrentKf.currentKfId, OrganizeId));
            result[2] = _homeApp.GetPsiCountVoByKf(_storageManageDmnService.GetSyCountByKf(Constants.CurrentKf.currentKfId, OrganizeId));
            return Content(result.ToJson());
        }

        /// <summary>
        /// Lấy tổng số nhập kho theo loại chứng từ
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRkCountByLx()
        {
            var result = _storageManageDmnService.GetRkCountByLx(Constants.CurrentKf.currentKfId, OrganizeId) ?? new List<ClassificationStatisticsEntity>();
            _homeApp.TransformRkCount(result);
            return Content(result.ToJson());
        }
        #endregion

        #region Nhắc nhở tin nhắn
        public ActionResult getmesquery()
        {
            string yfbm = Constants.CurrentKf.currentKfId;
            int gqyjz = -SysConfigReader.Int("GET_YFGQYG");//Giá trị cảnh báo hết hạn
            var retdata = _storageManageDmnService.MSGQuery(yfbm, this.OrganizeId, gqyjz);
            int gqyjcount = 0, kcyjcount = 0;
            if (retdata != null)
            {
                kcyjcount = retdata.Where(p => p.typeas == "1").Count();
                gqyjcount = retdata.Where(p => p.typeas == "2").Count();
            }
            var data = new
            {
                rows = retdata,
                kcyj = kcyjcount,
                gqyj = gqyjcount
            };
            return Content(data.ToJson());
        }
        #endregion

        #region Đồng bộ cấu hình tham số hệ thống
        public ActionResult SyncSysConfigParams(string orgId)
        {
            //Dữ liệu cơ sở
            var sysConfigBaseEntities = _sysConfigRepo.GetList("", "*").ToList();
            //Dữ liệu tự mang theo của tổ chức
            var sysConfigEntities = _sysConfigRepo.GetList("", orgId).ToList();
            //Theo code Khử trùng lặp
            var sysConfigCodes = new HashSet<string>(sysConfigEntities.Select(entity => entity.Code));
            var filteredEntities = sysConfigBaseEntities
                .Where(baseEntity => !sysConfigCodes.Contains(baseEntity.Code))
                .ToList();
            foreach (var item in filteredEntities)
            {
                item.Id = Guid.NewGuid().ToString();
                item.OrganizeId = orgId;
            }
            var insert = _sysConfigRepo.Insert(filteredEntities);
            return Success("",insert);
        }
        #endregion
    }
}