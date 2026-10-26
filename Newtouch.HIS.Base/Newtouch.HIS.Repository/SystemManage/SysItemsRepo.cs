using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
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
        public SysItemsRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Lấy phân loại từ điển (bao gồm cả các mục không hiệu lực)
        /// </summary>
        /// <returns></returns>
        public IList<SysItemsEntity> GetList()
        {
            var query = this.IQueryable();
            return query.OrderByDescending(t => t.CreateTime).ToList();
        }

        /// <summary>
        /// Lấy phân loại từ điển có hiệu lực
        /// </summary>
        /// <returns></returns>
        public IList<SysItemsEntity> GetValidList()
        {
            return this.IQueryable().Where(p => p.zt == "1").OrderByDescending(p => p.CreateTime).ToList();
        }

        /// <summary>
        /// Gửi tạo mới, cập nhật thực thể
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysItemsEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.Code == entity.Code && p.Id != keyValue))
                {
                    throw new FailedException("编码不可重复");
                }

                SysItemsEntity oldEntity = null;   //Entity trước khi thay đổi
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysItemsEntity.GetTableName(), oldEntity.Id);
                }
            }
            else
            {
                if (this.IQueryable().Any(p => p.Code == entity.Code))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Create(true);
                this.Insert(entity);
            }
        }

    }
}


