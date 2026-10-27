using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 出入库单审核 (Core port)
    /// </summary>
    public class OutOrInStorageBillApprovalApp : AppBase, IOutOrInStorageBillApprovalApp
    {
        private readonly IKfCrkdjRepo _kfCrkdjRepo;

        // Original relied on binary field injection and constructed the
        // BillApprovalProcess classes (Wbrk/Wbck/DirectDelivery/DeliveryOfReturn)
        // with `new`; those processes depend on IStorageManageDmnService
        // (StorageManage area, not ported in this copy), so approval cannot be
        // executed faithfully here yet. The bill is still validated from the
        // ported repo; the stock-moving step is an explicit stub.
        public OutOrInStorageBillApprovalApp(IKfCrkdjRepo kfCrkdjRepo)
        {
            _kfCrkdjRepo = kfCrkdjRepo;
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="billApprovalDTO"></param>
        /// <returns></returns>
        public string Approval(BillApprovalDTO billApprovalDTO)
        {
            if (billApprovalDTO == null) return "ID phiếu không được để trống";
            billApprovalDTO.OrganizeId = OrganizeId;
            var dj = _kfCrkdjRepo.FindEntity(p => p.Id == billApprovalDTO.djId
                && p.OrganizeId == billApprovalDTO.OrganizeId);
            if (dj == null) return "Không tìm thấy phiếu đã chỉ định";
            // STUB (explicit): Wbrk/Wbck/DirectDelivery/DeliveryOfReturn approval
            // processes move stock via IStorageManageDmnService, which belongs
            // to the StorageManage area and is not ported in this copy. No stock
            // or audit-state change is performed.
            return "Chức năng duyệt phiếu chưa khả dụng: quy trình duyệt (Wbrk/Wbck/DirectDelivery/DeliveryOfReturn) phụ thuộc module Quản lý kho (StorageManage) chưa được chuyển đổi.";
        }
    }
}
