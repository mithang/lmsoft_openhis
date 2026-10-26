using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// Bản tin yêu cầu lấy thông tin khám bệnh của bệnh nhân
    /// </summary>
    public class PatientTreatmentInfoQueryRequestDTO : RequestBase
    {
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// ID tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
    }
}