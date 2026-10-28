using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class RehabChargeItemComparisonRepo : RepositoryBase<RehabChargeItemComparisonEntity>, IRehabChargeItemComparisonRepo
    {
        public RehabChargeItemComparisonRepo()
            
        {
        }

        /// <summary>
        /// Lưu
        /// </summary>
        public void SubmitForm(RehabChargeItemComparisonEntity entity, string sfxmdzId)
        {
            if (!string.IsNullOrEmpty(sfxmdzId))
            {
                // FIX-2B (mau B-CIS-03): nhu 2 repo Rehab con lai — nap ban goc
                // de giu CreatorCode/CreateTime khi form post khong co cac truong
                // nay, tranh Update full-column ghi NULL len cot NOT NULL.
                var oldEntity = this.FindEntity(sfxmdzId);
                this.DetacheEntity(oldEntity);
                entity.Modify(sfxmdzId);
                if (oldEntity != null)
                {
                    if (string.IsNullOrWhiteSpace(entity.CreatorCode))
                        entity.CreatorCode = oldEntity.CreatorCode;
                    if (entity.CreateTime == default)
                        entity.CreateTime = oldEntity.CreateTime;
                }
                this.Update(entity);
            }
            else
            {
                entity.Create(true, System.Guid.NewGuid());
                this.Insert(entity);
            }
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string sfxmdzId, string OrganizeId)
        {
            this.Delete(a => a.sfxmdzId == sfxmdzId && a.OrganizeId == OrganizeId);
        }


    }
}
