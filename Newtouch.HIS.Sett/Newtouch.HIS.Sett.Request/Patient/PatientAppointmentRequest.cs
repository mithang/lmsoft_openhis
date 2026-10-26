using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// 签 đ ến 
    /// </summary>
    public class PatientAppointmentRequest : RequestBase
    {
        public string mzh { get; set; }
        public string orgId { get; set; }

    }
}