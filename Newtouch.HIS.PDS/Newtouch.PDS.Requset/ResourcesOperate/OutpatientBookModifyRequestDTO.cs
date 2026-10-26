using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// Thông báo yêu cầu sửa đặt trước
    /// </summary>
    public class OutpatientBookModifyRequestDTO : RequestBase
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        [Required]
        public List<ItemDetail> Items { get; set; }

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

}