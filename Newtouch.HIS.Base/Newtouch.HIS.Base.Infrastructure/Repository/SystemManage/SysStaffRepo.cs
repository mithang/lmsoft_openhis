using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository.SystemManage
{
    /// <summary>
    /// 
    /// </summary>
    public class SysStaffRepo : RepositoryBase<SysStaffEntity>, ISysStaffRepo
    {
        public SysStaffRepo()
            
        {
        }

        /// <summary>
        /// Lấy tất cả nhân viên hệ thống thuộc tổ chức(Không bao gồm tổ chức con)
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysStaffEntity> GetsatffListByOrg(string OrganizeId)
        {
            var sql = @"select * from Sys_Staff where OrganizeId=@OrganizeId";
            return this.FindList<SysStaffEntity>(sql, new SqlParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId)
            });
        }

        /// <summary>
        /// Lấy nhân viên quản lýIdDanh sách TheoUserId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<string> GetCurStaffIdListByUserId(string UserId)
        {
            var sql = "select StaffId from Sys_UserStaff where UserId = @UserId";
            return this.FindList<string>(sql, new SqlParameter[] {
                new SqlParameter("@UserId",UserId)
            });
        }

        public IList<SysStaffEntity> GetStaffList(string orgId, string keyword = null)
        {
            var sql = @"select * from Sys_Staff(nolock) where OrganizeId = @orgId
and (gh like @searchKeyword or name like @searchKeyword) and zt=1
order by CreateTime desc";

            return this.FindList<SysStaffEntity>(sql, new[] {
                new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
        }
    }
}
