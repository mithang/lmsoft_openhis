using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    public class WaitingDispenseMedicineDetailListRequest : RequestBase
    {
        /// <summary>
        /// Ngoại trú xếp thuốc chi tiết đ ơn thuốctra cứu
        /// </summary>
        [Required]
        public string cfh { get; set; }
    }
}
