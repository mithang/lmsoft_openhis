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
                //CodeKiểm tra trùng lặp
                if (this.IQueryable().Any(a => a.cwCode == entity.cwCode && a.cwId != cwId && a.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                SysWardBedEntity oldEntity = null;   //Trước khi thay đổiEntity
                oldEntity = this.FindEntity(cwId.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.cwId = cwId.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "xt_cw", oldEntity.cwId.ToString());
                }
            }
            else
            {
                //CodeKiểm tra trùng lặp
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
