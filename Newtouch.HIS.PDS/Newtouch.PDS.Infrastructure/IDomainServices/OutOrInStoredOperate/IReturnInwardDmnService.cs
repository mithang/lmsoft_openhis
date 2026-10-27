using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Trả lại phát thuốc nội bộ
    /// </summary>
    public interface IReturnInwardDmnService
    {

        /// <summary>
        /// Gửi trả lại phát thuốc nội bộ
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mx"></param>
        /// <returns></returns>
        string SubmitReturnInward(SysMedicineStorageIOReceiptEntity dj, List<SysMedicineStorageIOReceiptDetailEntity> mx);
    }
}