using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// V_S_xt_bq
    /// </summary>
    public interface ISysWardRepo : IRepositoryBase<SysWardVEntity>
    {
        /// <summary>
        /// lấy một cái khu bệnh tập hợp
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysWardVEntity> GetListByOrgId(string orgId);
    }
}
