using Newtouch.HIS.Domain.BusinessObjects;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Một số không biết đặt ở đâusqlTìm kiếm，Đặt tại đây
    /// </summary>
    public interface ICommonDmnService
    {
        /// <summary>
        /// Lấy số lượng người khám（Ngoại trú và nội trú）
        /// </summary>
        VisitNumBO GetVisitNum(bool isAdministrator, string orgId = null, string topOrgId = null);
    }
}
