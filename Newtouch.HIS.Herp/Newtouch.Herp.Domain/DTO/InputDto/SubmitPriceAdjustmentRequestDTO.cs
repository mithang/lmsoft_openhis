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
        /// Giá bán lẻ Tương ứng với hệ số chuyển đổi,Thông thường là giá đơn vị khoa phòng
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// Giá bán lẻ gốc Tương ứng với hệ số chuyển đổi,Thông thường là giá đơn vị khoa phòng
        /// </summary>
        public decimal ylsj { get; set; }

        /// <summary>
        /// Đơn vị Và zhyz Tương ứng,Thông thường là đơn vị khoa phòng
        /// </summary>
        public string dwmc { get; set; }

        /// <summary>
        /// Điều chỉnh tệp
        /// </summary>
        public string tzwj { get; set; }

        /// <summary>
        /// Thời gian thực hiện
        /// </summary>
        public DateTime zxsj { get; set; }
    }
}
