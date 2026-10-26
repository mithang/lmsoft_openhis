using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.MR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2019-11-20 13:45
    /// Mô tả: Chẩn đoán bệnh lý
    /// </summary>
    public interface IMrdicblzdRepo : IRepositoryBase<MrdicblzdEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int SubmitForm(MrdicblzdEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int DeleteForm(string keyValue);

    }
}