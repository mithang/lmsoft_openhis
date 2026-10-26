using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// Tra cứu bệnh nhân nhập viện
    /// </summary>
    public class InPatientDetailQueryRequest : RequestBase
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// ID tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

    }
}