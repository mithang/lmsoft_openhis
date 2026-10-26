using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;
using Newtouch.EMR.Domain.ValueObjects;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Người tạo: hyj
    /// Ngày: 2018-09-07 15:46
    /// Mô tả: Quản lý từ mục
    /// </summary>
    public interface IBlctglRepo : IRepositoryBase<BlctglEntity>
    {

        List<BlctglEntity> GetTreeList(string orgId, int qx, string UserCode, string parentId);

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi)
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

		List<BLYXFHVO> Getyxfhlist(int ls);

	}
}