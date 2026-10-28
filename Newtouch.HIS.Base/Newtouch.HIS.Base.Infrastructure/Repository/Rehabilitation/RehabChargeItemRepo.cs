using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Tools;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class RehabChargeItemRepo : RepositoryBase<RehabChargeItemEntity>, IRehabChargeItemRepo
    {
        public RehabChargeItemRepo()
            
        {
        }
        
        /// <summary>
        /// Lưu
        /// </summary>
        public void SubmitForm(RehabChargeItemEntity entity, string sfxmId)
        {
            if (!string.IsNullOrEmpty(sfxmId))
            {
                //CodeKiểm tra trùng lặp
                if (this.IQueryable().Any(a => a.Code == entity.Code && a.sfxmId != sfxmId))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                RehabChargeItemEntity oldEntity = null;   //Trước khi thay đổiEntity
                oldEntity = this.FindEntity(sfxmId);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.sfxmId = sfxmId;
                // FIX-2B (mau B-CIS-03): nhu RehabChargeClassification — giu
                // CreatorCode/CreateTime goc khi form post khong co cac truong nay.
                if (oldEntity != null)
                {
                    if (string.IsNullOrWhiteSpace(entity.CreatorCode))
                        entity.CreatorCode = oldEntity.CreatorCode;
                    if (entity.CreateTime == default)
                        entity.CreateTime = oldEntity.CreateTime;
                }
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "kf_sfxm", oldEntity.sfxmId.ToString());
                }
            }
            else
            {
                //CodeKiểm tra trùng lặp
                if (this.IQueryable().Any(a => a.Code == entity.Code))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                entity.Create(true, System.Guid.NewGuid());
                this.Insert(entity);

            }

        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string sfxmId, string OrganizeId)
        {
            this.Delete(a => a.sfxmId == sfxmId && a.OrganizeId == OrganizeId);
        }


    }
}
