using Newtouch.HIS.API.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// Ngoại trú xếp thuốc yêu cầu 
    /// </summary>
    public class WaitingDispenseMedicineQueryRequest : RequestBase
    {
        /// <summary>
        /// Nhà thuốc ngoại trú nhà thuốc bộ phậnCode
        /// </summary>
        [Required]
        public string yfbmCode { get; set; }

        [Required]
        public DateTime fysjs { get; set; }

        [Required]
        public int fybz { get; set; }

    }
}
