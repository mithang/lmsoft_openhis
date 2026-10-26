using Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thông tin chờ quyết toán đơn thuốc mzh+sfxmTính phí theo nhóm
    /// </summary>
    public class PresSettleInfoVO : ChargeRightDto
    {
        /// <summary>
        /// Số thẻ khám bệnh
        /// </summary>
        public string kh { get; set; }

    }
    /// <summary>
    /// Thông tin chờ quyết toán nội trú
    /// </summary>
    public class ZyPresSettleInfoVO : HospFeeChargeCategoryGroupDetailVO
    {
        /// <summary>
        /// Số thẻ khám bệnh
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

    }
}
