using FrameworkBase.MultiOrg.DmnService;
using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.PDS.Requset.DeptPharmacyDrugs
{
    /// <summary>
    /// Lấy thuốc nhà thuốc bộ phận
    /// </summary>
    public class DeptPharmacyDrugsRequset : RequestBase
    {
        /// <summary>
        /// Bộ phận nhà thuốc yfdm như：mzyf、zyyf、yjk
        /// </summary>
        [Required]
        public string DeptCode { get; set; }
    }
}
