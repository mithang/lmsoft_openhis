using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysWardDmnService : IScopedDependency
    {
        /// <summary>
        /// Lấy dữ liệu hiệu lực
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<SysWardVEntity>> GetbqList(string orgId);
        /// <summary>
        /// Lấy khu bệnh gắn với khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ks"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>

        Task<List<SysDepartmentWardRelationVO>> GetWardbyDept(string orgId, string ks, string keyword);
    }
}
