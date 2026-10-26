using Newtouch.HIS.Domain.BusinessObjects;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Một số truy vấn sql không biết đặt ở đâu thì đặt tại đây
    /// </summary>
    public interface ICommonDmnService
    {
        /// <summary>
        /// Lấy số lượng bệnh nhân khám (ngoại trú và nội trú)
        /// </summary>
        VisitNumBO GetVisitNum(bool isAdministrator, string orgId = null, string topOrgId = null);
    }
}
