using System;
using System.Collections.Generic;

namespace Newtouch.Herp.Domain.DTO.InputDto
{
    /// <summary>
    /// Truy vấn lãi/lỗ điều chỉnh giá
    /// </summary>
    public class PriceAdjustmentProfitLossDTO
    {
        /// <summary>
        /// Từ khóa
        /// </summary>
        public string keyWord { get; set; }

        /// <summary>
        /// Thời gian bắt đầu
        /// </summary>
        public DateTime startTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc
        /// </summary>
        public DateTime endTime { get; set; }

        /// <summary>
        /// Truy vấn kho
        /// </summary>
        public string kfbm { get; set; }

        /// <summary>
        /// Tổ chức
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// Có hiển thị tồn kho bằng 0 không 1:Có 0:Không
        /// </summary>
        public string lkc { get; set; }

        /// <summary>
        /// Kho có thể chọn
        /// </summary>
        public List<string> kfList { get; set; }
    }
}
