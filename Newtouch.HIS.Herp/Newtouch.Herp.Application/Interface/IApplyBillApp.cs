using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// Quản lý phiếu lĩnh
    /// </summary>
    public interface IApplyBillApp
    {
        /// <summary>
        /// Gửi phiếu lĩnh khoa
        /// </summary>
        /// <param name="sld"></param>
        /// <param name="sldmx"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string SubmitDepartmentApply(KfApplyOrderEntity sld, List<KfApplyOrderDetailEntity> sldmx, string organizeId);

        /// <summary>
        /// Gửi lĩnh hàng giao
        /// </summary>
        /// <param name="ckmx"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string SubmitApplyOutStock(List<VApplyBillDetailEntity> ckmx, string organizeId, string userCode);
    }
}