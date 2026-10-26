using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo: chl
    /// Ngày: 2018-09-20 14:48
    /// Mô tả: Bảng kiểm soát quyền mẫu
    /// </summary>
    public class BlmbqxkzRepo : RepositoryBase<BlmbqxkzEntity>, IBlmbqxkzRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public BlmbqxkzRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(BlmbqxkzEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                if (dbEntity != null)
                {
                    dbEntity.ctrlLevel = entity.ctrlLevel;
                    dbEntity.ctrlLevelDesc = entity.ctrlLevelDesc;
                    dbEntity.dutyCode = entity.dutyCode;
                    dbEntity.dutyName = entity.dutyName;
                    dbEntity.zt = entity.zt;
                    dbEntity.Modify(keyValue);
                    this.Update(dbEntity);
                }
                else
                {
                    entity.Create(true);
                    this.Insert(entity);
                }
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
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