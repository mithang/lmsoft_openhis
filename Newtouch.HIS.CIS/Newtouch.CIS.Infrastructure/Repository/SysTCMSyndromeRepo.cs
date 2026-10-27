using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Newtouch.CIS.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Repository;

namespace Newtouch.CIS.Repository
{
    public class SysTCMSyndromeRepo : ISysTCMSyndromeRepo
    {
        public IList<SysTCMSyndromeVEntity> GetList(string orgId, string keyword)
        {
            var sql = @"SELECT * FROM V_S_xt_zyzh WHERE zt = '1' AND (OrganizeId = @orgId OR OrganizeId = '*')
AND (@kw = '' OR zhmc LIKE @like OR py LIKE @like OR zhCode LIKE @like)";
            return BaseDb.GetConnection().Ado.SqlQuery<SysTCMSyndromeVEntity>(sql,
                Newtouch.CIS.Compat.SqlParam.ToSugar(new SqlParameter[] {
                    new SqlParameter("@orgId", orgId ?? ""),
                    new SqlParameter("@kw", keyword ?? ""),
                    new SqlParameter("@like", "%" + (keyword ?? "") + "%") })).ToList();
        }
    }
}
