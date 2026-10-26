using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// nhập kho từ bên ngoài
    /// </summary>
    public interface IInStorageDmnService
    {
        /// <summary>
        /// gửi nhập kho từ bên ngoài
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <returns></returns>
        string SubmitInStorage(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx);

        /// <summary>
        /// nhập kho từ bên ngoài duyệt
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="kcxx">thông tin tồn kho</param>
        /// <param name="auditor">duyệt viên</param>
        /// <returns></returns>
        string InStorageAdopt(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStockInfoEntity> kcxx, string auditor);

        /// <summary>
        /// nhập kho từ bên ngoài thu hồi duyệt
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <param name="auditor"></param>
        /// <returns></returns>
        string InStorageCancel(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx, string auditor);
    }
}