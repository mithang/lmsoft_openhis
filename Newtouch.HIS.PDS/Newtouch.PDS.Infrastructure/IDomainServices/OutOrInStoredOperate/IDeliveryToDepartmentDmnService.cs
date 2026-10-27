using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Phát thuốc cho khoa
    /// </summary>
    public interface IDeliveryToDepartmentDmnService
    {
        /// <summary>
        /// Gửi phát thuốc cho khoa
        /// </summary>
        /// <returns></returns>
        string SubmitDeliveryToDepartment(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx);
    }
}