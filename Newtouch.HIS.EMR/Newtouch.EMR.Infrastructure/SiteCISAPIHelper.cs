using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Web;

namespace Newtouch.EMR.Infrastructure
{
    /// <summary>
    /// Lớp trợ giúp yêu cầu CIS API
    /// </summary>
    public class SiteCISAPIHelper : SiteAPIRequestHelperBase<SiteCISAPIHelper>
    {
        /// <summary>
        /// Key cấu hình Addr của miền
        /// </summary>
        private readonly static string _domainConfigKey = "SiteCISAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "CIS";

    }

    /// <summary>
    /// Đồng bộ thông tin bệnh nhân nội trú
    /// </summary>
    public class InpatientPatientInfoDTO
    {
        /// <summary>
        /// id tổ chức
        /// </summary>
        public string orgId { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Mã nhân viên của người dùng
        /// </summary>
        public string rygh { get; set; }
        /// <summary>
        /// Tên người dùng
        /// </summary>
        public string username { get; set; }
        public string GetMAC { get; set; }

    }
}
