using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Tiền tạm ứng
    /// </summary>
    public interface IReserveDmnService
    {

        /// <summary>
        /// Lưu giao dịch tài khoản
        /// </summary>
        /// <param name="vo"></param>
        void PatAccDB(PatAccDataVO vo, SysAccountEntity zh, FinanceReceiptEntity cwsj, string type);

        /// <summary>
        /// Lấy quản lý nội trú》Quản lý tài khoản》Lấy thông tin bản ghi thu chi tài khoản
        /// </summary>
        /// <returns></returns>
        List<PatAccPayVO> GetAccPayInfo(int zh, string orgId, string zhxz);
    }
}
