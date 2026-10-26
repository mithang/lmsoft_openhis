namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// y lệnh thông tin
    /// </summary>
    public class YzxxVEntity
    {
        /// <summary>
        /// cờ phát thuốc
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// đề nghị trả thuốc cờ 0: không  1: có
        /// </summary>
        public int? sqtybz { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }
    }
}