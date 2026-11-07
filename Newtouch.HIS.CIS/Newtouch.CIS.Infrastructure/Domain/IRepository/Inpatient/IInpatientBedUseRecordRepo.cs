using Newtouch.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày: 2018-06-28 13:27
    /// Mô tả: Nhập viện giường ký tự Sử dụng ghi chép kho
    /// </summary>
    public interface IInpatientBedUseRecordRepo : IRepositoryBase<InpatientBedUseRecordEntity>
    {
        /// <summary>
        /// Lưu bảng phiếu (Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">thực thể đối tượng</param>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        void SubmitForm(InpatientBedUseRecordEntity entity, string keyValue);

        /// <summary>
        /// Xóa Dữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        void DeleteForm(string keyValue);

    }
}
