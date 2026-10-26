
namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thời lượng ngoại trú hiệu lực của khoa hiện tại
    /// </summary>
    public class MzActiveDurationVO
    {
        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Thời lượng ngoại trú hiệu lực
        /// </summary>
        public int mz { get; set; }

        /// <summary>
        /// Thời lượng cấp cứu hiệu lực
        /// </summary>
        public int jz { get; set; }
    }
}
