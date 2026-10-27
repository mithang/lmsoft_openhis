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
        /// Gửi phiếu lĩnh của khoa phòng
        /// </summary>
        /// <param name="sld"></param>
        /// <param name="sldmx"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string SubmitDepartmentApply(KfApplyOrderEntity sld, List<KfApplyOrderDetailEntity> sldmx, string organizeId);

        /// <summary>
        /// Gửi phát hàng theo phiếu lĩnh
        /// </summary>
        /// <param name="ckmx"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string SubmitApplyOutStock(List<VApplyBillDetailEntity> ckmx, string organizeId, string userCode);
    }
}