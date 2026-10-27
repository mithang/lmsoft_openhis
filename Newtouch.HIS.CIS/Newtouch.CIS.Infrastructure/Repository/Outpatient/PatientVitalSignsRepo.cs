
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System;
using Newtouch.CIS.Compat;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class PatientvitalSignsRepo : RepositoryBase<PatientVitalSignsEntity>, IPatientVitalSignsRepo
    {
        public PatientvitalSignsRepo()
            : base()
        {
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(PatientVitalSignsEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                PatientVitalSignsEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.Create(true);
                this.Insert(entity);
            }
        }
    }
}
