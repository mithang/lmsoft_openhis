using Newtouch.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày：2018-06-28 13:27
    /// Mô tả：Nhập việnphát thuốcyêu cầukho
    /// </summary>
    public interface IInpatientMedicineGrantRepo : IRepositoryBase<InpatientMedicineGrantEntity>
    {
        /// <summary>
        /// Lưubảngphiếu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void SubmitForm(InpatientMedicineGrantEntity entity, string keyValue);

        /// <summary>
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void DeleteForm(string keyValue);

    }
}
