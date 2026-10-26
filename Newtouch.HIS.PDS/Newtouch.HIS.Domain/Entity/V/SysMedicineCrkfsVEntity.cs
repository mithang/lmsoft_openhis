namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// hệ thống thuốc phương thức nhập/xuất kho
    /// </summary>
    public class SysMedicineCrkfsVEntity
    {
        /// <summary>
        /// phương thức nhập/xuất kho ID
        /// </summary>
        public int crkfsId { get; set; }

        /// <summary>
        /// phương thức nhập/xuất kho mã
        /// </summary>
        public string crkfsCode { get; set; }

        /// <summary>
        /// phương thức nhập/xuất kho tên
        /// </summary>
        public string crkfsmc { get; set; }

        /// <summary>
        /// cờ nhập/xuất kho  0: nhập kho  1: xuất kho
        /// </summary>
        public string crkbz { get; set; }

        /// <summary>
        /// trạng thái  1: còn hiệu lực 0: không hiệu lực
        /// </summary>
        public string zt { get; set; }
    }
}
