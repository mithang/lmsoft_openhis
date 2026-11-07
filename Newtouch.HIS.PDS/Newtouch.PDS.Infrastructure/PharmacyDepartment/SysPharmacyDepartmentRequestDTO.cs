using FrameworkBase.MultiOrg.DmnService;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.PharmacyDepartment
{
    /// <summary>
    /// Bộ phận nhà thuốc hệ thống
    /// </summary>
    public class SysPharmacyDepartmentRequestDto : RequestBase
    {
        /// <summary>
        /// Thuốc Id
        /// </summary>
        public string ypId { get; set; }

        /// <summary>
        /// Tổ chức ID
        /// </summary>
        public string organizeId { get; set; }
    }
}