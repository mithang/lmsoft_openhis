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
        /// Lấy Đơn thuốcchi tiết
        /// </summary>
        /// <param name="cfId"></param>
        /// <returns></returns>
        List<PrescriptionDetailEntity> SelectData(string cfId, bool djbz);
        /// <summary>
        /// Lấysắc hộ Mục
        /// </summary>
        /// <param name="cfId"></param>
        /// <returns></returns>
        List<PrescriptionDetailEntity> SelectDataDjxm(string cfId);
    }
}
