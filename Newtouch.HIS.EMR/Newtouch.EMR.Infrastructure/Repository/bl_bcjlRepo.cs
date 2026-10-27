using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using System.Linq;
using NewtouchHIS.Lib.Base.Exceptions;
using System;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2018-08-30 11:31
    /// Mô tả：Thông tin bệnh nhân nội trú
    /// </summary>
    public class bl_bcjlRepo : RepositoryBase<bl_bcjlEntity>, Ibl_bcjlRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh EF</param>



        public bl_bcjlEntity bl_bcjlGetByID(string ID)
        {

            return this.FindEntity(p => p.Id == ID);
        }

        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(bl_bcjlEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.IsLock = entity.IsLock;
                dbEntity.blxtmc_hj = entity.blxtmc_hj;
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.LastModifyTime= DateTime.Now;
                this.Update(dbEntity);
            }
            else
            {
                entity.CreateTime = DateTime.Now;
                entity.zt = "1";
                entity.IsLock = 0;
                this.Insert(entity);
            }
        }
    }
}
