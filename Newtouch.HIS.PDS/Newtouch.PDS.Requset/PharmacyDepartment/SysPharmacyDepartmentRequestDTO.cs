using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.PharmacyDepartment
{
    /// <summary>
    /// Bộ phận nhà thuốc hệ thống
    /// </summary>
    public class SysPharmacyDepartmentRequestDto : RequestBase
    {
        /// <summary>
        /// ThuốcId
        /// </summary>
        public string ypId { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string organizeId { get; set; }
    }
}