using System;

namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// Thông tin lô bị đóng băng
    /// </summary>
    public class FrozenBatchesDetailVEntity
    {
        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// giá nhập theo đơn vị kho thuốc
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// Tồn kho bị đóng băng
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// thất bại thông tin
        /// </summary>
        public string errorMsg { get; set; }
    }
}
