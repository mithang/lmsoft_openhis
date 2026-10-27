using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using SqlSugar;

namespace Newtouch.Herp.DomainServices
{
    /// <summary>
    /// Herp-local implementation of the one ISysUserDmnService method that
    /// DepartmentManage uses. The original was a Framework binary service with
    /// no Herp source; its contract was recovered by reflection over the
    /// shipped FrameworkBase.MultiOrg.Domain.dll:
    /// List&lt;SysDepartmentVEntity&gt; SelectUserDepartment(string userCode, string organizeId).
    ///
    /// Resolution chain (verified against the live Base DB, read-only):
    /// Sys_User.Account = userCode
    ///   -&gt; Sys_UserStaff (zt = '1') on UserId
    ///   -&gt; Sys_Staff (zt = '1') on StaffId, whose DepartmentCode names the department
    ///   -&gt; V_S_Sys_Department on Code, filtered to the given OrganizeId.
    /// Example: account 000000 -&gt; staff gh 000000 -&gt; DepartmentCode KS01
    /// (Khoa Noi). A user linked to several staff rows gets each department once.
    ///
    /// UNCERTAINTY (reported, not hidden): the binary's exact SQL is not
    /// recoverable, so the zt = '1' filters on UserStaff/Staff/Department are
    /// the most reasonable reading (they match how DepartmentQuery filters
    /// zt = '1'). If the original returned inactive departments too, only
    /// users with inactive department links would see a difference.
    /// </summary>
    public class SysUserDmnService : DmnServiceBase, ISysUserDmnService
    {
        // All tables read here live in the Base DB, like SysDepartmentRepo.
        protected override SqlSugarProvider Db => HerpDb.GetBaseConnection();

        // Merged at integration: GetStaffListByOrg comes from the
        // StorageManage port (same interface, same DI name). It delegates to
        // SysStaffRepo, which reads BaseDb Sys_Staff via its own override.
        private readonly ISysStaffRepo _sysStaffRepo;

        public SysUserDmnService(ISysStaffRepo sysStaffRepo)
        {
            _sysStaffRepo = sysStaffRepo;
        }

        public List<SysStaffVEntity> GetStaffListByOrg(string organizeId)
        {
            return _sysStaffRepo.GetValidStaffListByOrganizeId(organizeId);
        }

        public IList<SysDepartmentVEntity> SelectUserDepartment(string userCode, string organizeId)
        {
            if (string.IsNullOrWhiteSpace(userCode) || string.IsNullOrWhiteSpace(organizeId))
            {
                return new List<SysDepartmentVEntity>();
            }

            const string sql = @"select distinct d.Id, d.OrganizeId, d.ParentId, d.Name, d.Code, d.yjbz, d.mzzybz, d.py, d.zt
from dbo.V_S_Sys_Department d (nolock)
inner join dbo.Sys_Staff s (nolock) on s.DepartmentCode = d.Code
inner join dbo.Sys_UserStaff us (nolock) on us.StaffId = s.Id and us.zt = '1'
inner join dbo.Sys_User u (nolock) on u.Id = us.UserId
where u.Account = @userCode and d.OrganizeId = @organizeId and s.zt = '1' and d.zt = '1'";
            return FindList<SysDepartmentVEntity>(sql,
                new SugarParameter("@userCode", userCode),
                new SugarParameter("@organizeId", organizeId));
        }
    }
}
