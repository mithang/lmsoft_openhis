using Newtouch.CIS.Compat;
using System.Collections.Generic;

using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

namespace Newtouch.Repository
{
    public class InpatientDietDetailSplitRepo : RepositoryBase<InpatientDietDetailSplitEntity>, IInpatientDietDetailSplitRepo
    {
        /// <summary>
        /// Mặc định Hàm khởi tạo số
        /// </summary>
        /// <param name="databaseFactory">EF trên dưới văn Nhà máy</param>
        public InpatientDietDetailSplitRepo()
            : base()
        {
            
        }

        /// <summary>
        /// Lưu bảng phiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thực thể đối tượng</param>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        public void SubmitForm(InpatientDietDetailSplitEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                var ignoreProps = new List<string>() { "Id", "OrganizeId", "CreateTime", "CreatorCode", "LastModifyTime", "LastModifierCode" };
                entity.MapperTo(dbEntity, ignoreProps);
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
        /// Xóa Dữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

    }
}