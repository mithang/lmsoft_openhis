using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        /// Lấy danh sách nhân viên hợp lệ của tổ chức
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysStaffVEntity> GetStaffListByOrganizeId(string orgId)
        {
            var sql = "select * from [NewtouchHIS_Base]..V_S_Sys_Staff with(nolock) where OrganizeId = @orgId";
            return this.FindList<SysStaffVEntity>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }

    }
}
