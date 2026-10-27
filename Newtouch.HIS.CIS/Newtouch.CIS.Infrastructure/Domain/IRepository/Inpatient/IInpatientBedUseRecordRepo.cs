using Newtouch.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày：2018-06-28 13:27
    /// Mô tả：Nhập việngiường ký tựSử dụngghi chépkho
    /// </summary>
    public interface IInpatientBedUseRecordRepo : IRepositoryBase<InpatientBedUseRecordEntity>
    {
        /// <summary>
        /// Lưubảngphiếu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void SubmitForm(InpatientBedUseRecordEntity entity, string keyValue);

        /// <summary>
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void DeleteForm(string keyValue);

    }
}
