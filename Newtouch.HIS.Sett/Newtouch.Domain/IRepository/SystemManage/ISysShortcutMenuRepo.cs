using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Tạo：张冬林
    /// Ngày tháng:2017-10-16 11:07
    /// Mô tả:Menu nhanh
    /// </summary>
    public interface ISysShortcutMenuRepo : IRepositoryBase<SysShortcutMenuEntity>
    {
        /// <summary>
        /// Lấy danh sách thực thể
        /// </summary>
        /// <param name="keyword">Từ khóa lọc</param>
        IList<SysShortcutMenuEntity> GetList(string keyword);

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(SysShortcutMenuEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);

    }
}