using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;
using Newtouch.Core.Common;
using System.Collections.Generic;
using Newtouch.OR.ManageSystem.Domain.DTO.OutputDto;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// Tạo lập：chl
    /// Ngày tạo：2019-10-31 14:15
    /// Mô tả：Bảng từ điển phẫu thuật
    /// </summary>
    public interface IOROperationRepo : IRepositoryBase<OROperationEntity>
    {
        /// <summary>
        /// Lấy danh sách thực thể phân trang
        /// </summary>
        /// <param name="pagination">Phân trang</param>
        /// <param name="keyword">Từ khóa lọc</param>
        IList<OROperationEntity> GetPagintionList(Pagination pagination, string keyword,string organizeId);

        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int SubmitForm(OROperationEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int DeleteForm(string keyValue);
        /// <summary>
        /// Lấy danh sách phẫu thuật
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<OROperationEntity> GetOperationList(string organizeId,string keyword);
		IList<SysOpListDto> OpList(string orgId, string keyword, bool type);

	}
}