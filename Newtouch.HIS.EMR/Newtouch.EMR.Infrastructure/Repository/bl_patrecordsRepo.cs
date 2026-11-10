using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using System.Linq;
using NewtouchHIS.Lib.Base.Exceptions;
using System;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo:chl
    /// Ngày:2018-08-30 11:31
    /// Mô tả:Thông tin bệnh nhân nội trú
    /// </summary>
    public class bl_patrecordsRepo : RepositoryBase<bl_patrecordsEntity>, Ibl_patrecordsRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh EF</param>



        public bl_patrecordsEntity GetpatrecordsByID(string ID)
        {

            return this.FindEntity(p => p.Id == ID);
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(bl_patrecordsEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                // G17: keyValue không tồn tại trước đây NRE ở dòng dưới.
                if (dbEntity == null) throw new FailedException("Không tìm thấy bệnh án.");
                //properties
                dbEntity.IsLock = entity.IsLock;
                dbEntity.blxtmc_hj = entity.blxtmc_hj;
                // G19 (carry-fix PROBE-G18 §9): nhánh edit không copy
                // blxtmc_yj nên mọi lần sửa đều giữ tên cũ (hj thì đã copy
                // từ G18). Chỉ copy khi có giá trị để không wipe về NULL.
                if (!string.IsNullOrWhiteSpace(entity.blxtmc_yj))
                {
                    dbEntity.blxtmc_yj = entity.blxtmc_yj;
                }
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
