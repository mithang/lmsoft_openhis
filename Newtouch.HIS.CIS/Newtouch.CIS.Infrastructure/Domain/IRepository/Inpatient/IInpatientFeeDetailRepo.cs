using Newtouch.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày: 2018-06-28 13:27
    /// Mô tả: Nhập viện Chi phí chi tiết kho
    /// </summary>
    public interface IInpatientFeeDetailRepo : IRepositoryBase<InpatientFeeDetailEntity>
    {
        /// <summary>
        /// Lưu bảng phiếu (Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">thực thể đối tượng</param>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        void SubmitForm(InpatientFeeDetailEntity entity, string keyValue);

        /// <summary>
        /// Xóa Dữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        void DeleteForm(string keyValue);

        /// <summary>
        /// thông qua Số nhập viện, Y lệnh Tính chất Lấy Thu phí thông tin
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="zyh"></param>
        /// <param name="yzxz"></param>
        /// <returns></returns>
        List<InpatientFeeDetailEntity> GetListByZyhYzxz(string OrganizeId, string zyh, string yzxz);

    }
}
