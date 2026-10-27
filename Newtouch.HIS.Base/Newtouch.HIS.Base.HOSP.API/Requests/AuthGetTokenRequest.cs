using Newtouch.HIS.Base.HOSP.API.Envelope;

namespace Newtouch.HIS.Base.HOSP.Request
{
    public class AuthGetTokenRequest : RequestBase
    {
        public string UserId { get; set; }
        public string Account { get; set; }
        public string OrganizeId { get; set; }
        public string TopOrganizeId { get; set; }
    }
}
