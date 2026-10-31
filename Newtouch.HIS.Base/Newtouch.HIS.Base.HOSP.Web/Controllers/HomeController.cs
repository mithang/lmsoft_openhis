using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    [HandlerLogin]
    public class HomeController : Controller
    {
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ICommonDmnService _commonDmnService;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysDepartmentRepository _sysDepartmentRepo;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;

        public HomeController(ICommonDmnService commonDmnService, ISysUserDmnService sysUserDmnService
            , ISysOrganizeDmnService sysOrganizeDmnService
            , ISysDepartmentRepository sysDepartmentRepo
            , IUserRoleAuthDmnService userRoleAuthDmnService)
        {
            this._commonDmnService = commonDmnService;
            this._sysUserDmnService = sysUserDmnService;
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysDepartmentRepo = sysDepartmentRepo;
            this._userRoleAuthDmnService = userRoleAuthDmnService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Home mang [HandlerLogin] nên filter không chặn request ẩn danh:
            // không có session thì về Login (mẫu Organize) thay vì NRE kèm
            // stack trace ở GetCurrent().UserCode như trước.
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return Redirect("/Login/Index");
            }
            var cookieLoginFlag = HttpWebHelper.GetCookie(Constants.AppId + "_" + opr.UserCode + "_" + "LoginFlag");
            ViewBag.cookieLoginFlag = cookieLoginFlag;

            var nLanguageTypeKey = "Newtouch_language_type";
            if (!string.IsNullOrWhiteSpace(Constants.AppId))
            {
                nLanguageTypeKey = Constants.AppId + "_" + nLanguageTypeKey;
            }
            var language_type = HttpWebHelper.GetCookie(nLanguageTypeKey);
            if (string.IsNullOrWhiteSpace(language_type))
            {
                language_type = _sysUserDmnService.GetLanguageTypeByUserId(opr.UserId);   //Thuộc tính người dùng
                // Port fix: Lib HttpWebHelper.WriteCookie throws on a null value
                // (Uri.EscapeDataString); the demo user has no language type set, so
                // only write the cookie when a value actually came back.
                if (!string.IsNullOrWhiteSpace(language_type))
                {
                    HttpWebHelper.WriteCookie(nLanguageTypeKey, language_type);
                }
            }
            if (!string.IsNullOrWhiteSpace(opr.OrganizeId))
            {
                //Hiện tạiOrgTên(Liên kếtStaff)
                ViewBag.OrgName = _sysOrganizeDmnService.GetNameByOrgId(opr.OrganizeId);
            }
            else
            {

                //Hiển thị tên tổ chức cấp cao nhất
                ViewBag.OrgName = _sysOrganizeDmnService.GetNameByOrgId(opr.TopOrganizeId);
            }

            if (!opr.IsRoot && !opr.IsAdministrator)    //Luôntrue
            {
                //Hiện tạiUserId tất cả tương ứng Cơ sở y tế List
                var mediOrgList = _sysOrganizeDmnService.GetMedicalOrganizeListByUserId(opr.UserId);

                ViewBag.MediOrgList = mediOrgList;

                if (mediOrgList != null && mediOrgList.Count > 1)
                {
                    //Có phải khi tải trang là bắt buộc phải chọn khôngOrg
                    var needChooseOrg = HttpWebHelper.GetCookie(Constants.AppId + "_" + "CookieKey_ChooseOrg");
                    ViewBag.NeedChooseOrg = !string.IsNullOrWhiteSpace(needChooseOrg);
                }
            }
            return View();
        }

        /// <summary>
        /// Chuyển cơ sở y tế Khung nhìn
        /// </summary>
        /// <returns></returns>
        public IActionResult UserOrgChoose()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return Redirect("/Login/Index");
            }

            var mediOrgList = _sysOrganizeDmnService.GetMedicalOrganizeListByUserId(opr.UserId);

            ViewBag.MediOrgList = mediOrgList;

            return View();
        }

        /// <summary>
        /// Chuyển cơ sở y tế
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SwithOrg(string dstnOrgId)
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại." }.ToJson());
            }

            var staffList = _sysUserDmnService.GetStaffListByUserId(opr.UserId);

            var orgStaff = staffList.Where(p => p.OrganizeId == dstnOrgId).FirstOrDefault();

            if (orgStaff != null)
            {
                opr.UserName = orgStaff.Name;
                opr.DepartmentCode = orgStaff.DepartmentCode;
                opr.OrganizeId = orgStaff.OrganizeId;
                opr.StaffId = orgStaff.Id;
                opr.rygh = orgStaff.gh;
                opr.DepartmentName = _sysDepartmentRepo.GetNameByCode(orgStaff.DepartmentCode, orgStaff.OrganizeId);

                var roleList = _userRoleAuthDmnService.GetUserRoleList(opr.UserId, orgStaff.OrganizeId);
                //170904 AdministratorKhông thể liên kết nữaStaff,Chỉadmin
                opr.RoleIdList = roleList.Select(p => p.Id).ToList();
                opr.IsHospAdministrator = roleList.Any(p => p.Code == "HospAdministrator");

                OperatorProvider.AddCurrent(opr);

                HttpWebHelper.WriteCookie(Constants.AppId + "_" + "CookieKey_OrganizeId", orgStaff.OrganizeId);

                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "Chuyển đổi thành công" }.ToJson());
            }
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Chuyển đổi thất bại" }.ToJson());
        }

        [HttpGet]
        public IActionResult Default()
        {
            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Notice()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return Redirect("/Login/Index");
            }
            ViewBag.UserCode = opr.UserCode;
            ViewBag.OrganizeId = opr.OrganizeId;
            return View();
        }

        [HttpGet]
        /// <summary>
        /// Lấy số lượng người khám(Ghi nợ ngoại trú,Ghi nợ nội trú)
        /// </summary>
        public IActionResult GetVisitNum()
        {
            VisitNumBO BO = null;
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại." }.ToJson());
            }
            var userCode = opr.UserCode;
            if (userCode == "root")
            {
                BO = null;
            }
            else if (userCode == "admin")
            {
                var topOrgId = Constants.TopOrganizeId;
                if (string.IsNullOrEmpty(topOrgId))
                {
                    throw new FailedCodeException("SYS_GET_TOPORGANIZATIONAL_FAILURE");

                }
                BO = _commonDmnService.GetVisitNum(true, null, topOrgId);
            }
            else
            {
                var orgId = opr.OrganizeId;
                if (string.IsNullOrEmpty(orgId))
                {
                    throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
                }
                BO = _commonDmnService.GetVisitNum(false, orgId);
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = BO }.ToJson());
        }

        /// <summary>
        /// Lấy giá trị sinh ngẫu nhiên của trường nghiệp vụ(Tự tăng+Format)
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orgIdIsStar"></param>
        /// <param name="topOrgIdIsStar"></param>
        /// <param name="initFormat"></param>
        /// <param name="initFieldLength"></param>
        /// <returns></returns>
        public IActionResult GetNewFieldUniqueValue(string fieldName, bool? orgIdIsStar = null, bool? topOrgIdIsStar = null, string orgId = null, string topOrgId = null, string initFormat = "", int initFieldLength = 0)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                if (orgIdIsStar.HasValue)
                {
                    // Ẩn danh (GetCurrent null) thì coi như không có org:
                    // nhánh dưới trả value null như trường hợp thiếu org.
                    var opr = OperatorProvider.GetCurrent();
                    orgId = orgIdIsStar.Value ? "*" : opr?.OrganizeId;
                }
            }
            if (string.IsNullOrWhiteSpace(topOrgId))
            {
                if (topOrgIdIsStar.HasValue)
                {
                    topOrgId = topOrgIdIsStar.Value ? "*" : Constants.TopOrganizeId;
                }
            }
            if (string.IsNullOrWhiteSpace(initFormat) && initFieldLength > 0)
            {
                initFormat = "{0:D" + initFieldLength + "}";
            }
            string value = null;
            if (string.IsNullOrWhiteSpace(orgId) || string.IsNullOrWhiteSpace(topOrgId) || initFormat == null)
            {
                value = null;
            }
            //else if(orgId == topOrgId)
            //{
            //    value = null;   //???????????????Như vậy có hợp lý không
            //}
            else
            {
                value = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue(fieldName, orgId, topOrgId, initFormat);
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = value }.ToJson());
        }

    }
}