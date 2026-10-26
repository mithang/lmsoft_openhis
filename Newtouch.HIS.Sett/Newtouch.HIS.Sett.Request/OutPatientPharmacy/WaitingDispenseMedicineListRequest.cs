using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    public class WaitingDispenseMedicineListRequest : RequestBase
    {
        /// <summary>
        /// Nhà thuốc ngoại trú nhà thuốc bộ phậnCode
        /// </summary>
        [Required]
        public string yfbmCode { get; set; }

        [Required]
        public int yxq { get; set; }


    }
}
