using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.PDS.Requset.DeptPharmacyDrugs
{
    /// <summary>
    /// Lấy thuốc nhà thuốc của bộ phận
    /// </summary>
    public class DeptPharmacyDrugsRequset : RequestBase
    {
        /// <summary>
        /// Bộ phận nhà thuốc yfdm Như：mzyf、zyyf、yjk
        /// </summary>
        [Required]
        public string DeptCode { get; set; }
    }
}
