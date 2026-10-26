using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.MR.ManageSystem.Repository
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2019-11-20 11:05
    /// Mô tả: Bảng ghi phẫu thuật trang đầu bệnh án
    /// </summary>
    public class MrbasyssRepo : RepositoryBase<MrbasyssEntity>, IMrbasyssRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh</param>
        public MrbasyssRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int SubmitForm(MrbasyssEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                
                dbEntity.Modify(keyValue);
                return Update(dbEntity);
            }
            entity.Create(true);
            return Insert(entity);
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int DeleteForm(string keyValue)
        {
            return Delete(p => p.Id == keyValue);
        }
    }
}