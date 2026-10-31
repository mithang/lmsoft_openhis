using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using System.Collections.Generic;
using SqlSugar;
using System.Text;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Khoa phòng hệ thống
    /// </summary>
    public class SysDepartmentRepo : RepositoryBase<SysDepartmentVEntity>, ISysDepartmentRepo
    {
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysDepartmentRepo()
        {
        }

        protected override SqlSugarProvider Db => HerpDb.GetBaseConnection();

        /// <summary>
        /// Lấy danh sách khoa phòng
        /// </summary>
        /// <param name="orgId">Cơ sở y tế</param>
        /// <param name="zt">Cờ hiệu lực.0 Không hiệu lực 1 Hiệu lực null,Empty Tất cả</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysDepartmentVEntity> GetList(string orgId, string zt = null, string keyword = null)
        {
            var sql = new StringBuilder("select Id, OrganizeId, ParentId, Name, Code, yjbz, mzzybz, py, zt from dbo.V_S_Sys_Department(nolock) where OrganizeId = @orgId ");
            var pars = new List<SugarParameter>
            {
                new SugarParameter("@orgId", orgId)
            };
            if (!string.IsNullOrWhiteSpace(zt))
            {
                sql.AppendLine(" and zt = @zt");
                pars.Add(new SugarParameter("@zt", zt));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.AppendLine(" and ( Code like @keyword or Name like @keyword )");
                pars.Add(new SugarParameter("@keyword", "%" + keyword.Trim() + "%"));
            }

            return FindList<SysDepartmentVEntity>(sql.ToString(), pars.ToArray());
        }

        /// <summary>
        /// Theo Code Lấy tên khoa phòng
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
            const string sql = @"select Name from dbo.V_S_Sys_Department(nolock) where Code = @code and OrganizeId = @orgId";
            return FirstOrDefaultScalar<string>(sql, new[] { new SugarParameter("@orgId", orgId), new SugarParameter("@code", code) });
        }

        /// <summary>
        /// Theo Code Lấy thực thể khoa phòng
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public SysDepartmentVEntity GetEntityByCode(string code, string orgId)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            const string sql = "select Id, OrganizeId, ParentId, Name, Code, yjbz, mzzybz, py, zt from dbo.V_S_Sys_Department(nolock) where Code = @code and OrganizeId = @orgId";
            return FirstOrDefault<SysDepartmentVEntity>(sql, new[] { new SugarParameter("@orgId", orgId), new SugarParameter("@code", code) });
        }

        /// <summary>
        /// Theo Name Lấy Code
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetCodeByNameFirstOrDefault(string name, string orgId)
        {
            const string sql = "select Code from dbo.V_S_Sys_Department(nolock) where Name = @name and OrganizeId = @orgId";
            return FirstOrDefaultScalar<string>(sql, new[] { new SugarParameter("@orgId", orgId), new SugarParameter("@name", name) });
        }
    }
}
