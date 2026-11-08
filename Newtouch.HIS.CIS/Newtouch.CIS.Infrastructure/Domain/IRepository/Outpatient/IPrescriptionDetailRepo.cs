using System.Collections.Generic;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Đơn thuốc chi tiết
    /// </summary>
    public interface IPrescriptionDetailRepo : IRepositoryBase<PrescriptionDetailEntity>
    {

        /// <summary>
        /// Lấy Đơn thuốc chi tiết
        /// </summary>
        /// <param name="cfId"></param>
        /// <returns></returns>
        List<PrescriptionDetailEntity> SelectData(string cfId, bool djbz);
        /// <summary>
        /// Lấy sắc hộ Mục
        /// </summary>
        /// <param name="cfId"></param>
        /// <returns></returns>
        List<PrescriptionDetailEntity> SelectDataDjxm(string cfId);
    }
}
