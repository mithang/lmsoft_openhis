namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Tổng hợp tổng Số phát thuốc theo bộ phận nhà thuốc
    /// </summary>
    public class FyCountVoByYfbm
    {
        /// <summary>
        /// Tổng Số phát thuốc
        /// </summary>
        public long fyCount { get; set; }

        /// <summary>
        /// Sự kiện phát thuốc yyyy-MM
        /// </summary>
        public string fysj { get; set; }
    }
}
