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
    public class RehabChargeClassificationRepo : RepositoryBase<RehabChargeClassificationEntity>, IRehabChargeClassificationRepo
    {
        public RehabChargeClassificationRepo()
            
        {
        }

        /// <summary>
        /// Lấy tất cả danh sách
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="sfflId"></param>
        /// <returns></returns>
        public List<RehabChargeClassificationEntity> GetRehabChargeClassificationList(string OrganizeId, string keyword = null)
        {
            List<RehabChargeClassificationEntity> list = null;
            if (!string.IsNullOrEmpty(keyword))
            {
                list = this.IQueryable().Where(a => a.OrganizeId== OrganizeId &&( a.Name.Contains(keyword) || a.py.Contains(keyword) || a.Code.Contains(keyword))).ToList();
            }
            else
            {
                list = this.IQueryable().Where(a=>a.OrganizeId==OrganizeId).ToList();
            }
            return list;
        }

        /// <summary>
        /// Sửaform
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public RehabChargeClassificationEntity GetRehabChargeClassificationEntity(string sfflId, string OrganizeId)
        {
            var entity = this.IQueryable().Where(a => a.OrganizeId == OrganizeId && a.sfflId==sfflId).FirstOrDefault();
            return entity;
        }

        /// <summary>
        /// Lưu
        /// </summary>
        public void SubmitForm(RehabChargeClassificationEntity entity, string sfflId)
        {
            if (!string.IsNullOrEmpty(sfflId))
            {
                //CodeKiểm tra trùng lặp
                if (this.IQueryable().Any(a => a.Code == entity.Code && a.sfflId != sfflId))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                RehabChargeClassificationEntity oldEntity = null;   //Trước khi thay đổiEntity
                oldEntity = this.FindEntity(sfflId);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.sfflId = sfflId;
                // FIX-2B (mau B-CIS-03): form post khong co CreatorCode/CreateTime;
                // Update full-column se ghi NULL de len cot NOT NULL. Giu gia tri
                // goc khi phia post trong (oldEntity da duoc nap san o tren).
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
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "kf_sffl", oldEntity.sfflId.ToString());
                }
            }
            else
            {
                entity.Create(true, System.Guid.NewGuid());
                //CodeKiểm tra trùng lặp
                if (this.IQueryable().Any(a => a.Code == entity.Code))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                entity.Create();
                this.Insert(entity);

            }

        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string sfflId, string OrganizeId)
        {
            this.Delete(a => a.OrganizeId == OrganizeId && a.sfflId == sfflId);
        }

    }
}
