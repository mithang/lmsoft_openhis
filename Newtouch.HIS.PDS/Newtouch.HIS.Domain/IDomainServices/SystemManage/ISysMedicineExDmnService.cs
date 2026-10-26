using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// thông tin thuốc
    /// </summary>
    public interface ISysMedicineExDmnService
    {
        /// <summary>
        /// tra cứu thuốc chi tiết
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        SysMedicineComplexVEntity GetYpDetails(string orgId, string ypCode);
    }
}