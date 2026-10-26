using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysUserStaffDutyDmnService : IScopedDependency
    {
        /// <summary>
        /// quan hệ liên kết vị trí nhân viên List
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        Task<List<SysStaffDutyComplexVEntity>> GetStaffDutyListByOrganizeId(string orgId, string staffId = null);
        /// <summary>
        /// truy vấn danh sách nhân viên
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<SysDutyStaffVO>> GetStaffByDutyCode(string orgId, string keyword = null);
    }

}
