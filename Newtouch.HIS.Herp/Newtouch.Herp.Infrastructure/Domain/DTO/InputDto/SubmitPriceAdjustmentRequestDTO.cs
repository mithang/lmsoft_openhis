using System;

namespace Newtouch.Herp.Domain.DTO.InputDto
{
    /// <summary>
    /// Gửi điều chỉnh giá
    /// </summary>
    public class SubmitPriceAdjustmentRequestDTO
    {
        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Giá bán lẻ tương ứng với hệ số chuyển đổi,thường là giá đơn vị khoa phòng
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// Giá bán lẻ gốc tương ứng với hệ số chuyển đổi,thường là giá đơn vị khoa phòng
        /// </summary>
        public decimal ylsj { get; set; }

        /// <summary>
        /// Đơn vị với zhyz tương ứng,thường là đơn vị khoa phòng
        /// </summary>
        public string dwmc { get; set; }

        /// <summary>
        /// Tệp điều chỉnh
        /// </summary>
        public string tzwj { get; set; }

        /// <summary>
        /// Thời gian thực thi
        /// </summary>
        public DateTime zxsj { get; set; }
    }
}
