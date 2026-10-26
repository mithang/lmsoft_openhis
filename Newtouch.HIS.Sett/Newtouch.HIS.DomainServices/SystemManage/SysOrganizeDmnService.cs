using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Tổ chức DmnService
    /// </summary>
    public class SysOrganizeDmnService : DmnServiceBase, ISysOrganizeDmnService
    {
        public SysOrganizeDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Lấy nhóm 织 dư ới của tất cả các hiệu lực nhóm 织(parent)
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeVEntity> GetValidListByParentOrg(string parentOrgId)
        {
            var sql = @" 
    WITH cteTree
        AS (SELECT *
              FROM [NewtouchHIS_Base]..V_S_Sys_Organize(nolock)
              WHERE Id = @parentOrgId and zt = '1' --第一个查询作为递归的基点(锚点)
            UNION ALL
            SELECT [NewtouchHIS_Base]..V_S_Sys_Organize.*     --第二个查询作为递归成员， 下属成员的结果为空时，此递归结束。
              FROM
                   cteTree INNER JOIN [NewtouchHIS_Base]..V_S_Sys_Organize(nolock) ON cteTree.Id= [NewtouchHIS_Base]..V_S_Sys_Organize.ParentId and [NewtouchHIS_Base]..V_S_Sys_Organize.zt = '1') 
        SELECT  *
          FROM cteTree 

";
            return this.FindList<SysOrganizeVEntity>(sql, new SqlParameter[] {
                new SqlParameter("@parentOrgId",parentOrgId)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool IsHospital(string orgId)
        {
            var sql = "select CategoryCode from [NewtouchHIS_Base]..V_S_Sys_Organize(nolock) where Id = @orgId";
            var code = this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId) });
            return code == "Hospital";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool IsClinic(string orgId)
        {
            var sql = "select CategoryCode from [NewtouchHIS_Base]..V_S_Sys_Organize(nolock) where Id = @orgId";
            var code = this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId) });
            return code == "Clinic";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool IsMedicalOrganize(string orgId)
        {
            var sql = "select CategoryCode from [NewtouchHIS_Base]..V_S_Sys_Organize(nolock) where Id = @orgId";
            var code = this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId) });
            return code == "Hospital" || code == "Clinic";
        }

        /// <summary>
        /// Lấy tất cả các của tổ chức cấp cao nhất
        /// </summary>
        /// <returns></returns>
        public IList<SysOrganizeVEntity> GetValidTopOrgList()
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_Sys_Organize where zt = '1' and ParentId is null";
            return this.FindList<SysOrganizeVEntity>(sql);
        }

        /// <summary>
        /// LấyOrgTên
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetNameByOrgId(string orgId)
        {
            var sql = @"select Name from [NewtouchHIS_Base]..V_S_Sys_Organize where Id = @orgId";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }

        /// <summary>
        /// Lấy Userid Tư ơng ứng của y tế cơ quan List
        /// </summary>
        /// <returns></returns>
        public IList<SysOrganizeVEntity> GetMedicalOrganizeListByUserId(string userId)
        {
            var sql = @"select distinct c.* from [NewtouchHIS_Base]..V_S_Sys_UserStaff a
left join [NewtouchHIS_Base]..V_S_Sys_Staff b
on a.StaffId = b.Id
left join [NewtouchHIS_Base]..V_S_Sys_Organize c
on b.OrganizeId = c.Id

where a.zt = '1' and b.zt = '1' and c.zt = '1' and c.Id is not null and a.UserId = @userId
and (c.CategoryCode = 'Hospital' or c.CategoryCode = 'Clinic')";
            return this.FindList<SysOrganizeVEntity>(sql, new[] { new SqlParameter("@userId", userId) });
        }

    }
}
