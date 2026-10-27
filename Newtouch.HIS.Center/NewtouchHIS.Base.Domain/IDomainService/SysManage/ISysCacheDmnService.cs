using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    /// <summary>
    /// Cache hệ thống
    /// </summary>
    public interface ISysCacheDmnService : ISingletonDependency
    {
        /// <summary>
        /// Cache thông tin danh tính người dùng
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task<BusResult<UserIdentity>> GetUserDataCache(string accessToken);
    }
}
