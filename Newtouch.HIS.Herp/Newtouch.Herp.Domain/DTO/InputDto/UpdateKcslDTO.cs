namespace Newtouch.Herp.Domain.DTO.InputDto
{
    /// <summary>
    /// Sửa tham số tồn kho
    /// </summary>
    public class UpdateKcslDTO
    {
        /// <summary>
        /// Vật tư ID
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
        /// Kho đích
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        /// Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Cơ cấu tổ chức
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// Người thao tác
        /// </summary>
        public string userCode { get; set; }
    }
}
