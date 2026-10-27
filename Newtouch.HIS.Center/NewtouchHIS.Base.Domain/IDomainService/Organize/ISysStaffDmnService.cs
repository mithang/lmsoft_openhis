using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.ValueObjects.SysManage;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysStaffDmnService : IScopedDependency
    {
        Task<SysStaffEntity> FindKey(string userId);
        /// <summary>
        /// TheoUserIdLấy thông tin công nhân viên chức
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<SysStaffVEntity>> GetStaffListByUserId(string userId);
        /// <summary>
        /// Lấy thông tin khoa của bác sĩ theo mã nhân viên
        /// </summary>
        /// <param name="rygh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysStaffDeptVO>> GetStaffDeptByGh(string rygh, string orgId);
    }
}
