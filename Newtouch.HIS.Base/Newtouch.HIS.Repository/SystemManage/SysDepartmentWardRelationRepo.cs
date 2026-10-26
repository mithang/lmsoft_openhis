using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysDepartmentWardRelationRepo : RepositoryBase<SysDepartmentWardRelationEntity>, ISysDepartmentWardRelationRepo
    {
        public SysDepartmentWardRelationRepo(IBaseDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// Tìm thông tin liên kết khu bệnh của khoa
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public IList<SysDepartmentWardRelationEntity> GetDeptWardList(string deptId)
        {
            if (string.IsNullOrWhiteSpace(deptId))
            {
                return null;
            }
            string sql = @"select * from Sys_DepartmentWardRelation with(nolock) 
                            where zt='1' and Departmentid= @deptId";

            return this.FindList<SysDepartmentWardRelationEntity>(sql, new[] { new SqlParameter("@deptId", deptId) });
        }

    }
}
