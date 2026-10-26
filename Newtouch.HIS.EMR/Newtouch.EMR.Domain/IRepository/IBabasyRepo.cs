using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;
using Newtouch.EMR.Domain.BusinessObjects;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Người tạo: hyj
    /// Ngày: 2018-09-20 18:23
    /// Mô tả: Bảng chi tiết lựa chọn
    /// </summary>
    public interface IBabasyRepo : IRepositoryBase<BabasyEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(BabasyVO entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);

    }
}