using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.Tools;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysWardBedRepo : RepositoryBase<SysWardBedEntity>, ISysWardBedRepo
    {
        public SysWardBedRepo()
            
        {
        }

        /// <summary>
        /// Lưu
        /// </summary>
        public void SubmitForm(SysWardBedEntity entity, int? cwId)
        {
            if (cwId != null && cwId > 0)
            {
                //Code Kiểm tra trùng lặp
                if (this.IQueryable().Any(a => a.cwCode == entity.cwCode && a.cwId != cwId && a.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                SysWardBedEntity oldEntity = null;   //Trước khi thay đổi Entity
                oldEntity = this.FindEntity(cwId.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.cwId = cwId.Value;
                // F16-FIX D1: form khong post CreatorCode/CreateTime nen entity mang
                // null/default; this.Update(entity) 1 tham so bind vao overload generic
                // RepositoryBase.Update<T> (khong ignore) thay vi overload virtual co
                // ignore CreatorCode/CreateTime (RepositoryBase.cs:123 vs :331) -> UPDATE
                // gan NULL vao cot CreatorCode NOT NULL. Khoi phuc 2 cot tu dong dang
                // luu (oldEntity da nap san cho log thay doi) truoc khi update.
                if (oldEntity != null)
                {
                    entity.CreatorCode = oldEntity.CreatorCode;
                    entity.CreateTime = oldEntity.CreateTime;
                }
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "xt_cw", oldEntity.cwId.ToString());
                }
            }
            else
            {
                //Code Kiểm tra trùng lặp
                if (this.IQueryable().Any(a => a.cwCode == entity.cwCode && a.OrganizeId == entity.OrganizeId))
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
        public void DeleteForm(int cwId, string orgId)
        {
            this.Delete(a => a.cwId == cwId && a.OrganizeId == orgId);
        }

        /// <summary>
        /// Theo tình hình sử dụng giường
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="sfzy"></param>
        public void UpdateOccupyByCode(string code, string orgId, bool sfzy)
        {
            var entity = this.IQueryable(p => p.cwCode == code && p.OrganizeId == orgId && p.zt == "1").FirstOrDefault();
            if (entity != null)
            {
                entity.sfzy = sfzy;
                entity.Modify();
                this.Update(entity);
            }
        }

    }
}
