
using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// cần kết chuyển của thông tin thuốc
    /// </summary>
    public class NeedCarryOverMedicineVO
    {
        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// kho thuốc số lượng
        /// </summary>
        public int? kcsl { get; set; }

        /// <summary>
        /// giá bán buôn kho thuốc
        /// </summary>
        public Decimal? ykpfj { get; set; }

        /// <summary>
        /// giá bán lẻ kho thuốc
        /// </summary>
        public decimal yklsj { get; set; }

        /// <summary>
        /// kho thuốc giá nhập
        /// </summary>
        public decimal? jj { get; set; }

        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }
    }
}
