using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Tạo：张冬林
    /// Ngày tháng:2017-09-30 14:21
    /// Mô tả:Cấu hình nhắc lỗi
    /// </summary>
    public interface ISysFailedCodeMessageMappRepo : IRepositoryBase<SysFailedCodeMessageMappEntity>
    {
        /// <summary>
        /// Lấy danh sách thực thể phân trang
        /// </summary>
        /// <param name="pagination">Phân trang</param>
        /// <param name="keyword">Từ khóa lọc</param>
        IList<SysFailedCodeMessageMappEntity> GetPagintionList(Pagination pagination, string keyword);

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(SysFailedCodeMessageMappEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);

        /// <summary>
        /// Lấy tổ chứcFailMsgDanh sách cấu hình
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysFailedCodeMessageMappEntity> GetListByOrgId(string orgId = null);

    }
}