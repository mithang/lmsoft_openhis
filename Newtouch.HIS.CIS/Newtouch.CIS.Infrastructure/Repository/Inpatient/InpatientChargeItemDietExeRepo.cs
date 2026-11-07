using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

using System.Collections.Generic;
using Newtouch.CIS.Compat;
using NewtouchHIS.Lib.Base.Model;
using System.Data.SqlClient;
using System.Text;
using Newtouch.CIS.Repository;

namespace Newtouch.Repository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày:2018-06-28 13:27
    /// Mô tả: Nhập viện Chung ăn uống yêu cầu kho
    /// </summary>
    public class InpatientChargeItemDietExeRepo : RepositoryBase<InpatientChargeItemDietExeEntity>, IInpatientChargeItemDietExeRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Nhà máy ngữ cảnh EF</param>
        public InpatientChargeItemDietExeRepo()
            : base()
        {
            
        }

        /// <summary>
        /// Lưu bảng phiếu (Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">thực thể đối tượng</param>
        /// <param name="keyValue">Khóa chính giá trị</param>
        public void SubmitForm(InpatientChargeItemDietExeEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
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
        /// <param name="keyValue">Khóa chính giá trị</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

    }
}