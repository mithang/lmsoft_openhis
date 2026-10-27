using FrameworkBase.MultiOrg.DmnService;
using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// Hủy tất cả đặt trước của đơn thuốc chỉ định
    /// </summary>
    public class OutpatientCancelAllBookRequestDTO : RequestBase
    {
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        [Required]
        public string cfh { get; set; }

        /// <summary>
        /// Mã nhà thuốc phát thuốc
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        [Required]
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã nhân viên thao tác
        /// </summary>
        public string CreatorCode { get; set; }
    }
}