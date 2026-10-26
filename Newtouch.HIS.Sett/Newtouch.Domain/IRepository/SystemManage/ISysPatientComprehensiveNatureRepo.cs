using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPatientComprehensiveNatureRepo : IRepositoryBase<SysPatientComprehensiveNatureEntity>
    {
        /// <summary>
        /// Theo tính chất bệnh nhân chính Lấy Tất cả
        /// </summary>
        /// <param name="zbrxz"></param>
        /// <returns></returns>
        IList<SysPatientComprehensiveNatureEntity> GetListByZbrxz(string zbrxz, string orgId);

    }
}
