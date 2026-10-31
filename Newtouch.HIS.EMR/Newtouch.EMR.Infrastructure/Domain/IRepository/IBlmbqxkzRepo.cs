using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Người tạo:chl
    /// Ngày:2018-09-20 14:48
    /// Mô tả:Bảng kiểm soát quyền mẫu
    /// </summary>
    public interface IBlmbqxkzRepo : IRepositoryBase<BlmbqxkzEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(BlmbqxkzEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);

    }
}
