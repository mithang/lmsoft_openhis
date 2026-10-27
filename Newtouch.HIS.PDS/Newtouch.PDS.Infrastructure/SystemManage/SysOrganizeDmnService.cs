using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using SqlSugar;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Tổ chức DmnService
    /// </summary>
    public class SysOrganizeDmnService : DmnServiceBase, ISysOrganizeDmnService
    {
        
        /// <summary>
        /// Lấy loại tổ chức（Tập đoàn、Bệnh viện）
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetOrganizeTypeByOrganizeId(string orgId)
        {
            var Sql = @"select b.Name from [NewtouchHIS_Base]..V_S_Sys_Organize a
left join [NewtouchHIS_Base]..V_S_Sys_ItemsDetail b
on a.CategoryCode = b.Code
where a.Id = @orgId and (b.TopOrganizeId = @topOrgId or b.TopOrganizeId = '*')";
            return this.FirstOrDefault<string>(Sql, new[] { new SugarParameter("@orgId", orgId), new SugarParameter("@topOrgId", Constants.TopOrganizeId) });
        }

        /// <summary>
        /// Có chứa tổ chức cấp dưới không，Trả về nếu có tổ chức cấp dướitrue
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool IsHasLowerOrganize(string orgId)
        {
            var Sql = @"select 1 from [NewtouchHIS_Base]..V_S_Sys_Organize where ParentId = @orgId";
            return this.FirstOrDefault<int>(Sql, new[] { new SugarParameter("@orgId", orgId) }) == 1;
        }

        /// <summary>
        /// Lấy tất cả tổ chức hiệu lực thuộc tổ chức（parent）
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeVEntity> GetValidListByParentOrg(string parentOrgId)
        {
            var sql = @" 
    WITH cteTree
        AS (SELECT *
              FROM [NewtouchHIS_Base]..V_S_Sys_Organize
              WHERE Id = @parentOrgId --Truy vấn đầu tiên làm điểm cơ sở đệ quy(Điểm neo)
            UNION ALL
            SELECT [NewtouchHIS_Base]..V_S_Sys_Organize.*     --Truy vấn thứ hai làm thành viên đệ quy， Khi kết quả thành viên cấp dưới trống，Đệ quy này kết thúc。
              FROM
                   cteTree INNER JOIN [NewtouchHIS_Base]..V_S_Sys_Organize ON cteTree.Id= [NewtouchHIS_Base]..V_S_Sys_Organize.ParentId) 
        SELECT  *
          FROM cteTree 

";
            return this.FindList<SysOrganizeVEntity>(sql, new SugarParameter[] {
                new SugarParameter("@parentOrgId",parentOrgId)
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
            var code = this.FirstOrDefault<string>(sql, new[] { new SugarParameter("@orgId", orgId) });
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
            var code = this.FirstOrDefault<string>(sql, new[] { new SugarParameter("@orgId", orgId) });
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
            var code = this.FirstOrDefault<string>(sql, new[] { new SugarParameter("@orgId", orgId) });
            return code == "Hospital" || code == "Clinic";
        }

        /// <summary>
        /// Lấy tất cả tổ chức cấp cao nhất
        /// </summary>
        /// <returns></returns>
        public IList<SysOrganizeVEntity> GetValidTopOrgList()
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_Sys_Organize where ParentId is null";
            return this.FindList<SysOrganizeVEntity>(sql);
        }

    }
}
