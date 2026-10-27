using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Newtouch.EMR.Repository;
using Newtouch.EMR.Infrastructure;

namespace Newtouch.EMR.DomainServices
{
    public class MedicalRulesDmnService : EmrServiceBase, IMedicalRulesDmnService
    {
        public MedicalRulesDmnService()
        {
        }

        public void SubmitForm(RulesEntityVo entityvo)
        {
            string OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
            using (var db = new EmrTransaction().BeginTrans())
            {
                if (entityvo.czlx == "update")
                {
                    var entity = db.IQueryable<MrWritingRulesEntity>().Where(p => p.Id == entityvo.Id).Take(1).ToList().FirstOrDefault();
                    entity.Bllx = entityvo.Bllx;
                    entity.RulesDay = entityvo.RulesDay;
                    entity.RulesType = entityvo.RulesType;
                    entity.Remark = entityvo.Remark;
                    entity.Px = entityvo.Px;
                    entity.LastModifyTime = DateTime.Now;
                    entity.LastModifierCode = OperatorProvider.GetCurrent().UserCode;
                    db.Update(entity);
                }
                else
                {
                    MrWritingRulesEntity entity = new MrWritingRulesEntity();
                    entity.Id = Guid.NewGuid().ToString();
                    entity.Bllx = entityvo.Bllx;
                    entity.RulesType = entity.RulesType;
                    entity.RulesType = entityvo.RulesType;
                    entity.RulesDay = entityvo.RulesDay;
                    entity.Remark = entityvo.Remark;
                    entity.Px = entityvo.Px;
                    entity.OrganizeId = OrganizeId;
                    entity.CreateTime = DateTime.Now;
                    entity.CreatorCode = OperatorProvider.GetCurrent().UserCode;
                    entity.zt = "1";
                    db.Insert(entity);
                }


                db.Commit();
            }
        }
    }
}
