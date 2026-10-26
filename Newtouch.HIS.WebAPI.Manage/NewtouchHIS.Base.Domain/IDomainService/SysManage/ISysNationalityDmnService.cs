using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.ValueObjects.SysManage;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    public interface ISysNationalityDmnService : IScopedDependency
    {
        /// <summary>
        /// lấy quốc tịch hiệu lực
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<SysNationalityVEntity>> GetgjList();
    }
}
