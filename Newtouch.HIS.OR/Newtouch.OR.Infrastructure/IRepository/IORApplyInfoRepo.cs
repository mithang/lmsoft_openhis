using Newtouch.OR.ManageSystem.Domain.Entity;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// Tạo bởi：chl
    /// Ngày：2019-10-31 10:04
    /// Mô tả：Hồ sơ đơn đăng ký phẫu thuật
    /// </summary>
    public interface IORApplyInfoRepo : IRepositoryBase<ORApplyInfoEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int SubmitForm(ORApplyInfoEntity entity, string keyValue);

        

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Số đơn đăng ký</param>
        void DeleteForm(string keyValue, string orgId,string ssmcn);
        int UpdateSqzt(string keyValue, string sqzt);

    }
}