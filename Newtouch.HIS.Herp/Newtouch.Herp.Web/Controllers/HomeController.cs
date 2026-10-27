using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;

namespace Newtouch.Herp.Web.Controllers
{
    /// <summary>
    /// Trang chủ Herp (bản Core). Index là dashboard (nội dung Default.cshtml
    /// gốc, đã dịch): khối "cần xử lý" + biểu đồ thống kê nhập–xuất–tồn +
    /// biểu đồ tròn nhập kho theo loại phiếu + dòng cảnh báo tồn kho/quá hạn.
    /// Khung portal (nav/header/tab) của Index gốc KHÔNG port vì cổng SSO đã
    /// đảm nhiệm portal; các liên kết khu vực của trang đích tối giản cũ được
    /// giữ lại thành hàng điều hướng phía trên dashboard.
    /// Kế thừa ControllerBase của Herp (có [HandlerLogin]) vì mọi tác vụ ở đây
    /// đều cần session (kho hiện tại, tổ chức): bản gốc cũng chạy sau đăng nhập
    /// trong portal.
    /// </summary>
    public class HomeController : Newtouch.Herp.Web.ControllerBase
    {
        private readonly IHomeApp _homeApp;
        private readonly IStorageManageDmnService _storageManageDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;

        public HomeController(IHomeApp homeApp, IStorageManageDmnService storageManageDmnService,
            ISysConfigRepo sysConfigRepo)
        {
            _homeApp = homeApp;
            _storageManageDmnService = storageManageDmnService;
            _sysConfigRepo = sysConfigRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        /// <summary>Chuyển kho (trang chọn kho)</summary>
        public IActionResult KfChange()
        {
            ViewBag.KfList = Constants.CurrentKf.kfList;
            return View();
        }

        /// <summary>Chuyển kho hiện tại</summary>
        public IActionResult SwithKf(string kfId, string kfName, int kfLeve)
        {
            var opr = Constants.CurrentKf;
            opr.currentKfId = kfId;
            opr.currentKfName = kfName;
            opr.currentKfLevel = kfLeve;
            Constants.SetCurrentKf(opr.userId, opr);
            return Success("Chuyển kho thành công");
        }

        #region Trang chủ: khối cần xử lý

        /// <summary>Lấy HTML khối "cần xử lý" của dashboard</summary>
        [HttpGet]
        public IActionResult GetNeedDealDiv()
        {
            var result = _homeApp.AssembleNeedDealHtml();
            return Success("", result);
        }

        /// <summary>Lấy tổng số cần xử lý (đổ vào các thẻ dashboard)</summary>
        [HttpGet]
        public IActionResult GetPendingCount()
        {
            return Content(_homeApp.GetPendingCount().ToJson());
        }

        #endregion

        #region Trang chủ: thống kê

        /// <summary>Thống kê nhập–xuất–tồn theo tháng của kho hiện tại</summary>
        public IActionResult GetPsiByKf()
        {
            var result = new MonthlySummaryDTO[3];
            result[0] = _homeApp.GetPsiCountVoByKf(_storageManageDmnService.GetCkCountByKf(Constants.CurrentKf.currentKfId, OrganizeId));
            result[1] = _homeApp.GetPsiCountVoByKf(_storageManageDmnService.GetRkCountByKf(Constants.CurrentKf.currentKfId, OrganizeId));
            result[2] = _homeApp.GetPsiCountVoByKf(_storageManageDmnService.GetSyCountByKf(Constants.CurrentKf.currentKfId, OrganizeId));
            return Content(result.ToJson());
        }

        /// <summary>Nhập kho trong năm theo loại phiếu (dữ liệu biểu đồ tròn)</summary>
        public IActionResult GetRkCountByLx()
        {
            var result = _storageManageDmnService.GetRkCountByLx(Constants.CurrentKf.currentKfId, OrganizeId) ?? new List<ClassificationStatisticsEntity>();
            _homeApp.TransformRkCount(result);
            return Content(result.ToJson());
        }
        #endregion

        #region Trang chủ: nhắc cảnh báo
        /// <summary>
        /// Nhắc cảnh báo tồn kho/quá hạn của kho hiện tại. Ngưỡng quá hạn
        /// (ngày) đọc từ cấu hình GET_YFGQYG trong Sys_Config của CSDL Herp
        /// (không có dòng này trong dữ liệu demo → mặc định 0, đúng hành vi
        /// SysConfigReader.Int gốc); giá trị truyền vào truy vấn là số âm
        /// của ngưỡng, như bản gốc.
        /// typeas: "1" = cảnh báo tồn kho, "2" = cảnh báo quá hạn.
        /// </summary>
        public IActionResult getmesquery()
        {
            string yfbm = Constants.CurrentKf.currentKfId;
            int gqyjz = -_sysConfigRepo.GetIntValue("GET_YFGQYG", OrganizeId);
            var retdata = _storageManageDmnService.MSGQuery(yfbm, OrganizeId, gqyjz);
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

        #region Đồng bộ tham số cấu hình hệ thống
        /// <summary>
        /// Đồng bộ cấu hình gốc ("*") chưa có ở tổ chức orgId sang tổ chức đó
        /// (mỗi mã chỉ lấy một lần, dòng mới nhận Id GUID mới). Đây là tác vụ
        /// GHI dữ liệu nên KHÔNG được kiểm thử trực tiếp trong đợt port này
        /// (chính sách không ghi DB); code port trung thực từ bản gốc, chỉ
        /// kiểm chứng ở mức biên dịch và khởi động app.
        /// </summary>
        public IActionResult SyncSysConfigParams(string orgId)
        {
            // Dữ liệu gốc
            var sysConfigBaseEntities = _sysConfigRepo.GetList("", "*").ToList();
            // Dữ liệu riêng của tổ chức
            var sysConfigEntities = _sysConfigRepo.GetList("", orgId).ToList();
            // Theo code, loại trùng
            var sysConfigCodes = new HashSet<string>(sysConfigEntities.Select(entity => entity.Code));
            var filteredEntities = sysConfigBaseEntities
                .Where(baseEntity => !sysConfigCodes.Contains(baseEntity.Code))
                .ToList();
            foreach (var item in filteredEntities)
            {
                item.Id = Guid.NewGuid().ToString();
                item.OrganizeId = orgId;
            }
            // Không có dòng mới thì không gọi Insert (tránh lệnh insert rỗng).
            var insert = filteredEntities.Count > 0 ? _sysConfigRepo.Insert(filteredEntities) : 0;
            return Success("", insert);
        }
        #endregion
    }
}
