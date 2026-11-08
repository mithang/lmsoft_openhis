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
    public class SysItemsRepo : RepositoryBase<SysItemsEntity>, ISysItemsRepository
    {
        public SysItemsRepo()
            
        {

        }

        /// <summary>
        /// Lấy phân loại từ điển(Bao gồm cả vô hiệu)
        /// </summary>
        /// <returns></returns>
        public IList<SysItemsEntity> GetList()
        {
            var query = this.IQueryable();
            return query.OrderByDescending(t => t.CreateTime).ToList();
        }

        /// <summary>
        /// Lấy phân loại từ điển hiệu lực
        /// </summary>
        /// <returns></returns>
        public IList<SysItemsEntity> GetValidList()
        {
            return this.IQueryable().Where(p => p.zt == "1").OrderByDescending(p => p.CreateTime).ToList();
        }

        /// <summary>
        /// Gửi tạo mới,Cập nhật Thực thể
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysItemsEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.Id != keyValue))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                SysItemsEntity oldEntity = null;   //Trước khi thay đổi Entity
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
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "Sys_Items", oldEntity.Id);
                }
            }
            else
            {
                if (this.IQueryable().Any(p => p.Code == entity.Code))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                entity.Create(true);
                this.Insert(entity);
            }
        }

    }
}


