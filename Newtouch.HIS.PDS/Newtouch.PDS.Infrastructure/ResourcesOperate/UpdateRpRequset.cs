using FrameworkBase.MultiOrg.DmnService;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;


namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// Sửa đơn thuốc
    /// </summary>
    public class UpdateRpRequset : RequestBase
    {
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string Cfh { get; set; }

        /// <summary>
        /// Mã bộ phận nhà thuốc
        /// </summary>
        public string Yfbm { get; set; }

        /// <summary>
        /// Mã nội bộ đơn thuốc
        /// </summary>
        public int Cfnm { get; set; }

        /// <summary>
        /// Cơ cấu tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốc sau khi sửa
        /// </summary>
        public List<Cfmx> Cfmx { get; set; }

        /// <summary>
        /// Mã nhân viên thao tác
        /// </summary>
        public string CreatorCode { get; set; }
    }

    /// <summary>
    /// Chi tiết đơn thuốc
    /// </summary>
    public class Cfmx
    {

        /// <summary>
        /// Mã nội bộ chi tiết đơn thuốc(Không bắt buộc nhập)
        /// </summary>
        public int Cfmxnm { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        [Required]
        public string YpCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string YpName { get; set; }

        /// <summary>
        /// Số lượng thuốc
        /// </summary>
        [Required]
        public int YpCount { get; set; }

        /// <summary>
        /// Loại thu phí lớn
        /// </summary>
        public string DlCode { get; set; }
    }
}
