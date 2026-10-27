using Newtouch.Herp.Application.Implementation.BillApprovalProcess;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// Duyệt phiếu nhập/xuất kho (Core port, parity audit 2026-10-27).
    /// The original dispatched on bill type to Wbrk/Wbck/DirectDelivery/
    /// DeliveryOfReturn processes; the earlier port returned an explicit stub
    /// because those processes were not ported yet. They are ported now
    /// (BillApprovalProcess/*, ProcessorFun base), so this dispatches exactly
    /// like the original. Dependencies are constructor-injected (the original
    /// relied on binary field injection inside the process classes).
    /// </summary>
    public class OutOrInStorageBillApprovalApp : AppBase, IOutOrInStorageBillApprovalApp
    {
        private readonly IKfCrkdjRepo _kfCrkdjRepo;
        private readonly IKfCrkmxRepo _kfCrkmxRepo;
        private readonly IStorageManageDmnService _storageManageDmnService;

        public OutOrInStorageBillApprovalApp(IKfCrkdjRepo kfCrkdjRepo,
        IKfCrkmxRepo kfCrkmxRepo, IStorageManageDmnService storageManageDmnService)
        {
            _kfCrkdjRepo = kfCrkdjRepo;
            _kfCrkmxRepo = kfCrkmxRepo;
            _storageManageDmnService = storageManageDmnService;
        }

        /// <summary>
        /// Duyệt
        /// </summary>
        /// <param name="billApprovalDTO"></param>
        /// <returns></returns>
        public string Approval(BillApprovalDTO billApprovalDTO)
        {
            if (billApprovalDTO == null) return "ID phiếu không được để trống";
            IProcess process;
            billApprovalDTO.OrganizeId = OrganizeId;
            var result = new ActResult();
            switch (billApprovalDTO.djlx)
            {
            case (int)EnumOutOrInStorageBillType.Wbrk:
                process = new WbrkProcess(billApprovalDTO, _kfCrkdjRepo, _kfCrkmxRepo, _storageManageDmnService);
                result = process.Process();
                break;
            case (int)EnumOutOrInStorageBillType.Wbck:
                process = new WbckProcess(billApprovalDTO, _kfCrkdjRepo, _kfCrkmxRepo, _storageManageDmnService);
                result = process.Process();
                break;
            case (int)EnumOutOrInStorageBillType.Zjck:
                process = new DirectDeliveryProcess(billApprovalDTO, _kfCrkdjRepo, _kfCrkmxRepo, _storageManageDmnService);
                result = process.Process();
                break;
            case (int)EnumOutOrInStorageBillType.Nbth:
                process = new DeliveryOfReturnProcess(billApprovalDTO, _kfCrkdjRepo, _kfCrkmxRepo, _storageManageDmnService);
                result = process.Process();
                break;
            default:
                result.IsSucceed = false;
                result.ResultMsg = "Không tìm thấy quy trình duyệt phù hợp";
                break;
            }
            return result.ResultMsg;
        }
    }
}
