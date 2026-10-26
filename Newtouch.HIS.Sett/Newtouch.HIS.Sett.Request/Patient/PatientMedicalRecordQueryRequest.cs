using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request.Patient
{
    /// <summary>
    /// hồ sơ bệnh án tra cứu
    /// </summary>
    public class PatientMedicalRecordQueryRequest : RequestBase
    {
        /// <summary>
        /// Số hồ sơ bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// hồ sơ bệnh án id(xác nhận duy nhất hồ sơ bệnh án)
        /// </summary>
        public string blId { get; set; }

    }
}
