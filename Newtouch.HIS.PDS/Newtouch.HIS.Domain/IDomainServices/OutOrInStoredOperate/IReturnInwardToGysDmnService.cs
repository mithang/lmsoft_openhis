using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// xuất kho ra bên ngoài
    /// </summary>
    public interface IReturnInwardToGysDmnService
    {
        /// <summary>
        /// gửi xuất kho ra bên ngoài
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <returns></returns>
        string SubmitReturnInwardToGys(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx);
    }
}