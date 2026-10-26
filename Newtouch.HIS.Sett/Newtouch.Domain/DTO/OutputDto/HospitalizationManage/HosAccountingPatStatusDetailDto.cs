using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// 
    /// </summary>
    public class HosAccountingPatStatusDetailDto
    {
        /// <summary>
        /// Lần nội trú này đã Ghi sổ Kế hoạch
        /// </summary>
        public IList<HospAccountingPlanVO> jzjhList { get; set; }

        /// <summary>
        /// Thông tin cơ bản bệnh nhân nội trú
        /// </summary>
        public HospPatientInfoVO patInfo { get; set; }

        /// <summary>
        /// Ngày quyết toán giữa chừng
        /// </summary>
        public DateTime? zjrq { get; set; }

    }
}
