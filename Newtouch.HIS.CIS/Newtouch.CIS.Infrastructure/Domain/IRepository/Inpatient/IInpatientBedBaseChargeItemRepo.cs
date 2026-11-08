using Newtouch.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository
{
    /// </summary>
    public interface IInpatientBedLevelChargeItemRepo : IRepositoryBase<InpatientBedLevelChargeItemEntity>
    {
        /// <summary>
        /// Lưu bảng phiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thực thể đối tượng</param>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        void SubmitForm(InpatientBedLevelChargeItemEntity entity, string keyValue);

        /// <summary>
        /// Xóa Dữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        void DeleteForm(string cwCode, string orgId);

    }
}
