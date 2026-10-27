namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// Thông tin y lệnh
    /// </summary>
    public class YzxxVEntity
    {
        /// <summary>
        /// Cờ phát thuốc
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// Cờ xin trả thuốc 0：Không  1：Có
        /// </summary>
        public int? sqtybz { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }
    }
}