using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Domain.IDomainService
{
    public interface IOrgBaseDmnService : IScopedDependency
    {
        /// <summary>
        /// HIS truy vấn khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<HisDeptVO>> HisDeptQuery(string orgId, string? keyword);
        /// <summary>
        /// HIS truy vấn bác sĩ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<HisStaffVO>> HisDocQuery(string orgId, string? keyword);
    }
}
