
using Newtouch.CIS.Repository;
using Newtouch.CIS.Compat;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class InspectionCategoryRepo : RepositoryBase<InspectionCategoryEntity>, IInspectionCategoryRepo
    {
        public InspectionCategoryRepo()
            : base()
        {
        }

        /// <summary>
        /// Lấydanh sách
        /// </summary>
        /// <returns></returns>
        public List<InspectionCategoryEntity> GetListByOrg(string orgId)
        {
            return this.IQueryable().Where(p => p.OrganizeId == orgId).OrderByDescending(p => p.CreateTime).ToList();
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(InspectionCategoryEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.dlCode == entity.dlCode && p.Id != keyValue))
                {
                    throw new FailedException("Số hiệu không được trùng lặp");
                }
                
                // FIX-2 B-CIS-03 (cung mau AuxiliaryDictionary: edit bang posted
                // entity + full-column update): nap ban goc de giu CreatorCode/
                // CreateTime khi form post khong co cac truong nay.
                var oldEntity = this.FindEntity(keyValue);
                entity.Modify(keyValue);
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
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.dlCode == entity.dlCode))
                {
                    throw new FailedException("Số hiệu không được trùng lặp");
                }
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="orgId"></param>
        public void DeleteForm(string keyValue)
        {
            var entity = this.FindEntity(keyValue);
            this.Delete(entity);
        }

    }
}
