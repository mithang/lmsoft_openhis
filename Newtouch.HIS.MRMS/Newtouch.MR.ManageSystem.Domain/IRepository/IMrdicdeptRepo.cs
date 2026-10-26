using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;
using Newtouch.Core.Common;

namespace Newtouch.MR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2019-11-20 13:45
    /// Mô tả: Khoa bệnh án
    /// </summary>
    public interface IMrdicdeptRepo : IRepositoryBase<MrdicdeptEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int SubmitForm(MrdicdeptEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int DeleteForm(string keyValue);

        /// <summary>
        /// LấyDanh sách bệnh án
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<MrdicdeptEntity> GetDicDeptList(string organizeId);

        /// <summary>
        /// Lấy danh sách bệnh án theo trang
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<MrdicdeptEntity> GetPagintionDicDeptList(Pagination pagination, string organizeId, string keyword);

    }
}