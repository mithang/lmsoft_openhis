namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// Sửa Tồn khotham số
    /// </summary>
    public class UpdateKcslDTO
    {
        /// <summary>
        /// vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Mục tiêu Kho
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất Số lượng
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// tổ chức kết cấu trúc
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// Thao tác người
        /// </summary>
        public string userCode { get; set; }
    }
}
