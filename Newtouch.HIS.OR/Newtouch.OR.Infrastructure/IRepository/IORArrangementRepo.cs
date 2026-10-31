using Newtouch.OR.ManageSystem.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// Tạo bởi:chl
    /// Ngày:2019-11-06 10:22
    /// Mô tả:Hồ sơ xếp lịch phẫu thuật
    /// </summary>
    public interface IORArrangementRepo : IRepositoryBase<ORArrangementEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int SubmitForm(ORArrangementEntity entity, string ApplyId,string ArrangeId);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int DeleteForm(string keyValue);

        IList<ORArrangementEntity> GetPagintionListForRegistration(Pagination pagination, string keyword, string bq,string OrganizeId);
        int UpdateSqzt(string keyValue, string value);
    }
}