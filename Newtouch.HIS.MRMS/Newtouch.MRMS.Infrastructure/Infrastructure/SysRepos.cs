using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtouch.MR.ManageSystem.Domain.DTO;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using SqlSugar;

namespace Newtouch.MR.ManageSystem.Infrastructure
{
    /// <summary>Department lookup row (NewtouchHIS_Base..Sys_Department).</summary>
    public class SysDepartmentDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string py { get; set; }
    }

    public class SysConfigRepo : ISysConfigRepo
    {
        public ISugarQueryable<SysConfigEntity> IQueryable()
        {
            return MrmsDb.GetConnection().Queryable<SysConfigEntity>();
        }

        public SysConfigEntity GetByCode(string code, string orgId)
        {
            var q = MrmsDb.GetConnection().Queryable<SysConfigEntity>()
                .Where(c => c.Code == code && c.zt == "1");
            if (!string.IsNullOrWhiteSpace(orgId))
            {
                var mine = q.Where(c => c.OrganizeId == orgId).First();
                if (mine != null) return mine;
            }
            return q.Where(c => c.OrganizeId == "*").First();
        }

        public IList<SysConfigEntity> GetList(string organizeId, string zt)
        {
            var q = MrmsDb.GetConnection().Queryable<SysConfigEntity>();
            if (!string.IsNullOrWhiteSpace(organizeId)) q = q.Where(c => c.OrganizeId == organizeId);
            if (!string.IsNullOrWhiteSpace(zt) && zt != "*") q = q.Where(c => c.zt == zt);
            return q.ToList();
        }

        public int Insert(IEnumerable<SysConfigEntity> entities)
        {
            return MrmsDb.GetConnection().Insertable(entities.ToList()).ExecuteCommand();
        }
    }

    public class SysDiagnosisRepo : ISysDiagnosisRepo
    {
        public IList<SysZdListDto> GetList(string orgId, string keyword, string zdlx, string ybnhlx)
        {
            string sql = @"select zdCode, zdCode as zddm, zdmc, icd10, icd10fjm, py
from [NewtouchHIS_Base].[dbo].[xt_zd] with(nolock)
where zt='1' and OrganizeId in ('*', @orgId)";
            var pars = new List<SugarParameter> { new SugarParameter("@orgId", orgId ?? "") };
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (zdmc like @keyword or py like @keyword or zdCode like @keyword)";
                pars.Add(new SugarParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(zdlx))
            {
                sql += " and zdlx = @zdlx";
                pars.Add(new SugarParameter("@zdlx", zdlx));
            }
            if (!string.IsNullOrWhiteSpace(ybnhlx))
            {
                sql += " and ybnhlx = @ybnhlx";
                pars.Add(new SugarParameter("@ybnhlx", ybnhlx));
            }
            return MrmsDb.GetBaseConnection().SqlQueryable<SysZdListDto>(sql).AddParameters(pars.ToArray()).ToList();
        }
    }

    public class SysDepartmentRepo : ISysDepartmentRepo
    {
        public IList<SysDepartmentDto> GetList(string orgId, string zt, string keyword)
        {
            string sql = @"select Code, Name, py
from [NewtouchHIS_Base].[dbo].[Sys_Department] with(nolock)
where OrganizeId = @orgId";
            var pars = new List<SugarParameter> { new SugarParameter("@orgId", orgId ?? "") };
            if (!string.IsNullOrWhiteSpace(zt))
            {
                sql += " and zt = @zt";
                pars.Add(new SugarParameter("@zt", zt));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (Name like @keyword or py like @keyword or Code like @keyword)";
                pars.Add(new SugarParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return MrmsDb.GetBaseConnection().SqlQueryable<SysDepartmentDto>(sql).AddParameters(pars.ToArray()).ToList();
        }
    }
}
