
using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thông tin thuốc cần kết chuyển
    /// </summary>
    public class NeedCarryOverMedicineVO
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hạn dùng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Số lượng kho thuốc
        /// </summary>
        public int? kcsl { get; set; }

        /// <summary>
        /// Giá bán buôn kho thuốc
        /// </summary>
        public Decimal? ykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc
        /// </summary>
        public decimal yklsj { get; set; }

        /// <summary>
        /// Giá nhập kho thuốc
        /// </summary>
        public decimal? jj { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }
    }
}
