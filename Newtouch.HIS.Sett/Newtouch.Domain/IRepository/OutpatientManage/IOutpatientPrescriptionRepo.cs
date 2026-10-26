
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Bảng chính đơn thuốc
    /// </summary>
    public interface IOutpatientPrescriptionRepo : IRepositoryBase<OutpatientPrescriptionEntity>
    {
        /// <summary>
        /// TheocfhLấy Thực thể đơn thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfh"></param>
        /// <returns></returns>
        OutpatientPrescriptionEntity GetValidEntityByCfh(string orgId, string cfh);

        /// <summary>
        /// TheocfnmLấy Thực thể đơn thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfnm"></param>
        /// <returns></returns>
        OutpatientPrescriptionEntity GetValidEntityByCfnm(string orgId, int cfnm);

        /// <summary>
        /// Tạo số đơn thuốc mới
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="type">Loại đơn thuốc1Thuốc 2Không phải thuốc</param>
        /// <returns></returns>
        string GeneratePresNo(string orgId, int type);

    }
}
