using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.EMR.Domain.BusinessObjects;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Người tạo:hyj
    /// Ngày:2018-09-20 18:23
    /// Mô tả:Bảng chi tiết tùy chọn
    /// </summary>
    public interface IBabasyRepo : IRepositoryBase<BabasyEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
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
