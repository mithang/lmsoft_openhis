using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Nhập kho từ ngoài
    /// </summary>
    public interface IInStorageDmnService
    {
        /// <summary>
        /// Gửi nhập kho từ ngoài
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <returns></returns>
        string SubmitInStorage(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx);

        /// <summary>
        /// Nhập kho từ ngoài duyệt đạt
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="kcxx">Thông tin tồn kho</param>
        /// <param name="auditor">Người duyệt</param>
        /// <returns></returns>
        string InStorageAdopt(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStockInfoEntity> kcxx, string auditor);

        /// <summary>
        /// Nhập kho từ ngoài Thu hồi duyệt
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <param name="auditor"></param>
        /// <returns></returns>
        string InStorageCancel(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx, string auditor);
    }
}