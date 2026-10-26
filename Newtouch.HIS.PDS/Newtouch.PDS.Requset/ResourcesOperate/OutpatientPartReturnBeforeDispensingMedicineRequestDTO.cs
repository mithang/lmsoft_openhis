using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// （Trước khi phát thuốc）Thông báo yêu cầu trả thuốc một phần
    /// </summary>
    public class OutpatientPartReturnBeforeDispensingMedicineRequestDTO : RequestBase
    {
        /// <summary>
        /// Chi tiết hủy
        /// </summary>
        [Required]
        public List<ReturnItemData> Items { get; set; }

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
        /// Số lượng trả （số lượng đơn vị bộ phận）
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi dùng cùng sl  sl*zhyz=số lượng đơn vị nhỏ nhất
        /// </summary>
        public int zhyz { get; set; }
    }
}