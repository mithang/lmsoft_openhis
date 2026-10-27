namespace Newtouch.Herp.Domain.ValueObjects
{
    /// <summary>
    /// Quan hệ liên kết đơn vị vật tư
    /// </summary>
    public class ProductUnitRelVo
    {
        /// <summary>
        /// gys_contacts Id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Đơn vị ID
        /// </summary>
        public string unitId { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string unitName { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Nhà cung cấp ID
        /// </summary>
        public string keyWord { get; set; }
    }
}
