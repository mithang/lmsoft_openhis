using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using System.Text;
using System;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo:hyj
    /// Ngày:2018-09-19 19:07
    /// Mô tả:Trang đầu bệnh án
    /// </summary>
    public class BlbasyRepo : RepositoryBase<BlbasyEntity>, IBlbasyRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh EF</param>


        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
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
                this.Update(dbEntity);
            }
            else
            {
                entity.CreateTime = DateTime.Now;
                entity.zt = "1";
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
