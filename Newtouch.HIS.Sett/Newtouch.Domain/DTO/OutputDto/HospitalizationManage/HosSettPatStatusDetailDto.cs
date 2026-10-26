using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// Quyết toán nội trú Bệnh nhân Chi tiết trạng thái hiện tại(Thông tin cơ bản bệnh nhân,Tính phí hạng mục,Tính phí thuốc,Thu chi tài khoản)
    /// </summary>
    public class HosSettPatStatusDetailDto
    {
        /// <summary>
        /// Thông tin cơ bản bệnh nhân nội trú
        /// </summary>
        public HospSettPatInfoVO HospSettPatInfo { get; set; }

        /// <summary>
        /// Tính phí loại lớnList
        /// </summary>
        public IList<HospFeeClassifyWithDLBO> DLFeeList { get; set; }

        /// <summary>
        /// Lịch sử nộp tiền(Bản ghi thu chi tài khoản hệ thống)
        /// </summary>
        public IList<HosPatAccPayVO> ChargeList { get; set; }
        
        /// <summary>
        /// Tiền tạm ứng tài khoản
        /// </summary>
        public decimal zhyjj { get; set; }

        /// <summary>
        /// Số tiền quyết toán(Chưa trừ phần giảm miễn)
        /// </summary>
        public decimal jsje { get; set; }


    }

}
