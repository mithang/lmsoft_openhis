using System.ComponentModel.DataAnnotations;

namespace HIS.BaseAPI.Models.System
{
    /// <summary>
    /// danh sách menu ủy quyền vai trò
    /// </summary>
    public class MenuAuthRequest
    {
        public List<string>? RoleIdList { get; set; }
        public bool IsRoot { get; set; } = false;
        public bool IsAdministrator { get; set; } = false;
        public string? DB { get; set; }
        public string? UserId { get; set; }
        /// <summary>
        /// chỉ giới hạn menu có hiệu lực
        /// </summary>
        public bool ValidLimit { get; set; } = true;
        /// <summary>
        /// ứng dụng Id
        /// </summary>
        public string? MenuAppId { get; set; }
    }
}
