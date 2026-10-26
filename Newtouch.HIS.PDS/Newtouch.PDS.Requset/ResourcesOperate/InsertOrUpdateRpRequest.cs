using Newtouch.HIS.API.Common;
using System.Collections.Generic;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// Thông báo yêu cầu thêm mới hoặc sửa đơn thuốc
    /// </summary>
    public class InsertOrUpdateRpRequest : RequestBase
    {
        /// <summary>
        /// Tập hợp đơn thuốc thêm mới hoặc sửa
        /// </summary>
        public List<RpInfo> Rps { get; set; }
    }

    /// <summary>
    /// Thông tin đơn thuốc
    /// </summary>
    public class RpInfo
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
        /// Mã người thao tác
        /// </summary>
        public string CreatorCode { get; set; }
    }

    /// <summary>
    /// Đơn thuốc chèn hoặc cập nhật thất bại
    /// </summary>
    public class FailRp : RpInfo
    {
        /// <summary>
        /// Nguyên nhân thất bại
        /// </summary>
        public string FailMsg { get; set; }
    }
}
