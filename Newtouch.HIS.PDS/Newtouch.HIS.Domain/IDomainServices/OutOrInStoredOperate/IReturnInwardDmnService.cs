using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// trả lại thuốc phát nội bộ
    /// </summary>
    public interface IReturnInwardDmnService
    {

        /// <summary>
        /// gửi trả lại thuốc phát nội bộ
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <returns></returns>
        string SubmitReturnInward(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx);
    }
}