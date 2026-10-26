using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;
using System;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo: hyj
    /// Ngày: 2018-09-19 19:07
    /// Mô tả: Trang đầu hồ sơ bệnh án
    /// </summary>
    public class BlbasyRepo : RepositoryBase<BlbasyEntity>, IBlbasyRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public BlbasyRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(BlbasyEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties             
                dbEntity.blxtmc_yj = entity.blxtmc_yj;
                dbEntity.blxtmc_hj = entity.blxtmc_hj;
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.LastModifyTime = DateTime.Now;
                dbEntity.IsLock = entity.IsLock;
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                entity.CreateTime = DateTime.Now;
                entity.zt = "1";
                entity.Create(true);
                this.Insert(entity);
            }
        }
        public BlbasyEntity bl_basyGetByID(string ID)
        {

            return this.FindEntity(p => p.Id == ID);
        }
        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

    }
}