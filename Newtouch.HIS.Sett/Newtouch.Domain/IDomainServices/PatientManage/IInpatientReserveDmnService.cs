using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.PatientManage
{
    public interface IInpatientReserveDmnService
    {
        /// <summary>
        /// Lưu giao dịch dữ liệu tài khoản nội trú
        /// </summary>
        /// <param name="vo"></param>
        void PatZyAccDBProc(PatZyAccDataVO vo, InpatientAccountEntity zh, FinanceReceiptEntity cwsj, string type);

        /// <summary>
        /// Lấy quản lý nội trú》Quản lý tài khoản》Lấy thông tin bản ghi thu chi tài khoản
        /// </summary>
        /// <returns></returns>
        List<InpatientPatAccPayVO> GetAccPayInfo(int zh, string orgId, string zhxz);
    }
}
