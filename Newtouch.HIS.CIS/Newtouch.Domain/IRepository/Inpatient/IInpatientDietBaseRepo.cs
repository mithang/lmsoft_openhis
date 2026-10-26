using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{

    public interface IInpatientDietBaseRepo : IRepositoryBase<InpatientDietBaseEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(InpatientDietBaseEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);

        IList<InpatientDietBaseEntity> GetGridList(Pagination pagination, string lb, string keyword, string orgId);

    }
}