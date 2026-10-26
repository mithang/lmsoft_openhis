using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Controllers
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
        public ActionResult Index()
        {
            var cookieLoginFlag = WebHelper.GetCookie(Constants.AppId + "_" + OperatorProvider.GetCurrent().UserCode + "_" + "LoginFlag");
            ViewBag.cookieLoginFlag = cookieLoginFlag;

            var opr = OperatorProvider.GetCurrent();
            var nLanguageTypeKey = "Newtouch_language_type";
            if (!string.IsNullOrWhiteSpace(Constants.AppId))
            {
                nLanguageTypeKey = Constants.AppId + "_" + nLanguageTypeKey;
            }
            var language_type = WebHelper.GetCookie(nLanguageTypeKey);
            if (string.IsNullOrWhiteSpace(language_type))
            {
                language_type = _sysUserDmnService.GetLanguageTypeByUserId(OperatorProvider.GetCurrent().UserId);   //Thuộc tính người dùng
                WebHelper.WriteCookie(nLanguageTypeKey, language_type);
            }
            if (!string.IsNullOrWhiteSpace(opr.OrganizeId))
            {
                //Hiện tạiOrgTên（Liên kếtStaff）
                ViewBag.OrgName = _sysOrganizeDmnService.GetNameByOrgId(opr.OrganizeId);
            }
            else
            {

                //Hiển thị tên tổ chức cấp cao nhất
                ViewBag.OrgName = _sysOrganizeDmnService.GetNameByOrgId(opr.TopOrganizeId);
            }

            if (!opr.IsRoot && !opr.IsAdministrator)    //Luôntrue
            {
                //Hiện tạiUserId Tất cả tương ứng Cơ sở y tế List
                var mediOrgList = _sysOrganizeDmnService.GetMedicalOrganizeListByUserId(opr.UserId);

                ViewBag.MediOrgList = mediOrgList;

                if (mediOrgList != null && mediOrgList.Count > 1)
                {
                    //Khi tải trang có bắt buộc phải chọn khôngOrg
                    var needChooseOrg = WebHelper.GetCookie(Constants.AppId + "_" + "CookieKey_ChooseOrg");
                    ViewBag.NeedChooseOrg = !string.IsNullOrWhiteSpace(needChooseOrg);
                }
            }
            return View();
        }

        /// <summary>
        /// Chuyển cơ sở y tế Chế độ xem
        /// </summary>
        /// <returns></returns>
        public ActionResult UserOrgChoose()
        {
            var opr = OperatorProvider.GetCurrent();

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
        public ActionResult SwithOrg(string dstnOrgId)
        {
            var opr = OperatorProvider.GetCurrent();

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
                //170904 AdministratorKhông còn có thể liên kếtStaff，Chỉadmin
                opr.RoleIdList = roleList.Select(p => p.Id).ToList();
                opr.IsHospAdministrator = roleList.Any(p => p.Code == "HospAdministrator");

                OperatorProvider.AddCurrent(opr);

                WebHelper.WriteCookie(Constants.AppId + "_" + "CookieKey_OrganizeId", orgStaff.OrganizeId);

                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "切换成功" }.ToJson());
            }
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = "切换失败" }.ToJson());
        }

        [HttpGet]
        public ActionResult Default()
        {
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Notice()
        {
            ViewBag.UserCode = OperatorProvider.GetCurrent().UserCode;
            ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
            return View();
        }

        [HttpGet]
        /// <summary>
        /// Lấy số người khám bệnh（Ghi sổ ngoại trú、Ghi sổ nội trú）
        /// </summary>
        public ActionResult GetVisitNum()
        {
            VisitNumBO BO = null;
            var userCode = OperatorProvider.GetCurrent().UserCode;
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
                var orgId = OperatorProvider.GetCurrent().OrganizeId;
                if (string.IsNullOrEmpty(orgId))
                {
                    throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
                }
                BO = _commonDmnService.GetVisitNum(false, orgId);
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = BO }.ToJson());
        }

        /// <summary>
        /// Lấy giá trị sinh ngẫu nhiên của trường nghiệp vụ（Tự tăng+Format）
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orgIdIsStar"></param>
        /// <param name="topOrgIdIsStar"></param>
        /// <param name="initFormat"></param>
        /// <param name="initFieldLength"></param>
        /// <returns></returns>
        public ActionResult GetNewFieldUniqueValue(string fieldName, bool? orgIdIsStar = null, bool? topOrgIdIsStar = null, string orgId = null, string topOrgId = null, string initFormat = "", int initFieldLength = 0)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                if (orgIdIsStar.HasValue)
                {
                    orgId = orgIdIsStar.Value ? "*" : OperatorProvider.GetCurrent().OrganizeId;
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
            //    value = null;   //???????????????Như vậy có phù hợp không
            //}
            else
            {
                value = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue(fieldName, orgId, topOrgId, initFormat);
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = value }.ToJson());
        }

    }
}