using Newtouch.Herp.Domain.DTO.InputDto;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// Duyệt phiếu nhập/xuất kho
    /// </summary>
    public interface IOutOrInStorageBillApprovalApp
    {
        /// <summary>
        /// Duyệt
        /// </summary>
        /// <param name="billApprovalDTO"></param>
        /// <returns>error message</returns>
        string Approval(BillApprovalDTO billApprovalDTO);
    }
}