using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Tạo：张冬林
    /// Ngày tháng:2018-01-29 15:52
    /// Mô tả:Cấu hình hoa hồng lợi nhuận của kỹ thuật viên điều trị
    /// </summary>
    public interface ITherapeutistMonthProfitShareConfigRepo : IRepositoryBase<TherapeutistMonthProfitShareConfigEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(TherapeutistMonthProfitShareConfigEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);

    }
}