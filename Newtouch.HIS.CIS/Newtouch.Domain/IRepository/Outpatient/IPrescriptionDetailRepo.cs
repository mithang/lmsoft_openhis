using System.Collections.Generic;
using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Chi tiết đơn thuốc
    /// </summary>
    public interface IPrescriptionDetailRepo : IRepositoryBase<PrescriptionDetailEntity>
    {

        /// <summary>
        /// Lấy chi tiết đơn thuốc
        /// </summary>
        /// <param name="cfId"></param>
        /// <returns></returns>
        List<PrescriptionDetailEntity> SelectData(string cfId, bool djbz);
        /// <summary>
        /// Lấy hạng mục sắc hộ
        /// </summary>
        /// <param name="cfId"></param>
        /// <returns></returns>
        List<PrescriptionDetailEntity> SelectDataDjxm(string cfId);
    }
}
