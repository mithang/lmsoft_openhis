using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using SqlSugar;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysDepartmentRepo : RepositoryBase<SysDepartmentVEntity>, ISysDepartmentRepo
    {
        public SysDepartmentRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        
        /// <summary>
        /// 获取机构科室列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysDepartmentVEntity> GetList(string orgId)
        {
            var sql = "select * from [NewtouchHIS_Base]..V_S_Sys_Department with(nolock) where OrganizeId = @orgId";
            return this.FindList<SysDepartmentVEntity>(sql, new[] { new SugarParameter("@orgId", orgId) });
        }

        /// <summary>
        /// 根据Code获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetNameByCode(string code, string orgId)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            var sql = "select Name from [NewtouchHIS_Base]..V_S_Sys_Department with(nolock) where Code = @code and OrganizeId = @orgId";
            return this.FirstOrDefault<string>(sql, new[] { new SugarParameter("@orgId", orgId)
                ,new SugarParameter("@code", code)});
        }

        public IList<SysDepartmentVEntity> GetList(string orgId, string zt, string keyword)
        {
            var sql = "select * from [NewtouchHIS_Base]..V_S_Sys_Department with(nolock) where OrganizeId = @orgId and zt = @zt";
            var pars = new System.Collections.Generic.List<SugarParameter>
            {
                new SugarParameter("@orgId", orgId),
                new SugarParameter("@zt", zt)
            };
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (Name like @keyword or Code like @keyword or py like @keyword)";
                pars.Add(new SugarParameter("@keyword", "%" + keyword + "%"));
            }
            return this.FindList<SysDepartmentVEntity>(sql, pars.ToArray());
        }


    }
}
