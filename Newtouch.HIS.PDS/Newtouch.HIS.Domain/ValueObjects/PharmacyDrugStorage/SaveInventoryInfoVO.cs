namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    /// <summary>
    /// lưu kiểm kê
    /// </summary>
    public class SaveInventoryInfoVO
    {
        /// <summary>
        /// chi tiết kiểm kê ID
        /// </summary>
        public string pdmxId { get; set; }

        /// <summary>
        /// số lượng thực tế
        /// </summary>
        public int sjsl { get; set; }

        /// <summary>
        /// mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// có tách lẻ không
        /// 1: có
        /// 2: không
        /// </summary>
        public int? sfcl { get; set; }
    }
}
