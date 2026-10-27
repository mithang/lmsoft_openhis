namespace Newtouch.Herp.Domain.ValueObjects
{
    /// <summary>
    /// supplier contact vo
    /// </summary>
    public class SupplierContactVO
    {
        /// <summary>
        /// gys_contacts Id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Tên người liên hệ
        /// </summary>
        public string contactName { get; set; }

        /// <summary>
        /// Điện thoại di động
        /// </summary>
        public string telphone { get; set; }

        /// <summary>
        /// Chức trách
        /// </summary>
        public string duties { get; set; }

        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Nhà cung cấp ID
        /// </summary>
        public string keyWord { get; set; }

    }
}
