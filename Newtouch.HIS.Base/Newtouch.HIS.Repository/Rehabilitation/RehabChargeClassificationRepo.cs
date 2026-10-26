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
    public class RehabChargeClassificationRepo : RepositoryBase<RehabChargeClassificationEntity>, IRehabChargeClassificationRepo
    {
        public RehabChargeClassificationRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
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
        /// Sửa đổi form
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
                //Kiểm tra trùng lặp Code
                if (this.IQueryable().Any(a => a.Code == entity.Code && a.sfflId != sfflId))
                {
                    throw new FailedException("编码不可重复");
                }

                RehabChargeClassificationEntity oldEntity = null;   //Entity trước khi thay đổi
                oldEntity = this.FindEntity(sfflId);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.sfflId = sfflId;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, RehabChargeClassificationEntity.GetTableName(), oldEntity.sfflId.ToString());
                }
            }
            else
            {
                entity.Create(true, System.Guid.NewGuid());
                //Kiểm tra trùng lặp Code
                if (this.IQueryable().Any(a => a.Code == entity.Code))
                {
                    throw new FailedException("编码不可重复");
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
