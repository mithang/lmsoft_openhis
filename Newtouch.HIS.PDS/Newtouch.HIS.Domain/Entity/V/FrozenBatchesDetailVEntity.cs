using System;

namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// 被冻结批次信息
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
        /// 被冻结库存
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
