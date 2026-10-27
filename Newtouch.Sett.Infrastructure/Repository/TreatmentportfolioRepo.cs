using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Repository
{
    /// <summary>
    /// SqlSugar port of the original TreatmentportfolioRepo (single table
    /// mz_gh_zlxmzh in the Sett DB, joined to Base xt_sfdl for the grid view).
    /// Delete parity note: the original EF Delete(predicate) is reproduced as a
    /// soft delete (zt = '0') because every read in the original filters
    /// zt = '1', so visible behaviour is identical and the rows stay recoverable.
    /// </summary>
    public class TreatmentportfolioRepo : ITreatmentportfolioRepo
    {
        private SqlSugarProvider Db => SettDb.GetConnection();

        public void ADDInsert(TreatmentportfolioEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Id = keyValue;
                entity.LastModifyTime = DateTime.Now;
                Db.Updateable(entity)
                    .IgnoreColumns(p => new { p.CreateTime, p.CreatorCode })
                    .Where(p => p.Id == keyValue)
                    .ExecuteCommand();
            }
            else
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.CreateTime = DateTime.Now;
                if (string.IsNullOrEmpty(entity.zt))
                {
                    entity.zt = "1";
                }
                Db.Insertable(entity).ExecuteCommand();
            }
        }

        public void ADDrowdata(TreatmentportfolioEntity entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            entity.CreateTime = DateTime.Now;
            if (string.IsNullOrEmpty(entity.zt))
            {
                entity.zt = "1";
            }
            Db.Insertable(entity).ExecuteCommand();
        }

        public void deleteid(string keyValue, string orgId)
        {
            Db.Updateable<TreatmentportfolioEntity>()
                .SetColumns(p => p.zt == "0")
                .Where(p => p.zhcode == keyValue && p.OrganizeId == orgId && p.zt == "1")
                .ExecuteCommand();
        }

        public void deletemc(string keyValue, string zhcodemc, string orgId)
        {
            Db.Updateable<TreatmentportfolioEntity>()
                .SetColumns(p => p.zt == "0")
                .Where(p => p.zlxmmc == keyValue && p.zhcode == zhcodemc && p.OrganizeId == orgId && p.zt == "1")
                .ExecuteCommand();
        }

        public IList<TreatmentportfolioEntity> TJchaxun(string zhcode, string sfxmmc, string orgId)
        {
            return Db.SqlQueryable<TreatmentportfolioEntity>(
                "select * from mz_gh_zlxmzh with (nolock) where zhcode = @zhcode and zlxmmc = @sfxmmc and OrganizeId = @orgId and zt = '1'")
                .AddParameters(new[]
                {
                    new SugarParameter("@zhcode", zhcode),
                    new SugarParameter("@sfxmmc", sfxmmc),
                    new SugarParameter("@orgId", orgId),
                })
                .ToList();
        }

        public IList<TreatmentportfolioEntity> Codecx(string orgId, string zhcode)
        {
            return Db.SqlQueryable<TreatmentportfolioEntity>(
                "select * from mz_gh_zlxmzh with (nolock) where zhcode = @zhcode and OrganizeId = @orgId and zt = '1'")
                .AddParameters(new[]
                {
                    new SugarParameter("@zhcode", zhcode),
                    new SugarParameter("@orgId", orgId),
                })
                .ToList();
        }

        public IList<TreamentviewVO> Keyword(Pagination pagination, string keyword, string orgId)
        {
            const string fromWhere = @"from [NewtouchHIS_Sett].[dbo].[mz_gh_zlxmzh] a with (nolock),
                [NewtouchHIS_Base].[dbo].[xt_sfdl] b with (nolock)
                where a.sfdl = b.dlCode and a.OrganizeId = b.OrganizeId and a.zt = '1' and a.OrganizeId = @orgId";
            var pars = new List<SugarParameter> { new SugarParameter("@orgId", orgId) };
            var keywordFilter = "";
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keywordFilter = " and (a.zhmc like @keyword or a.zhcode like @keyword)";
                pars.Add(new SugarParameter("@keyword", "%" + keyword + "%"));
            }
            pagination.records = Db.Ado.GetInt(
                "select count(*) " + fromWhere + keywordFilter, pars.ToArray());
            var page = pagination.page ?? 1;
            var rows = pagination.rows ?? 15;
            var pagePars = new List<SugarParameter>(pars)
            {
                new SugarParameter("@offset", (page - 1) * rows),
                new SugarParameter("@rows", rows),
            };
            return Db.SqlQueryable<TreamentviewVO>(
                "select a.*, b.dlmc, a.zhmc + '-' + a.zhcode as zhmccode " + fromWhere + keywordFilter +
                " order by a.zhcode offset @offset rows fetch next @rows rows only")
                .AddParameters(pagePars.ToArray())
                .ToList();
        }
    }
}
