namespace Newtouch.HIS.Domain.DO.Book
{
    /// <summary>
    /// xếp thuốc thất bại chi tiết đơn thuốc
    /// </summary>
    public class PyFailCfmxDO
    {
        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }


        /// <summary>
        /// lỗi thông tin
        /// </summary>
        public string errorMsg { get; set; }
    }
}
