using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysFailedCodeMessageMappDmnService : IScopedDependency
    {
        /// <summary>
        /// Lấy nội dung hiệu lực
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<SysFailedCodeMessageMappEntity>> GetList(string orgId = null);
    }
}
