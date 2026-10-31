using Newtouch.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tác giả: 周珏琦
    /// Ngày:2019-01-30 10:23
    /// Mô tả:Sắp xếp phẫu thuậtbảng
    /// </summary>
    public interface IInpatientOperationArrangementRepo : IRepositoryBase<InpatientOperationArrangementEntity>
    {
        /// <summary>
        /// Lưubảngphiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        bool SubmitForm(InpatientOperationArrangementEntity entity, string keyValue);

        /// <summary>
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void DeleteForm(string keyValue);

    }
}
