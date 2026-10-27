using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using SqlSugar;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Staff repository (Herp-local; the original was a Framework binary repo).
    /// Reads NewtouchHIS_Base.dbo.Sys_Staff via the BaseDb connection, so it
    /// overrides the repository Db like SysDepartmentRepo does.
    /// </summary>
    public class SysStaffRepo : RepositoryBase<SysStaffVEntity>, ISysStaffRepo
    {
        public SysStaffRepo()
        {
        }

        protected override SqlSugarProvider Db => HerpDb.GetBaseConnection();

        public List<SysStaffVEntity> GetValidStaffListByOrganizeId(string organizeId)
        {
            const string sql = @"SELECT Id, gh, Name, py FROM dbo.Sys_Staff(NOLOCK)
WHERE OrganizeId = @OrganizeId AND zt = '1'";
            return FindList<SysStaffVEntity>(sql, new SugarParameter("@OrganizeId", organizeId));
        }
    }
}
