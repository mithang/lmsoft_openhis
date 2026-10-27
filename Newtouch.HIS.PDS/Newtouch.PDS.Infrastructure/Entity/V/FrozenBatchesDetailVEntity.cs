using System;

namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// Thông tin lô bị đóng băng
    /// </summary>
    public class FrozenBatchesDetailVEntity
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Giá nhập đơn vị kho thuốc
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// Tồn kho bị đóng băng
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// Hạn dùng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Thông tin thất bại
        /// </summary>
        public string errorMsg { get; set; }
    }
}
