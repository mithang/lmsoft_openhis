namespace Newtouch.Domain.DTO
{
    /// <summary>
    /// Danh sách thả xuốngvật tưthông tin
    /// </summary>
    public class DepartmentStockListQueryParamDTO
    {
        /// <summary>
        /// keyword vật tưTên/Pinyin
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// hiện tạiKho
        /// </summary>
        public string warehouseId { get; set; }



        /// <summary>
        /// tổ chứckếtcấu trúc
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }
    }
}
