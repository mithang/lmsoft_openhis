using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysPatBasicInfoAppDmnService : IScopedDependency
    {
        /// <summary>
        /// Lấy danh sách tính chất bệnh nhân
        /// </summary>
        /// <returns></returns>
        Task<List<SysPatientNatureEntity>> GetBRXZListAsync(string orgId);
    }
}
