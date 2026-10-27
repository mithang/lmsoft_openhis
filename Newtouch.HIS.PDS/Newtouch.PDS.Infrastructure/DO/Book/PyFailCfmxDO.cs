namespace Newtouch.HIS.Domain.DO.Book
{
    /// <summary>
    /// Chi tiết đơn thuốc xếp thuốc thất bại
    /// </summary>
    public class PyFailCfmxDO
    {
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }


        /// <summary>
        /// Thông tin lỗi
        /// </summary>
        public string errorMsg { get; set; }
    }
}
