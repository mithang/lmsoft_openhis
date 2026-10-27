using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MRQC.Domain;
using Newtouch.MRQC.Domain.Entity.QcItemManage;
using Newtouch.MRQC.Domain.ValueObjects;
using Newtouch.MRQC.Infrastructure;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.MRQC.DomainServices
{
    public class QcBlzkDmnService : IQcBlzkDmnService
    {
        public void SubmitForm(ZkxmEntityVo entityvo)
        {
            string organizeId = OperatorProvider.GetCurrent().OrganizeId;
            string userCode = OperatorProvider.GetCurrent().UserCode;
            var db = MrqcDb.GetConnection();
            db.Ado.UseTran(() =>
            {
                if (entityvo.czlx == "update")
                {
                    var entity = db.Queryable<QcItemDataEntity>().First(p => p.Id == entityvo.Id);
                    entity.Bllmbmc = entityvo.Bllmbmc;
                    entity.Name = entityvo.Name;
                    entity.Score = entityvo.Score;
                    entity.Remark = entityvo.Remark;
                    entity.Px = entityvo.Px;
                    entity.LastModifyTime = DateTime.Now;
                    entity.LastModifierCode = userCode;
                    db.Updateable(entity).ExecuteCommand();
                }
                else
                {
                    int cnt = 0;
                    var last = db.Queryable<QcItemDataEntity>()
                        .Where(p => p.BlmbId == entityvo.BlmbId && p.OrganizeId == organizeId && p.zt == "1")
                        .OrderByDescending(p => p.Code).First();
                    if (last != null)
                    {
                        int.TryParse(last.Code, out cnt);
                    }
                    var entity = new QcItemDataEntity
                    {
                        Bllmbmc = entityvo.Bllmbmc,
                        Code = (cnt + 1).ToString(),
                        BlmbId = entityvo.BlmbId,
                        Name = entityvo.Name,
                        Score = entityvo.Score,
                        Remark = entityvo.Remark,
                        zklx = entityvo.zklx,
                        Px = entityvo.Px,
                        OrganizeId = organizeId,
                        CreateTime = DateTime.Now,
                        CreatorCode = userCode,
                        zt = "1"
                    };
                    db.Insertable(entity).ExecuteCommand();
                }
            });
        }

        public IList<ScoreItemVo> Getybbxbldata(string bllx, string orgId, string zyh)
        {
            string sqlstr = @"SELECT b.Zyh,a.Score,a.BlmbId,a.Code,a.Name,a.Remark,isnull(b.sl,0) qxs,NULL ysc
FROM [MR_Qc_ItemData] a with(nolock)
LEFT JOIN [Mr_Qc_Score] b with(nolock) on a.BlmbId = b.BllxId and a.Code=b.ScoreCode and a.OrganizeId = b.OrganizeId and Zyh=@zyh
WHERE a.BlmbId = @bllx and a.OrganizeId =@orgId order by a.px";
            var pars = new[]
            {
                new SugarParameter("@zyh", zyh),
                new SugarParameter("@orgId", orgId),
                new SugarParameter("@bllx", bllx)
            };
            return MrqcDb.GetConnection().SqlQueryable<ScoreItemVo>(sqlstr).AddParameters(pars).ToList();
        }

        public void SaveScoreDate(List<ScoreEntity> entity, string orgId, string CreatorCode, string bllx, string blmc, string zyh)
        {
            var db = MrqcDb.GetConnection();
            db.Ado.UseTran(() =>
            {
                var existing = db.Queryable<MrQcScoreEntity>()
                    .Where(p => p.Zyh == zyh && p.BllxId == bllx && p.zt == "1" && p.OrganizeId == orgId).ToList();
                foreach (var item in existing)
                {
                    db.Deleteable<MrQcScoreEntity>().Where(p => p.Id == item.Id).ExecuteCommand();
                }
                foreach (var item in entity)
                {
                    var data = new MrQcScoreEntity
                    {
                        Id = Guid.NewGuid().ToString(),
                        Zyh = zyh,
                        BllxId = bllx,
                        Blmc = blmc,
                        ScoreCode = item.Code,
                        ScoreMc = item.Name,
                        ScoreMcValue = item.Score,
                        sl = item.qxs,
                        OrganizeId = orgId,
                        zt = "1",
                        CreateTime = DateTime.Now,
                        CreatorCode = CreatorCode
                    };
                    db.Insertable(data).ExecuteCommand();
                }
            });
        }
    }
}
