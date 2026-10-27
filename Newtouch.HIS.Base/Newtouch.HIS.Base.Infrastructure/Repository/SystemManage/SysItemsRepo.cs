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
        /// Lấy phân loại từ điển（Bao gồm cả vô hiệu）
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
        /// Gửi tạo mới、Cập nhật Thực thể
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

                SysItemsEntity oldEntity = null;   //Trước khi thay đổiEntity
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
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


