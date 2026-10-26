using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// Nhân viên hệ thống 扩展
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
        /// Lấy nhân viên vị trí công tác
        /// </summary>
        /// <param name="orgId">Tổ chứcId</param>
        /// <param name="dutyCode">Vị trí công tácCode</param>
        /// <param name="ks">Khoa</param>
        /// <param name="keyword">Từ khóa 匹配人员名称或工号</param>
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