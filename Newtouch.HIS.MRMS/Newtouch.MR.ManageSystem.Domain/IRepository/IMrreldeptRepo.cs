using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.MR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2019-11-20 13:45
    /// Mô tả: Bảng quan hệ khoa bệnh án và khoa HIS
    /// </summary>
    public interface IMrreldeptRepo : IRepositoryBase<MrreldeptEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int SubmitForm(MrreldeptEntity entity,string keyValue);

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Save(MrreldeptEntity entity);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int DeleteForm(string keyValue);

    }
}