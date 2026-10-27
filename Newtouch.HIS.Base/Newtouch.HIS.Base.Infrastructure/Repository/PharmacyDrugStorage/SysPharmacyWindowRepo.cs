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
    public class SysPharmacyWindowRepo : RepositoryBase<SysPharmacyWindowEntity>, ISysPharmacyWindowRepo
    {
        public SysPharmacyWindowRepo()
            
        {
        }

        public void submitForm(SysPharmacyWindowEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId 
                && p.yfckId != keyValue && p.yfckCode == entity.yfckCode
                ))
                {
                    throw new FailedException("Mã cửa sổ không được trùng lặp!");
                }

                SysPharmacyWindowEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.yfckId = keyValue.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "xt_yfck", oldEntity.yfckId.ToString());
                }
            }
            else
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId &&
                p.yfckCode == entity.yfckCode))
                {
                    throw new FailedException("Mã cửa sổ không được trùng lặp!");
                }
                entity.Create();
                this.Insert(entity);
            }
        }
    }
}
