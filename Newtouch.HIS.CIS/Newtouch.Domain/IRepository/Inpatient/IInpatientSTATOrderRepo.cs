using Newtouch.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tạo bởi:张冬林
    /// Ngày:2018-06-28 13:27
    /// Mô tả:Y lệnh tạm thời
    /// </summary>
    public interface IInpatientSTATOrderRepo : IRepositoryBase<InpatientSTATOrderEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(InpatientSTATOrderEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);

    }
}