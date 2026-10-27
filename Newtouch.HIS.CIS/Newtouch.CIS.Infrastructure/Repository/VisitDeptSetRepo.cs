using System.Collections.Generic;
using Newtouch.CIS.Compat;
using System.Data.Common;
using System.Data.SqlClient;

using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using DbParameter = System.Data.Common.DbParameter;

namespace Newtouch.Repository
{
    /// <summary>
    /// rakhámCài đặt
    /// </summary>
    public class VisitDeptSetRepo : RepositoryBase<VisitDeptSetEntity>, IVisitDeptSetRepo
    {
        public VisitDeptSetRepo() : base()
        {
        }

        /// <summary>
        /// Lấyrakhámthông tin
        /// </summary>
        /// <param name="ysgh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<VisitDeptSetEntity> SelectData(string ysgh, string organizeId)
        {
            const string sql = @"
SELECT * 
FROM dbo.visit_deptSet(NOLOCK) 
WHERE ysgh=@ysgh AND zt='1'
AND OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@ysgh", ysgh),
                new SqlParameter("@OrganizeId",organizeId )
            };
            return FindList<VisitDeptSetEntity>(sql, param);
        }

        /// <summary>
        /// TheoIDvậtlýXóaghi chép
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteItem(string id)
        {
            return ExecuteSqlCommand(@"DELETE FROM dbo.visit_deptSet WHERE Id=@id", new SqlParameter("@id", id));
        }
    }
}