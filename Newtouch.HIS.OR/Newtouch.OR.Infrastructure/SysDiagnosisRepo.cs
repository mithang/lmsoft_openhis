using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using Newtouch.OR.ManageSystem.Infrastructure;
using SqlSugar;
using System.Collections.Generic;

namespace Newtouch.OR.ManageSystem.Repository
{
    public class SysDiagnosisRepo : ISysDiagnosisRepo
    {
        private SqlSugarProvider Db => OrDb.GetConnection();

        public IList<SysDiagnosisVO> GetList(string orgId, string keyword, string zdlx, string ybnhlx)
        {
            var sql = @"select zdId, zdCode, icd10, icd10fjm, zdmc, py, OrganizeId, wb, zt, zdlx, ybnhlx
from [NewtouchHIS_Base]..V_S_xt_zd with(nolock) where zt='1'";
            var pars = new List<SugarParameter>();
            if (!string.IsNullOrWhiteSpace(orgId))
            {
                sql += " and OrganizeId=@orgId";
                pars.Add(new SugarParameter("@orgId", orgId));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (zdCode like @kwd or zdmc like @kwd or py like @kwd or icd10 like @kwd)";
                pars.Add(new SugarParameter("@kwd", "%" + keyword.Trim() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(zdlx))
            {
                sql += " and zdlx=@zdlx";
                pars.Add(new SugarParameter("@zdlx", zdlx));
            }
            if (!string.IsNullOrWhiteSpace(ybnhlx))
            {
                sql += " and ybnhlx=@ybnhlx";
                pars.Add(new SugarParameter("@ybnhlx", ybnhlx));
            }
            return Db.SqlQueryable<SysDiagnosisVO>(sql).AddParameters(pars.ToArray()).ToList();
        }
    }
}
