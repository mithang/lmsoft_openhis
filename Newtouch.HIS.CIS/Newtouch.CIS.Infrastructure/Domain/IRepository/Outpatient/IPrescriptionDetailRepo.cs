using System.Collections.Generic;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Đơn thuốcchi tiết
    /// </summary>
    public interface IPrescriptionDetailRepo : IRepositoryBase<PrescriptionDetailEntity>
    {

        /// <summary>
        /// LấyĐơn thuốcchi tiết
        /// </summary>
        /// <param name="cfId"></param>
        /// <returns></returns>
        List<PrescriptionDetailEntity> SelectData(string cfId, bool djbz);
        /// <summary>
        /// Lấysắc hộMục
        /// </summary>
        /// <param name="cfId"></param>
        /// <returns></returns>
        List<PrescriptionDetailEntity> SelectDataDjxm(string cfId);
    }
}
