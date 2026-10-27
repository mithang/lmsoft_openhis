using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// Herp-local replacement for the Framework binary ISysUserDmnService
    /// (FrameworkBase.MultiOrg.Domain.IDomainServices.ISysUserDmnService).
    /// Only the method DepartmentManage actually uses is declared here; the
    /// binary interface's other members (CheckLogin, staff lists, password
    /// revision, ...) are NOT ported because no Herp controller calls them.
    /// Signature verified by reflection over the shipped binary:
    /// List&lt;SysDepartmentVEntity&gt; SelectUserDepartment(string userCode, string organizeId).
    /// </summary>
    public interface ISysUserDmnService
    {
        /// <summary>
        /// Departments belonging to a user (by login account) within an organization.
        /// </summary>
        /// <param name="userCode">Login account (Sys_User.Account).</param>
        /// <param name="organizeId">Organization id (department's OrganizeId).</param>
        /// <returns>Department view rows for that user.</returns>
        IList<SysDepartmentVEntity> SelectUserDepartment(string userCode, string organizeId);

        /// <summary>
        /// Valid staff of an organization (used by ProfitAndLossController.GetZRRList).
        /// Merged at integration from the StorageManage port, which implemented
        /// it via SysStaffRepo (BaseDb Sys_Staff, zt = '1').
        /// </summary>
        List<SysStaffVEntity> GetStaffListByOrg(string organizeId);
    }
}
