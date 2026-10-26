using Newtouch.HIS.Domain.Entity;
using Newtouch.Infrastructure.Model;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// V_S_xt_yfbm
    /// </summary>
    public interface ISysPharmacyDepartmentRepo : IRepositoryBase<SysPharmacyDepartmentVEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetYjbmjbByCode(string code, string orgId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetMzzybzByCode(string code, string orgId);

        /// <summary>
        /// theo yfbmcode lấy bộ phận nhà thuốc thực thể
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        LoginUserCurrentYfbmModel GetUserYfbmByCode(string code, string orgId);
    }
}
