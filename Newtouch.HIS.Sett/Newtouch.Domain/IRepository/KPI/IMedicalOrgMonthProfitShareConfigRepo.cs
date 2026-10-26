using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Tạo：Trương Đông Lâm
    /// Ngày tháng:2018-01-29 15:52
    /// Mô tả:Cấu hình chia lợi nhuận cơ sở y tế
    /// </summary>
    public interface IMedicalOrgMonthProfitShareConfigRepo : IRepositoryBase<MedicalOrgMonthProfitShareConfigEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(MedicalOrgMonthProfitShareConfigEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);
    }
}