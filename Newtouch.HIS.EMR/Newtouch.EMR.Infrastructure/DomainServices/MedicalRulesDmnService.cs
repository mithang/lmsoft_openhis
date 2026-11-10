using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Exceptions;
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
            // G17: entityvo null / Bllx / RulesType rỗng trước đây NRE hoặc lỗi
            // SQL thô "Cannot insert NULL" (Bllx/RulesType NOT NULL).
            if (entityvo == null)
            {
                throw new FailedException("Dữ liệu quy tắc không được để trống.");
            }
            if (string.IsNullOrWhiteSpace(entityvo.Bllx))
            {
                throw new FailedException("Mã loại bệnh án không được để trống.");
            }
            if (string.IsNullOrWhiteSpace(entityvo.RulesType))
            {
                throw new FailedException("Tên quy tắc không được để trống.");
            }
            string OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
            using (var db = new EmrTransaction().BeginTrans())
            {
                if (entityvo.czlx == "update")
                {
                    // G17: trước đây FindEntity chỉ theo Id (không filter org)
                    // rồi deref không null-check → NRE với Id bogus + sửa được
                    // quy tắc của org khác.
                    var entity = db.IQueryable<MrWritingRulesEntity>().Where(p => p.Id == entityvo.Id && p.OrganizeId == OrganizeId).Take(1).ToList().FirstOrDefault();
                    if (entity == null)
                    {
                        throw new FailedException("Không tìm thấy quy tắc.");
                    }
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
