using Newtouch.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tạo bởi:周珏琦
    /// Ngày:2019-01-30 10:23
    /// Mô tả:Bảng sắp xếp phẫu thuật
    /// </summary>
    public interface IInpatientOperationArrangementRepo : IRepositoryBase<InpatientOperationArrangementEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        bool SubmitForm(InpatientOperationArrangementEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);

    }
}