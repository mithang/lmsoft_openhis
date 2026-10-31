using FrameworkBase.MultiOrg.DmnService;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// (Trước khi phát thuốc)Bản tin yêu cầu trả thuốc một phần
    /// </summary>
    public class OutpatientPartReturnBeforeDispensingMedicineRequestDTO : RequestBase
    {
        /// <summary>
        /// Hủy chi tiết
        /// </summary>
        [Required]
        public List<ReturnItemData> Items { get; set; }

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
    /// Nội dung chi tiết trả thuốc
    /// </summary>
    public class ReturnItemData
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
        /// Bộ phận nhà thuốc
        /// </summary>
        public string Yfbm { get; set; }

        /// <summary>
        /// Số lượng trả (Số lượng đơn vị bộ phận)
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Hệ số quy đổi vàslDùng kết hợp  sl*zhyz=Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int zhyz { get; set; }
    }
}