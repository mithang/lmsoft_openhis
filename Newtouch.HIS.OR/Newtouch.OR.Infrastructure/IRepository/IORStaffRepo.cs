using Newtouch.OR.ManageSystem.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// Tạo bởi：chl
    /// Ngày：2019-11-06 10:22
    /// Mô tả：Nhân viên phẫu thuật
    /// </summary>
    public interface IORStaffRepo : IRepositoryBase<ORStaffEntity>
    {
        /// <summary>
        /// Lấy danh sách thực thể phân trang
        /// </summary>
        /// <param name="pagination">Phân trang</param>
        /// <param name="keyword">Từ khóa lọc</param>
        /// <returns></returns>
        IList<ORStaffEntity> GetPagintionList(Pagination pagination, string keyword,string organizeId);
        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int SubmitForm(ORStaffEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int DeleteForm(string keyValue);

		/// <summary>
		/// Lấy danh sách nhân viên khung nổi
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		IList<ORStaffEntity> GetFloatStaffList(string organizeId, string keyword);


	}
}