using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// khoa phát thuốc
    /// </summary>
    public interface IDeliveryToDepartmentDmnService
    {
        /// <summary>
        /// gửi khoa phát thuốc
        /// </summary>
        /// <returns></returns>
        string SubmitDeliveryToDepartment(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx);
    }
}