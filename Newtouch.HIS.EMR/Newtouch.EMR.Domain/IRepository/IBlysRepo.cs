using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Người tạo: hyj
    /// Ngày: 2018-10-12 17:43
    /// Mô tả: Bảng phần tử
    /// </summary>
    public interface IBlysRepo : IRepositoryBase<BlysEntity>
    {
        List<BlysEntity> GetYsTree(string orgId, string yssjid);
        List<BlysEntity> GetYsTreeV2(string orgId, string keyword);
        List<BlysMXEntity> GetYsMX(string orgId, string YsId);
        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(BlysEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);

    }
}