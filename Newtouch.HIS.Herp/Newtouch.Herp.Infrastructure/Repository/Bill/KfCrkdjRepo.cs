using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Chứng từ nhập/xuất kho
    /// </summary>
    public class KfCrkdjRepo : RepositoryBase<KfCrkdjEntity>, IKfCrkdjRepo
    {
        
        
        /// <summary>
        /// Trả lại đơn xin Từ chờ xử lý chuyển thành lưu tạm，Chỉ áp dụng cho nhập kho ngoài
        /// </summary>
        /// <param name="crkId"></param>
        /// <returns></returns>
        public string BackToTemporary(long crkId)
        {
            var entity = FindEntity(p => p.Id == crkId);
            if (entity == null) return "Không tìm thấy phiếu đã chỉ định";
            if (entity.auditState.Equals(((int)EnumAuditState.Waiting).ToString()))
            {
                entity.auditState = ((int)EnumAuditState.Temporary).ToString();
                entity.Modify();
                return Update(entity) > 0 ? "" : "Sửa trạng thái duyệt thất bại";
            }
            return "Thao tác này chỉ áp dụng cho phiếu đang chờ nhập kho";
        }
    }
}
