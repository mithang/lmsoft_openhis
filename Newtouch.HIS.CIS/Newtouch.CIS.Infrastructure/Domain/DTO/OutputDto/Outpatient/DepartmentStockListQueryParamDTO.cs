namespace Newtouch.Domain.DTO
{
    /// <summary>
    /// Danh sách thả xuống vật tư thông tin
    /// </summary>
    public class DepartmentStockListQueryParamDTO
    {
        /// <summary>
        /// keyword vật tư Tên/Pinyin
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// hiện tại Kho
        /// </summary>
        public string warehouseId { get; set; }



        /// <summary>
        /// tổ chức kết cấu trúc
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }
    }
}
