using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.Base.HOSP.API.Envelope;

namespace Newtouch.HIS.Base.HOSP.Request
{
    public class UpdateBedOccupyRequest : RequestBase
    {
        [Required]
        public string cwCode { get; set; }

        [Required]
        public bool isOccupy { get; set; }
    }
}
