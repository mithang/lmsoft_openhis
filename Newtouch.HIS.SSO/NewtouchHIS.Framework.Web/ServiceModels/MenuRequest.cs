using NewtouchHIS.Base.Domain.ValueObjects;

namespace NewtouchHIS.Framework.Web.ServiceModels
{
    /// <summary>
    /// danh sách menu ủy quyền vai trò
    /// </summary>
    public class MenuAuthRequest
    {
        public List<string> RoleIdList { get; set; }
        public bool IsRoot { get; set; } = false;
        public bool IsAdministrator { get; set; } = false;
        public string? DB { get; set; }
        public string? UserId { get; set; }
        /// <summary>
        /// （menu）hệ thống nghiệp vụAppId
        /// </summary>
        public string? MenuAppId { get; set; }
        /// <summary>
        /// chỉ giới hạn menu có hiệu lực
        /// </summary>
        public bool ValidLimit { get; set; } = true;
        /// <summary>
        /// có hiển thị đã đồng bộ hay không
        /// </summary>
        public bool? ShowSync { get; set; }=false;
        
    }
    /// <summary>
    /// thêm menu
    /// </summary>
    public class MenuAddRequest : SysModuleVO
    {
        public string user { get; set; }
    }
}
