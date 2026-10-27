namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// SửaTồn khotham số
    /// </summary>
    public class UpdateKcslDTO
    {
        /// <summary>
        /// vật tưID
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
        /// Mục tiêuKho
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhấtSố lượng
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// tổ chứckếtcấu trúc
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// Thao tác người
        /// </summary>
        public string userCode { get; set; }
    }
}
