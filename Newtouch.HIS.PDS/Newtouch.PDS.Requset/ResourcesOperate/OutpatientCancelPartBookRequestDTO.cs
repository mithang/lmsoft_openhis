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
        /// Chi tiết hủy
        /// </summary>
        public List<CancelItemData> Items { get; set; }

        /// <summary>
        /// ID tổ chức
        /// </summary>
        [Required]
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã người thao tác
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
        /// Số lượng hủy （số lượng đơn vị bộ phận）
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi dùng cùng sl  sl*zhyz=số lượng đơn vị nhỏ nhất
        /// </summary>
        public int zhyz { get; set; }
    }
}