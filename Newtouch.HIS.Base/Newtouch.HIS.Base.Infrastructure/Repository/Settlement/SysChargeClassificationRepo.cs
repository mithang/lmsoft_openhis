using System.Collections.Generic;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;
using NewtouchHIS.Lib.Base.Exceptions;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Phân loại thu phí
    /// </summary>
    public class SysChargeClassificationRepo : RepositoryBase<SysChargeClassificationEntity>, ISysChargeClassificationRepo
    {
        public SysChargeClassificationRepo()
            
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysChargeClassificationEntity GetForm(int keyValue)
        {
            return this.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<SysChargeClassificationEntity> GetList(string keyValue, string orgId)
        {
            return this.IQueryable().Where(a => a.OrganizeId == orgId).OrderBy(a => a.px).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<SysChargeClassificationEntity> GetValidList(string orgId)
        {
            return this.IQueryable().Where(a => a.OrganizeId == orgId && a.zt == "1").OrderBy(a => a.px).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysChargeClassificationEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                //CodeKiểm tra trùng lặp
                if (this.IQueryable().Any(p => p.flCode == entity.flCode && p.flId != keyValue.Value && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                //CodeKiểm tra trùng lặp
                if (this.IQueryable().Any(p => p.flCode == entity.flCode && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                entity.Create();
                this.Insert(entity);
            }
        }
    }
}
