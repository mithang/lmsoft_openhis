using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{

    public interface IInpatientDietBaseRepo : IRepositoryBase<InpatientDietBaseEntity>
    {
        /// <summary>
        /// Lưu bảng phiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thực thể đối tượng</param>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        void SubmitForm(InpatientDietBaseEntity entity, string keyValue);

        /// <summary>
        /// Xóa Dữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        void DeleteForm(string keyValue);

        IList<InpatientDietBaseEntity> GetGridList(Pagination pagination, string lb, string keyword, string orgId);

    }
}
