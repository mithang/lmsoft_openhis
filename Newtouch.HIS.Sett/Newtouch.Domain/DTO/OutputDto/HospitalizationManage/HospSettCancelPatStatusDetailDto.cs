using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// Quyết toán nội trú Hủy thao tác Bệnh nhân Chi tiết trạng thái hiện tại(Thông tin cơ bản bệnh nhân,Tính phí hạng mục,Tính phí thuốc,Thu chi tài khoản)
    /// </summary>
    public class HospSettCancelPatStatusDetailDto
    {
        /// <summary>
        /// Thông tin cơ bản bệnh nhân nội trú
        /// </summary>
        public HospSettPatInfoVO SettPatInfo { get; set; }

        /// <summary>
        /// Lần cuối cùng(Chưa trả lại)Quyết toán
        /// </summary>
        public HospSettlementEntity LastUnCancelledSett { get; set; }

    }

}
