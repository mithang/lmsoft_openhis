using FrameworkBase.MultiOrg.DmnService;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// Bản tin yêu cầu hủy xếp thuốc ngoại trú
    /// </summary>
    public class OutpatientCancelArrangementRequestDTO : RequestBase
    {
        /// <summary>
        /// Số đơn thuốc đích
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Số đơn thuốc xử lý tối đa 0:Xử lý tất cả
        /// </summary>
        public int processesMaxNum { get; set; }

        /// <summary>
        /// Mã nhà thuốc phát thuốc
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Người gửi
        /// </summary>
        public string CreatorCode { get; set; }
    }
}