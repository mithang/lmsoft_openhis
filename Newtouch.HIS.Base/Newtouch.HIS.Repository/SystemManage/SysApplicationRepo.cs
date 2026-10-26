using Newtouch.Common;
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
    public class SysApplicationRepo : RepositoryBase<SysApplicationEntity>, ISysApplicationRepo
    {
        public SysApplicationRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Lấy danh sách (bao gồm cả các mục không hiệu lực)
        /// </summary>
        /// <returns></returns>
        public IList<SysApplicationEntity> GetList()
        {
            return this.IQueryable().ToList();
        }

        /// <summary>
        /// Lấy danh sách có hiệu lực
        /// </summary>
        /// <returns></returns>
        public IList<SysApplicationEntity> GetValidList()
        {
            return this.IQueryable().Where(p => p.zt == "1").ToList();
        }

        /// <summary>
        /// Gửi tạo mới, cập nhật thực thể
        /// </summary>
        /// <returns></returns>
        public void SubmitForm(SysApplicationEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                SysApplicationEntity oldEntity = null;   //Entity trước khi thay đổi
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysApplicationEntity.GetTableName(), oldEntity.Id);
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
