namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// theo bộ phận nhà thuốc tổng hợp phát thuốc tổng số
    /// </summary>
    public class FyCountVoByYfbm
    {
        /// <summary>
        /// phát thuốc tổng số
        /// </summary>
        public long fyCount { get; set; }

        /// <summary>
        /// phát thuốc sự kiện yyyy-MM
        /// </summary>
        public string fysj { get; set; }
    }
}
