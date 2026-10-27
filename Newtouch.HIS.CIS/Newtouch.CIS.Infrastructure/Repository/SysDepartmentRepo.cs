using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Newtouch.CIS.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Repository;

namespace Newtouch.CIS.Repository
{
    public class SysDepartmentRepo : ISysDepartmentRepo
    {
        public IList<SysDepartmentEntity> GetList(string orgId, string flag, string keyword = null)
        {
            var sql = "SELECT * FROM Sys_Department WHERE zt = '1' AND (OrganizeId = @orgId OR OrganizeId = '*')";
            return BaseDb.GetConnection().Ado.SqlQuery<SysDepartmentEntity>(sql,
                Newtouch.CIS.Compat.SqlParam.ToSugar(new SqlParameter[] {
                    new SqlParameter("@orgId", orgId ?? "") })).ToList();
        }
    }
}
