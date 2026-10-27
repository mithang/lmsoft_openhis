namespace Newtouch.Herp.Domain.DTO.InputDto
{
    /// <summary>
    /// Thông tin vật tư trong danh sách thả xuống
    /// </summary>
    public class DepartmentStockListQueryParam
    {
        /// <summary>
        /// keyword Tên vật tư/Pinyin
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// Kho hiện tại
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        /// Số phiếu phân phối
        /// </summary>
        public string deliveryNo { get; set; }

        /// <summary>
        /// Nhà cung cấp ID
        /// </summary>
        public string gysId { get; set; }

        /// <summary>
        /// Cơ cấu tổ chức
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }
    }
}
