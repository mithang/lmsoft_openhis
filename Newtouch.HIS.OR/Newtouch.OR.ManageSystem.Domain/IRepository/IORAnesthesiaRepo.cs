using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;
using Newtouch.Core.Common;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// Tạo lập：chl
    /// Ngày tạo：2019-10-31 15:44
    /// Mô tả：Bảng từ điển gây mê
    /// </summary>
    public interface IORAnesthesiaRepo : IRepositoryBase<ORAnesthesiaEntity>
    {
        /// <summary>
        /// Lấy danh sách thực thể phân trang
        /// </summary>
        /// <param name="pagination">Phân trang</param>
        /// <param name="keyword">Từ khóa lọc</param>
        /// <returns></returns>
        IList<ORAnesthesiaEntity> GetPagintionList(Pagination pagination, string keyword,string organizeId);
        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        int SubmitForm(ORAnesthesiaEntity entity, string keyValue);
        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        int DeleteForm(string keyValue);
    }
}