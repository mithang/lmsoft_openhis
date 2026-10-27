using FrameworkBase.MultiOrg.DmnService;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// Bản tin yêu cầu sửa đặt trước
    /// </summary>
    public class OutpatientBookModifyRequestDTO : RequestBase
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        [Required]
        public List<ItemDetail> Items { get; set; }

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