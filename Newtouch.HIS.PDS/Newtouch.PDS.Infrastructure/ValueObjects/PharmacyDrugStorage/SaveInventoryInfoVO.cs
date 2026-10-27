namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    /// <summary>
    /// Lưu kiểm kê
    /// </summary>
    public class SaveInventoryInfoVO
    {
        /// <summary>
        /// Chi tiết kiểm kêID
        /// </summary>
        public string pdmxId { get; set; }

        /// <summary>
        /// Số lượng thực tế
        /// </summary>
        public int sjsl { get; set; }

        /// <summary>
        /// Mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// Có chia lẻ không
        /// 1： Có
        /// 2： Không
        /// </summary>
        public int? sfcl { get; set; }
    }
}
