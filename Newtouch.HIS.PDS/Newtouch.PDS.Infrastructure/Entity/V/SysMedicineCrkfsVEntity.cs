namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// Hệ thống Thuốc Phương thức xuất nhập kho
    /// </summary>
    public class SysMedicineCrkfsVEntity
    {
        /// <summary>
        /// Phương thức xuất nhập khoID
        /// </summary>
        public int crkfsId { get; set; }

        /// <summary>
        /// Mã phương thức xuất nhập kho
        /// </summary>
        public string crkfsCode { get; set; }

        /// <summary>
        /// Tên phương thức xuất nhập kho
        /// </summary>
        public string crkfsmc { get; set; }

        /// <summary>
        /// Cờ xuất nhập kho  0：Nhập kho  1：Xuất kho
        /// </summary>
        public string crkbz { get; set; }

        /// <summary>
        /// Trạng thái  1：Hiệu lực 0：Không hiệu lực
        /// </summary>
        public string zt { get; set; }
    }
}
