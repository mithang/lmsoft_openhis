using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// Tạo lập：chl
    /// Ngày tạo：2019-11-06 10:22
    /// Mô tả：Hồ sơ nhân viên phẫu thuật
    /// </summary>
    public interface IOROpStaffRecordRepo : IRepositoryBase<OROpStaffRecordEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int SubmitForm(OROpStaffRecordEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int DeleteForm(string keyValue);

        /// <summary>
        /// Lấy danh sách mã theo số đăng ký phẫu thuật
        /// </summary>
        /// <param name="ssxh"></param>
        /// <returns></returns>
        IList<OROpStaffRecordEntity> getIdBySsxh(string ssxh);

    }
}