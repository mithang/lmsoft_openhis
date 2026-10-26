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
    public class SysMedicalOrderFrequencyRepo : RepositoryBase<SysMedicalOrderFrequencyEntity>, ISysMedicalOrderFrequencyRepo
    {
        public SysMedicalOrderFrequencyRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }


        /// <summary>
        /// Lưu
        /// </summary>
        public void SubmitForm(SysMedicalOrderFrequencyEntity entity, int? yzpcId)
        {
            if (yzpcId != null && yzpcId > 0)
            {
                //Kiểm tra trùng lặp Code
                if (this.IQueryable().Any(a => a.yzpcCode == entity.yzpcCode && a.OrganizeId == entity.OrganizeId && a.yzpcId != yzpcId))
                {
                    throw new FailedException("编码不可重复");
                }

                SysMedicalOrderFrequencyEntity oldEntity = null;   //Entity trước khi thay đổi
                oldEntity = this.FindEntity(yzpcId.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.yzpcId = yzpcId.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysMedicalOrderFrequencyEntity.GetTableName(), oldEntity.yzpcId.ToString());
                }
            }
            else
            {
                //Kiểm tra trùng lặp Code
                if (this.IQueryable().Any(a => a.yzpcCode == entity.yzpcCode && a.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("编码不可重复");
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
        /// Sửa đổi form
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
