using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest.Inpatient
{
    /// <summary>
    /// LấyNhập việnbệnh nhânThông tin cơ bản
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
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }
    }
}