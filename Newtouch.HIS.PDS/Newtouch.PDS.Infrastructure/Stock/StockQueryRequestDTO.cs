using FrameworkBase.MultiOrg.DmnService;
using Newtouch.HIS.API.Common;
using System.Collections.Generic;

namespace Newtouch.PDS.Requset
{
    /// <summary>
    /// Truy vấn tồn kho theo mã thuốc
    /// </summary>
    public class StockQueryRequestDTO : RequestBase
    {
        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tập hợp thông tin thuốc
        /// </summary>
        public List<DrugInfo> yplist { get; set; }
    }

    /// <summary>
    /// Thông tin thuốc
    /// </summary>
    public class DrugInfo
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Nhà thuốc nhận thuốc
        /// </summary>
        public string lyyf { get; set; }
    }
}
