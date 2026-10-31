using Newtouch.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày:2018-06-28 13:27
    /// Mô tả:Nhập việnY lệnhgói
    /// </summary>
    public interface IInpatientOrderPackageRepo : IRepositoryBase<InpatientOrderPackageEntity>
    {
        /// <summary>
        /// Lưubảngphiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void SubmitForm(InpatientOrderPackageEntity entity, string keyValue);

        /// <summary>
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void DeleteForm(string keyValue);


        List<InpatientOrderPackageEntity> GettcTree(int tcfw, string yzlx, string orgId);
    }
}
