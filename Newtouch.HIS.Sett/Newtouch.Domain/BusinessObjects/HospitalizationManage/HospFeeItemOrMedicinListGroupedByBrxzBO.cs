using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// Quyết toán xuất viện Hạng mục/Thuốc Tính phí Danh sách theo Tính chất bệnh nhân Quyết toán theo nhóm
    /// </summary>
    public class HospFeeItemOrMedicinListGroupedByBrxzBO
    {
        /// <summary>
        /// Tính chất bệnh nhân(Trong bảng tổ hợp)
        /// </summary>
        public SysPatientComprehensiveNatureEntity brxzzhEntity { get; set; }

        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public SysPatientNatureEntity brxzEntity { get; set; }

        /// <summary>
        /// Hạng mục Hạng mục tính phí List
        /// </summary>
        public IList<HospFeeItemOrMedicinDetailBO> FeeList { get; set; }

        /// <summary>
        /// Phần vượt mức ghi sổ(Cần chuyển sang tự chi trả)
        /// </summary>
        public decimal jzce { get; set; }

        /// <summary>
        /// Số tiền khởi điểm sau khi tính toán(Cần chuyển sang tự chi trả)
        /// </summary>
        public decimal qfje { get; set; }

    }
}
