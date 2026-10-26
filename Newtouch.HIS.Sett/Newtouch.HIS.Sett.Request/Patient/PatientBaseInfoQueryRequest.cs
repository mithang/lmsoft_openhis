using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request.Patient
{
    /// <summary>
    /// Bệnh nhân cơ bản thông tintra cứu yêu cầu tham số
    /// </summary>
    public class PatientBaseInfoQueryRequest : RequestBase
    {

        /// <summary>
        /// Số đ ơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// ID tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
    }
}