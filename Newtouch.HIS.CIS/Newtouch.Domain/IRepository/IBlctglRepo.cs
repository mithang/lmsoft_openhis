
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.IRepository
{
    public interface IBlctglRepo : IRepositoryBase<BlctglEntity>
    {

        List<BlctglEntity> GetTreeList(string orgId, int qx, string UserCode, string parentId);

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(BlctglEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);

        BlctglEntity GetCitiaoByID(string orgId, string ID);

    }
}
