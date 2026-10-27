using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Người tạo：hyj
    /// Ngày：2018-10-12 17:43
    /// Mô tả：Bảng phần tử
    /// </summary>
    public interface IBlysRepo : IRepositoryBase<BlysEntity>
    {
        List<BlysEntity> GetYsTree(string orgId, string yssjid);
        List<BlysEntity> GetYsTreeV2(string orgId, string keyword);
        List<BlysMXEntity> GetYsMX(string orgId, string YsId);
        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
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
