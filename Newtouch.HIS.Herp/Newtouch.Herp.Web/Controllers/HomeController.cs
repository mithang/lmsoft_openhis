using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Web.Models;

namespace Newtouch.Herp.Web.Controllers
{
    /// <summary>
    /// Trang chủ Herp — khung portal shell (SHELL-B, D-SHELL-001/002/003).
    /// Index là khung shell gốc 421 dòng (skin-black, #theme-wrapper, header +
    /// org switcher + nhắc cảnh báo tồn kho/quá hạn, sidebar + tab/iframe, tab
    /// mặc định /Home/Default); dashboard KPI/biểu đồ nằm ở Default.cshtml +
    /// action Default(). Org switcher: triển khai đơn cơ sở như mẫu MRMS
    /// (chỉ 1 org hiện tại, NeedChooseOrg=false).
    /// Kế thừa ControllerBase của Herp (KHÔNG đặt [HandlerLogin] — auth qua
    /// FrameworkAuthorizationFilter toàn cục, D-SHELL-008) vì mọi tác vụ ở đây
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

        // MUST be override: BaseController (tổ tiên của ControllerBase) đã định nghĩa
        // virtual Index() [HttpGet][HandlerAuthorize] { return View(); }. Khai báo
        // không override sẽ bị action discovery chọn method gốc -> ViewBag rỗng
        // (đây là nguyên nhân header OrgName rỗng sau commit 03f64d72).
        public override IActionResult Index()
        {
            // D-SHELL-002: cấp đủ 3 ViewBag org theo mẫu MRMS (1 org duy nhất,
            // tên đọc từ [NewtouchHIS_Base]..Sys_Organize; không bịa org).
            var opr = OperatorProvider.GetCurrent();
            string orgId = opr?.OrganizeId ?? "";
            string orgName = orgId;
            try
            {
                var name = HerpDb.GetBaseConnection().Ado.GetString(
                    "select top 1 Name from [NewtouchHIS_Base].[dbo].[Sys_Organize] with(nolock) where Id = @id",
                    new SqlSugar.SugarParameter("@id", orgId));
                if (!string.IsNullOrWhiteSpace(name)) orgName = name;
            }
            catch { /* fall back to org id */ }
            ViewBag.MediOrgList = new List<HerpOrgItem>
            {
                new HerpOrgItem { Id = orgId, Name = orgName }
            };
            ViewBag.OrgName = orgName;
            ViewBag.NeedChooseOrg = false;
            return View();
        }

        /// <summary>Tab mặc định của khung shell: dashboard KPI/biểu đồ (đã dời từ Index sang đây, D-SHELL-003).</summary>
        public IActionResult Default()
        {
            return View();
        }

        /// <summary>Trang chọn cơ sở y tế (modal đổi org của shell).</summary>
        public IActionResult UserOrgChoose()
        {
            var opr = OperatorProvider.GetCurrent();
            ViewBag.MediOrgList = new List<HerpOrgItem>
            {
                new HerpOrgItem { Id = opr?.OrganizeId ?? "", Name = ViewBag.OrgName as string ?? opr?.OrganizeId ?? "" }
            };
            return View();
        }

        /// <summary>
        /// Đổi cơ sở y tế hiện tại. Triển khai đơn cơ sở (mẫu MRMS): đổi về
        /// chính org hiện tại thì success, org khác thì lỗi rõ ràng thay vì
        /// im lặng (shell chỉ hiện nút đổi khi MediOrgList.Count &gt; 1 nên
        /// nhánh lỗi không tới được với dữ liệu hiện tại).
        /// </summary>
        public IActionResult SwithOrg(string dstnOrgId)
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr != null && string.Equals(dstnOrgId, opr.OrganizeId, StringComparison.OrdinalIgnoreCase))
            {
                return Content(new { state = "success" }.ToJson());
            }
            return Content(new { state = "error", message = "Chuyển đổi cơ sở y tế khác không khả dụng trong triển khai đơn cơ sở này." }.ToJson());
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
            try
            {
                var result = new MonthlySummaryDTO[3];
                result[0] = _homeApp.GetPsiCountVoByKf(_storageManageDmnService.GetCkCountByKf(Constants.CurrentKf.currentKfId, OrganizeId));
                result[1] = _homeApp.GetPsiCountVoByKf(_storageManageDmnService.GetRkCountByKf(Constants.CurrentKf.currentKfId, OrganizeId));
                result[2] = _homeApp.GetPsiCountVoByKf(_storageManageDmnService.GetSyCountByKf(Constants.CurrentKf.currentKfId, OrganizeId));
                return Content(result.ToJson());
            }
            catch
            {
                // FIX-2B (B-HERP-01, chi chuoi hien thi): khi DB loi, exception
                // cua SqlSugar mang tien to tieng Trung "中文提示 :" (format cung
                // trong DLL SqlSugar, khong sua duoc bang chuoi app) va middleware
                // tra nguyen message tho ra JSON. Chan o seam action nay va tra
                // thong bao tieng Viet chung; khong dich framework, khong doi
                // duong thanh cong.
                return Error("Không thể lấy dữ liệu từ cơ sở dữ liệu. Vui lòng thử lại sau.");
            }
        }

        /// <summary>Nhập kho trong năm theo loại phiếu (dữ liệu biểu đồ tròn)</summary>
        public IActionResult GetRkCountByLx()
        {
            try
            {
                var result = _storageManageDmnService.GetRkCountByLx(Constants.CurrentKf.currentKfId, OrganizeId) ?? new List<ClassificationStatisticsEntity>();
                _homeApp.TransformRkCount(result);
                return Content(result.ToJson());
            }
            catch
            {
                // FIX-2B (B-HERP-01): nhu GetPsiByKf — chan message tho cua
                // SqlSugar ("中文提示 :") o seam action, tra thong bao tieng Viet.
                return Error("Không thể lấy dữ liệu từ cơ sở dữ liệu. Vui lòng thử lại sau.");
            }
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
            try
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
            catch
            {
                // FIX-2B (B-HERP-01): nhu GetPsiByKf — chan message tho cua
                // SqlSugar ("中文提示 :") o seam action, tra thong bao tieng Viet.
                return Error("Không thể lấy dữ liệu từ cơ sở dữ liệu. Vui lòng thử lại sau.");
            }
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
