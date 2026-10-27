using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Thông tin thuốc
    /// </summary>
    public interface ISysMedicineExDmnService
    {
        /// <summary>
        /// Truy vấn chi tiết thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        SysMedicineComplexVEntity GetYpDetails(string orgId, string ypCode);
    }
}