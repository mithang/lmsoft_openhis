namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Thông tin hóa đơn và nhà cung cấp
    /// </summary>
    public class VFphAndGysEntity
    {
        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Nhà cung cấp ID
        /// </summary>
        public string gysId { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string gysmc { get; set; }
    }
}
