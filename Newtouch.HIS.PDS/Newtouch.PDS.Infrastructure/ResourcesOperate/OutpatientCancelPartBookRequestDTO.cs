using FrameworkBase.MultiOrg.DmnService;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// Hủy đặt trước
    /// </summary>
    public class OutpatientCancelPartBookRequestDTO : RequestBase
    {
        /// <summary>
        /// Hủy chi tiết
        /// </summary>
        public List<CancelItemData> Items { get; set; }

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

    /// <summary>
    /// Nội dung hủy đặt trước
    /// </summary>
    public class CancelItemData
    {

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        [Required]
        public string cfh { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        [Required]
        public string ypdm { get; set; }

        /// <summary>
        /// Mã bộ phận nhà thuốc
        /// </summary>
        [Required]
        public string yfbmCode { get; set; }

        /// <summary>
        /// Số lượng hủy (Số lượng đơn vị bộ phận)
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Hệ số quy đổi vàslDùng kết hợp  sl*zhyz=Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int zhyz { get; set; }
    }
}