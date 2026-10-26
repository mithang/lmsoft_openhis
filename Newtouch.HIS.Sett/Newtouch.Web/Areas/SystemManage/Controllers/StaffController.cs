using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// Nhân sự hệ thống Mở rộng
    /// </summary>
    [AutoResolveIgnore]
    public class StaffController : FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers.SysStaffController
    {
        private readonly ISysUserDmnService _sysUserDmnService;

        public StaffController(ISysOrganizeDmnService sysOrganizeDmnService, ISysDepartmentRepo sysDepartmentRepo, ISysUserDmnService sysUserDmnService)
            : base(sysOrganizeDmnService, sysDepartmentRepo, sysUserDmnService)
        {
            this._sysUserDmnService = sysUserDmnService;
        }

        /// <summary>
        /// Lấy nhân sự theo vị trí
        /// </summary>
        /// <param name="orgId">Tổ chức Id</param>
        /// <param name="dutyCode">Vị trí Code</param>
        /// <param name="ks">Khoa</param>
        /// <param name="keyword">Từ khóa Khớp tên nhân sự hoặc mã nhân viên</param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetStaffByDutyCode(string orgId, string dutyCode, string ks, string keyword)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                orgId = this.OrganizeId;
            }
            if (string.IsNullOrWhiteSpace(orgId) || string.IsNullOrWhiteSpace(dutyCode))
            {
                return null;
            }
            var list = this._sysUserDmnService.GetStaffByDutyCode(orgId, dutyCode);
            if (!string.IsNullOrWhiteSpace(ks))
            {
                list = list.Where(a => a.ks == ks).ToList();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                list = list.Where(a => a.StaffName.Contains(keyword.Trim()) || a.StaffGh.Contains(keyword.Trim())).ToList();
            }
            return Content(list.ToJson());
        }

    }
}