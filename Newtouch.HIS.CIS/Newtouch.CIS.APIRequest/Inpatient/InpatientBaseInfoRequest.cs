using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest.Inpatient
{
    /// <summary>
    /// Lấy thông tin cơ bản của bệnh nhân nội trú
    /// </summary>
    public class InpatientBaseInfoRequest : JSONRequestBase
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// ID tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
    }
}