using System.Collections.Generic;
using System.Linq;
using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.Repository
{
    public class SysDepartmentRepo : RepositoryBase<SysDepartmentVEntity>, ISysDepartmentRepo
    {

        public SysDepartmentRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// LấyCơ quanKhoaDanh sách
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysDepartmentVEntity> GetList(string orgId, string keyword = null)
        {
            var sql = "";
            IList<SysDepartmentVEntity> list = null;
            if (string.IsNullOrEmpty(keyword))
            {
                sql = "select * from [NewtouchHIS_Base]..V_S_Sys_Department with(nolock) where zt = '1' and OrganizeId = @orgId";
                list= this.FindList<SysDepartmentVEntity>(sql, new[] { new SqlParameter("@orgId", orgId) });
            }
            else
            {
                sql = "select * from [NewtouchHIS_Base]..V_S_Sys_Department with(nolock) where zt = '1' and OrganizeId = @orgId and ( Code like @keyword or Name like @keyword )";
                list = this.FindList<SysDepartmentVEntity>(sql, new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@keyword", "%" + keyword.Trim() + "%") });
            }
            return list;
        }

        /// <summary>
        /// theo Code lấykhoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public SysDepartmentVEntity GetByCode(string code, string orgId)
        {
            var sql = "select * from [NewtouchHIS_Base]..V_S_Sys_Department with(nolock) where code = @code OrganizeId = @orgId";
            return this.FirstOrDefault<SysDepartmentVEntity>(sql, new[] { new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@code", code)});
        }

        /// <summary>
        /// theo Name lấyCode
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetCodeByName_FirstOrDefault(string name, string orgId)
        {
            var sql = "select Code from [NewtouchHIS_Base]..V_S_Sys_Department with(nolock) where Name = @name and OrganizeId = @orgId";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@name", name)});
        }

        /// <summary>
        /// theo Code lấy tên
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
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@code", code)});
        }

    }
}
