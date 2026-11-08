using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest.Inpatient
{
    /// <summary>
    /// Lấy Nhập viện bệnh nhân Thông tin cơ bản
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
        /// Tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }
    }
}