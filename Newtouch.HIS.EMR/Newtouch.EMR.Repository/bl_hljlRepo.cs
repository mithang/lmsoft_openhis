using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Linq;
using Newtouch.Core.Common.Exceptions;
using System;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo: chl
    /// Ngày: 2018-08-30 11:31
    /// Mô tả: Thông tin bệnh nhân nội trú
    /// </summary>
    public class bl_hljlRepo : RepositoryBase<bl_hljlEntity>, Ibl_hljlRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public bl_hljlRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }


        public bl_hljlEntity bl_hljlGetByID(string ID)
        {

            return this.FindEntity(p => p.Id == ID);
        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(bl_hljlEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.IsLock = entity.IsLock;
                dbEntity.blxtmc_hj = entity.blxtmc_hj;
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.LastModifyTime = DateTime.Now;
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                entity.CreateTime = DateTime.Now;
                entity.zt = "1";
                entity.IsLock = 0;
                entity.Create(true);
                this.Insert(entity);
            }
        }
    }
}