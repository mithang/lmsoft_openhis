using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.DataBaseSvr;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.DomainServices.OutPatientPharmacy;
using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;

using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure.Model;
using Newtouch.HIS.Domain.ValueObjects;
using SqlSugar;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// Trang chủ
    /// </summary>
    // NX16-001: bo [HandlerLogin] (mau HOSP NX2-002) de duong adopt ?appId= chay duoc:
    // FrameworkAuthorizationFilter se chan request an danh truoc khi vao action.
    // Cac guard null-opr trong action duoc giu lai lam phong thu sau filter.
    public class HomeController : NewtouchHIS.Framework.Web.Controllers.HomeController
    {
        private readonly ISysUserExDmnService _sysUserDmnService;
        private readonly ISysPharmacyDepartmentRepo _sysPharmacyDepartmentRepo;
        private readonly IPyDmnService _pyDmnService;
        private readonly IMedicineApp _medicineApp;
        private readonly IHomePageStatisticsApp _homePageStatisticsApp;
        private readonly ISysConfigRepo _sysConfigRepo;

        public HomeController(IOrganizeAppService organizeApp,
            ISysUserExDmnService sysUserDmnService,
            ISysPharmacyDepartmentRepo sysPharmacyDepartmentRepo,
            IPyDmnService pyDmnService,
            IMedicineApp medicineApp,
            IHomePageStatisticsApp homePageStatisticsApp,
            ISysConfigRepo sysConfigRepo) : base(organizeApp)
        {
            _sysUserDmnService = sysUserDmnService;
            _sysPharmacyDepartmentRepo = sysPharmacyDepartmentRepo;
            _pyDmnService = pyDmnService;
            _medicineApp = medicineApp;
            _homePageStatisticsApp = homePageStatisticsApp;
            _sysConfigRepo = sysConfigRepo;
        }
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override IActionResult Index()
        {
            // NX16-001: [HandlerLogin] da bo — request an danh bi filter chan truoc khi vao day;
            // không có session thì về Login thay vì NRE ở GetCurrent().UserCode.
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return Redirect("/Login/Index");
            }
            var cookieLoginFlag = Request.Cookies[Constants.AppId + "_" + opr.UserCode + "_" + "LoginFlag"];
            ViewBag.cookieLoginFlag = cookieLoginFlag;
            // SHELL-C (S5-15, D-SHELL-002): Index doc 3 ViewBag org (truoc day
            // base.Index() bare tra null) — mau don-org MRQC: ten org doc truc
            // tiep tu BaseDb (PDS ISysOrganizeDmnService khong co
            // GetNameByOrgId/GetMedicalOrganizeListByUserId).
            string orgId = opr.OrganizeId ?? "";
            string orgName = orgId;
            try
            {
                var name = SqlSugarDbContext.Db.GetConnection("BaseDb").Ado.GetString(
                    "select top 1 Name from [NewtouchHIS_Base].[dbo].[Sys_Organize] with(nolock) where Id = @id",
                    new SugarParameter("@id", orgId));
                if (!string.IsNullOrWhiteSpace(name)) orgName = name;
            }
            catch { /* fall back to org id */ }
            ViewBag.MediOrgList = new List<SysOrganizeVEntity>
            {
                new SysOrganizeVEntity { Id = orgId, Name = orgName }
            };
            ViewBag.OrgName = orgName;
            ViewBag.NeedChooseOrg = false;
            return base.Index();
        }

        /// <summary>
        /// A6-NX31-001 (QUYẾT ĐỊNH BA/PM): PDS là portal duy nhất thiếu UserOrgChoose/
        /// SwithOrg — base Framework (NewtouchHIS.Framework.Web) không có 2 action này,
        /// Views/Home cũng không có UserOrgChoose.cshtml, Index.cshtml gọi
        /// showSwitchOrgPrompt() nhưng không định nghĩa hàm đó (7 portal còn lại đều có
        /// đủ 3 thứ). KHÔNG dùng chung view CIS/Herp: Razor resolve view theo portal,
        /// mỗi portal có AppId/namespace riêng (CIS dùng Newtouch.Domain.Entity.
        /// SysOrganizeVEntity + CIS.Compat AppId; Herp dùng HerpOrgItem riêng).
        /// Tạo mới cho PDS theo đúng mẫu CIS đơn-org (NX30-CSRF-001): list 1 org,
        /// tên đọc trực tiếp từ BaseDb (PDS không có ISysOrganizeDmnService.
        /// GetMedicalOrganizeListByUserId — xem ghi chú SHELL-C trong Index()).
        /// SwithOrgSuccessOprBeforeSave KHÔNG gọi ở đây: nó là hook của base nhị phân
        /// FrameworkBase.MultiOrg đã mất (không action nào gọi nó nữa); mẫu đơn-org
        /// chỉ success khi đổi về đúng org hiện tại nên không đổi yfbm. Khi nào làm
        /// đa-org thật thì SwithOrg phải gọi lại hook này trước khi trả success.
        /// </summary>
        public IActionResult UserOrgChoose()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return Redirect("/Login/Index");
            }
            string orgId = opr.OrganizeId ?? "";
            ViewBag.MediOrgList = new List<SysOrganizeVEntity>
            {
                new SysOrganizeVEntity { Id = orgId, Name = GetOrgName(orgId) }
            };
            return View();
        }

        /// <summary>
        /// Đổi cơ sở y tế hiện tại. Triển khai đơn cơ sở (mẫu CIS NX30-CSRF-001):
        /// chỉ chấp nhận đổi về đúng org hiện tại; org khác trả lỗi rõ ràng.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SwithOrg(string dstnOrgId)
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr != null && string.Equals(dstnOrgId, opr.OrganizeId, StringComparison.OrdinalIgnoreCase))
            {
                return Content(new { state = "success" }.ToJson());
            }
            return Content(new { state = "error", message = "Chuyển đổi cơ sở y tế khác không khả dụng trong triển khai đơn cơ sở này." }.ToJson());
        }

        private static string GetOrgName(string orgId)
        {
            string orgName = orgId;
            try
            {
                var name = SqlSugarDbContext.Db.GetConnection("BaseDb").Ado.GetString(
                    "select top 1 Name from [NewtouchHIS_Base].[dbo].[Sys_Organize] with(nolock) where Id = @id",
                    new SugarParameter("@id", orgId));
                if (!string.IsNullOrWhiteSpace(name)) orgName = name;
            }
            catch { /* fall back to org id */ }
            return orgName;
        }

        /// <summary>
        /// Người dùng chuyển nhà thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult SetUserPharmacyForm()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return Redirect("/Login/Index");
            }
            ViewBag.yfbmList = _sysUserDmnService.GetYfbmListByUserId(opr.UserId, opr.OrganizeId);
            return View();
        }

        /// <summary>
        /// Hook doi org (FrameworkBase.MultiOrg da mat) — khong action nao goi nua.
        /// G34-F3: [NonAction] — chan routing GET ghi Redis + cookie + param trust (UserId/OrganizeId
        /// tu request); chi chan routing, goi truc tiep tu C# van chay.
        /// </summary>
        /// <param name="opr"></param>
        [NonAction]
        public void SwithOrgSuccessOprBeforeSave(OperatorModel opr)
        {
            //Cập nhật phần nhà thuốc người dùng
            if (ConfigurationHelper.GetAppConfigBoolValue("Is_UserYfbmRelated") == true && opr.OrganizeId != null)
            {
                //Bộ phận nhà thuốc hiện cần thao tác
                if (!string.IsNullOrWhiteSpace(opr.UserId) && !string.IsNullOrWhiteSpace(opr.OrganizeId))
                {
                    opr.yfbmCodeList = _sysUserDmnService.GetYfbmCodeListByUserId(opr.UserId, opr.OrganizeId)?.ToList();
                }
                if (opr.yfbmCodeList != null && opr.yfbmCodeList.Count > 0)
                {
                    //Bộ phận nhà thuốc hiện cần thao tác    //tại OrgId Đã liên kết nhiều yfbm Làm thế nào
                    var curYfbmCode = opr.yfbmCodeList.FirstOrDefault();
                    var curYfbmObj = _sysPharmacyDepartmentRepo.GetUserYfbmByCode(curYfbmCode, opr.OrganizeId);
                    if (curYfbmObj != null)
                    {
                        Constants.SetCurrentYfbm(opr.UserId, new LoginUserCurrentYfbmModel()
                        {
                            yfbmCode = curYfbmCode,
                            yfbmjb = curYfbmObj.yfbmjb,
                            mzzybz = curYfbmObj.mzzybz,
                            yfbmmc = curYfbmObj.yfbmmc
                        });
                    }                    
                }
                else
                {
                    Constants.SetCurrentYfbm(opr.UserId, null);  //Loại bỏ
                }
            }

            //Chuyển bộ phận nhà thuốc
            Response.Cookies.Append(Constants.AppId + "_" + "CookieKey_IsHospAdministrator", opr.IsHospAdministrator.ToString().ToLower());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Default()
        {
            return View();
        }

        /// <summary>
        /// Lấy chờ xử lý html
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetNeedDealDiv()
        {
            var result = _homePageStatisticsApp.AssembleNeedDealHtml();
            return Success("", result);
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Lấy tổng số chờ xử lý
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPendingCount()
        {
            return Content(_homePageStatisticsApp.GetPendingCount().ToJson());
        }

        /// <summary>
        /// Lấy lần phát thuốc đơn tháng ngoại trú
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFyCountVoByYfbm()
        {
            var result = new MonthlySummaryDO[2];
            result[0] = _medicineApp.GetFyCountVoByYfbm(_pyDmnService.GetMzFyCountVoByYfbm());
            result[1] = _medicineApp.GetFyCountVoByYfbm(_pyDmnService.GetZyFyCountVoByYfbm());
            return Content(result.ToJson());
        }

        /// <summary>
        /// Lấy lần phát thuốc đơn tháng ngoại trú
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFyCountBydl()
        {
            var result = _medicineApp.GetFyCountBydl() ?? new List<FyClassificationStatistics>();
            return Content(result.ToJson());
        }

        /// <summary>
        /// Chuyển nhà thuốc người dùng đăng nhập
        /// </summary>
        /// <param name="kfId">KhoID</param>
        /// <param name="kfName">Tên kho</param>
        /// <param name="kfLeve">Cấp kho</param>
        /// <returns></returns>
        // G34-F1: GET-write -> chi POST + antiforgery (mau SwithOrg cung controller).
        // G34-F2: validate yfbmCode + lay gia tri hien thi tu DB, khong tin client.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetUserPharmacy(string yfbmCode, string mzzybz, string yfbmjb, string yfbmmc)
        {
            // NX16-001: [HandlerLogin] da bo — request an danh bi filter chan; giu guard phong thu:
            // chạm Constants.CurrentYfbm (bản thân getter cũng deref GetCurrent()).
            var curOpr = OperatorProvider.GetCurrent();
            if (curOpr == null)
            {
                return Error("Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại.");
            }
            if (string.IsNullOrWhiteSpace(yfbmCode))
            {
                return Error("Vui lòng chọn nhà thuốc");
            }
            // G34-F2: code phai ton tai o org hien tai (zt=1) — GetUserYfbmByCode tra null
            // neu code bogus / khac org / zt!=1; va phai nam trong danh sach kho cua user.
            var yfbm = _sysPharmacyDepartmentRepo.GetUserYfbmByCode(yfbmCode, curOpr.OrganizeId);
            if (yfbm == null)
            {
                return Error("Nhà thuốc không hợp lệ");
            }
            var userCodes = _sysUserDmnService.GetYfbmCodeListByUserId(curOpr.UserId, curOpr.OrganizeId);
            if (userCodes == null || !userCodes.Contains(yfbmCode))
            {
                return Error("Nhà thuốc không hợp lệ");
            }
            var opr = Constants.CurrentYfbm;
            // G34-F2: lay mzzybz/yfbmjb/yfbmmc tu DB (yfbm), bo 3 param client
            // (giu signature de view khong doi).
            opr.yfbmCode = yfbm.yfbmCode;
            opr.yfbmjb = yfbm.yfbmjb;
            opr.mzzybz = yfbm.mzzybz;
            opr.yfbmmc = yfbm.yfbmmc;
            Constants.SetCurrentYfbm(curOpr.UserId, opr);
            return Success("Chuyển thành công");
        }

		#region Nhắc tin nhắn
        public IActionResult getmesquery()
		{
            string yfbm= Constants.CurrentYfbm.yfbmCode;
            string kcyjz = SysConfigReader.String("GET_YFKCYG");//Giá trị cảnh báo tồn kho
            int gqyjz = -SysConfigReader.Int("GET_YFGQYG");//Giá trị cảnh báo hết hạn
            var retdata = _sysUserDmnService.MSGQuery(yfbm,this.OrganizeId, gqyjz, kcyjz);
            int gqyjcount=0,kcyjcount=0,kcdryjcount=0;
			if (retdata!=null)
			{
                kcyjcount=retdata.Where(p => p.typeas == "1").Count();
                gqyjcount = retdata.Where(p => p.typeas == "2").Count();
                kcdryjcount = retdata.Where(p => p.typeas == "3").Count();
            }
            var data =new {
                rows = retdata,
                kcyj = kcyjcount,
                gqyj= gqyjcount,
                drgqyj= kcdryjcount
            };
            return Content(data.ToJson());
		}
        #endregion
        
        /**
         * Đồng bộ tham Số cấu hình hệ thống
         */
        // G34-F4: GET-write -> chi POST + antiforgery (mau SwithOrg cung controller).
        // G34-F5: orgId rong -> loi TV (sua NRE); org khong ton tai -> loi TV.
        // G34-F6: skip Insert khi 0 dong (mau Herp).
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SyncSysConfigParams(string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                return Error("Vui lòng chọn tổ chức");
            }
            // G34-F5: org phai ton tai trong Sys_Organize (tra cuu BaseDb, mau SHELL-C GetOrgName
            // cung controller; read-only, khong ghi demo) — neu khong, dong bo se tao dong rac OrganizeId bogus.
            string orgName = null;
            try
            {
                orgName = SqlSugarDbContext.Db.GetConnection("BaseDb").Ado.GetString(
                    "select top 1 Name from [NewtouchHIS_Base].[dbo].[Sys_Organize] with(nolock) where Id = @id",
                    new SugarParameter("@id", orgId));
            }
            catch { /* org khong ton tai hoac loi tra cuu -> loi ben duoi */ }
            if (string.IsNullOrWhiteSpace(orgName))
            {
                return Error("Tổ chức không tồn tại");
            }
            //Dữ liệu cơ bản
            var sysConfigBaseEntities = _sysConfigRepo.GetList("", "*").ToList();
            //Dữ liệu tự mang của tổ chức
            var sysConfigEntities = _sysConfigRepo.GetList("", orgId).ToList();
            //Theocode Khử trùng lặp
            var sysConfigCodes = new HashSet<string>(sysConfigEntities.Select(entity => entity.Code));
            var filteredEntities = sysConfigBaseEntities
                .Where(baseEntity => !sysConfigCodes.Contains(baseEntity.Code))
                .ToList();
            foreach (var item in filteredEntities)
            {
                item.Id = Guid.NewGuid().ToString();
                item.OrganizeId = orgId;
            }
            // G34-F6: khong co dong moi thi khong goi Insert (tranh lenh insert rong).
            var insert = filteredEntities.Count > 0 ? _sysConfigRepo.Insert(filteredEntities) : 0;
            return Success("",insert);
        }
    }
}