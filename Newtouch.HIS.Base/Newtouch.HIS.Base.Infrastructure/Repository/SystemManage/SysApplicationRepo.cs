using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysApplicationRepo : RepositoryBase<SysApplicationEntity>, ISysApplicationRepo
    {
        public SysApplicationRepo()
            
        {
        }

        /// <summary>
        /// Lấy danh sách(Bao gồm cả vô hiệu)
        /// </summary>
        /// <returns></returns>
        public IList<SysApplicationEntity> GetList()
        {
            return this.IQueryable().ToList();
        }

        /// <summary>
        /// Lấy danh sách hiệu lực
        /// </summary>
        /// <returns></returns>
        public IList<SysApplicationEntity> GetValidList()
        {
            return this.IQueryable().Where(p => p.zt == "1").ToList();
        }

        /// <summary>
        /// Gửi tạo mới,Cập nhật Thực thể
        /// </summary>
        /// <returns></returns>
        public void SubmitForm(SysApplicationEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                SysApplicationEntity oldEntity = null;   //Trước khi thay đổi Entity
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                // F19-FIX D1: form khong post CreatorCode/CreateTime nen entity mang
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
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "Sys_Application", oldEntity.Id);
                }
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// Trả về một thực thể
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public SysApplicationEntity GetEntity(string appId)
        {
            if (string.IsNullOrWhiteSpace(appId))
            {
                return null;
            }
            var entity = this.IQueryable().Where(a => a.Id == appId).FirstOrDefault();
            return entity;
        }

    }
}
