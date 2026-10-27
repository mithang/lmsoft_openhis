using Newtouch.HIS.Base.HOSP.API.Envelope;
using NewtouchHIS.Lib.Base.Model;

namespace Newtouch.HIS.Base.HOSP.Request
{
    public class MedicineQueryRequest : Newtouch.HIS.Base.HOSP.API.Envelope.RequestBase
    {
        public string orgId { get; set; }
        public string xmbm { get; set; }
        public string xmmc { get; set; }
        public string ck_kc { get; set; }
        public Pagination pagination { get; set; }
    }
}
