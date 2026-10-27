using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 出入库单据
    /// </summary>
    public class KfCrkdjRepo : RepositoryBase<KfCrkdjEntity>, IKfCrkdjRepo
    {
        
        
        /// <summary>
        /// 打回申请 从待处理变成暂存，只针对外部入库
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
