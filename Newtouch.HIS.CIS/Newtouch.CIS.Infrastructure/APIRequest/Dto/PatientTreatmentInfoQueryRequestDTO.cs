using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// Lấy bệnh nhân Khám bệnh thông tin yêu cầu báo văn
    /// </summary>
    public class PatientTreatmentInfoQueryRequestDTO : RequestBase
    {
        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }
    }
}