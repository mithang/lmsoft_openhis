using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.Base.HOSP.API.Envelope;

namespace Newtouch.HIS.Base.HOSP.Request
{
    public class MedicineStorageIOModeQueryRequest : RequestBase
    {
        [Required]
        public string crkbz { get; set; }
    }
}
