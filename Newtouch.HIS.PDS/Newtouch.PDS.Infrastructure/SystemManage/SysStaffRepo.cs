using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using SqlSugar;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysStaffRepo : RepositoryBase<SysStaffVEntity>, ISysStaffRepo
    {
        public SysStaffRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Lấy tổ chức Danh sách nhân viên hiệu lực
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysStaffVEntity> GetStaffListByOrganizeId(string orgId)
        {
            var sql = "select * from [NewtouchHIS_Base]..V_S_Sys_Staff with(nolock) where OrganizeId = @orgId";
            return this.FindList<SysStaffVEntity>(sql, new[] { new SugarParameter("@orgId", orgId) });
        }

        public IList<SysStaffVEntity> GetValidStaffListByOrganizeId(string orgId)
        {
            var sql = "select * from [NewtouchHIS_Base]..V_S_Sys_Staff with(nolock) where OrganizeId = @orgId and zt = '1'";
            return this.FindList<SysStaffVEntity>(sql, new[] { new SugarParameter("@orgId", orgId) });
        }


    }
}
