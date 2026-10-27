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
    public class SysMedicalOrderFrequencyRepo : RepositoryBase<SysMedicalOrderFrequencyEntity>, ISysMedicalOrderFrequencyRepo
    {
        public SysMedicalOrderFrequencyRepo()
            
        {
        }


        /// <summary>
        /// Lưu
        /// </summary>
        public void SubmitForm(SysMedicalOrderFrequencyEntity entity, int? yzpcId)
        {
            if (yzpcId != null && yzpcId > 0)
            {
                //CodeKiểm tra trùng lặp
                if (this.IQueryable().Any(a => a.yzpcCode == entity.yzpcCode && a.OrganizeId == entity.OrganizeId && a.yzpcId != yzpcId))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }

                SysMedicalOrderFrequencyEntity oldEntity = null;   //Trước khi thay đổiEntity
                oldEntity = this.FindEntity(yzpcId.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.yzpcId = yzpcId.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, "xt_yzpc", oldEntity.yzpcId.ToString());
                }
            }
            else
            {
                //CodeKiểm tra trùng lặp
                if (this.IQueryable().Any(a => a.yzpcCode == entity.yzpcCode && a.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                entity.Create();
                this.Insert(entity);

            }

        }

        /// <summary>
        /// Lấy tất cả danh sách
        /// </summary>
        public List<SysMedicalOrderFrequencyEntity> GetOrderFrequencyList(string orgId, string keyword = null)
        {
            List<SysMedicalOrderFrequencyEntity> list = null;
            if (!string.IsNullOrEmpty(keyword))
            {
                list = this.IQueryable().Where(a => a.OrganizeId == orgId &&(a.yzpcmc.Contains(keyword) || a.yzpcCode.Contains(keyword))).ToList();
            }
            else
            {
                list = this.IQueryable().Where(a => a.OrganizeId == orgId).ToList();
            }
            return list;
        }

        /// <summary>
        /// Sửaform
        /// </summary>
        public SysMedicalOrderFrequencyEntity GetOrderFrequencyEntity(int? yzpcId,string orgId)
        {
            var entity = this.IQueryable().Where(a => a.OrganizeId == orgId && a.yzpcId == yzpcId).FirstOrDefault();
            return entity;
        }

        /// <summary>
        /// Xóa
        /// </summary>
        public void DeleteForm(string orgId, int yzpcId)
        {
            this.Delete(a => a.yzpcId == yzpcId && a.OrganizeId == orgId);
        }



    }
}
