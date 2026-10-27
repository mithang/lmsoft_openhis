using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Người tạo：hyj
    /// Ngày：2018-09-19 19:07
    /// Mô tả：Trang đầu bệnh án
    /// </summary>
    public interface IBlbasyRepo : IRepositoryBase<BlbasyEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(BlbasyEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);
        BlbasyEntity bl_basyGetByID(string ID);

    }
}
