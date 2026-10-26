using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    /// <summary>
    /// Bảng thống kê thu chi của trạm
    /// </summary>
    public class SiteCostEarningVO
    {
        /// <summary>
        /// Thông tin thu nhập
        /// </summary>
        public jgssEarningVO srxx { get; set; }
        /// <summary>
        /// Của thông tin thu nhập gridlist
        /// </summary>
        public List<jgssEarningGridVO> srxxList { get; set; }
        /// <summary>
        /// Thông tin chi phí
        /// </summary>
        public List<jgssCostVO> cbxxList { get; set; }

        public List<jgssAttachmentVO> fjxx { get; set; }
    }
}
