using Newtouch.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày：2018-06-28 13:27
    /// Mô tả：Nhập việnChi phíchi tiếtkho
    /// </summary>
    public interface IInpatientFeeDetailRepo : IRepositoryBase<InpatientFeeDetailEntity>
    {
        /// <summary>
        /// Lưubảngphiếu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void SubmitForm(InpatientFeeDetailEntity entity, string keyValue);

        /// <summary>
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void DeleteForm(string keyValue);

        /// <summary>
        /// thông quaSố nhập viện,Y lệnhTính chấtLấyThu phíthông tin
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="zyh"></param>
        /// <param name="yzxz"></param>
        /// <returns></returns>
        List<InpatientFeeDetailEntity> GetListByZyhYzxz(string OrganizeId, string zyh, string yzxz);

    }
}
