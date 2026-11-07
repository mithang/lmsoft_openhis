using Newtouch.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày: 2018-06-28 13:27
    /// Mô tả: Nhập viện Y lệnh gói
    /// </summary>
    public interface IInpatientOrderPackageRepo : IRepositoryBase<InpatientOrderPackageEntity>
    {
        /// <summary>
        /// Lưu bảng phiếu (Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">thực thể đối tượng</param>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        void SubmitForm(InpatientOrderPackageEntity entity, string keyValue);

        /// <summary>
        /// Xóa Dữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        void DeleteForm(string keyValue);


        List<InpatientOrderPackageEntity> GettcTree(int tcfw, string yzlx, string orgId);
    }
}
