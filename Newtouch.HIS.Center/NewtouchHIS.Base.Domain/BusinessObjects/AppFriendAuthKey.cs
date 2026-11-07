using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain
{
    /// <summary>
    /// Tạo ủy quyềnkey
    /// </summary>
    public class AppFriendAuthKey
    {
        /// <summary>
        /// Desc:Ủy quyền ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? AuthId { get; set; }

        /// <summary>
        /// Desc:Dùng chung cho ứng dụng Id
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? AppId { get; set; }

        /// <summary>
        /// Desc:Tên ứng dụng
        /// Default:
        /// Nullable:False
        public string? AppName { get; set; }
        public string? FriendId { get; set; }
        public string? FriendAppId { get; set; }
        public string? AuthLevs { get; set; }
        public DateTime? TimeSpanNow { set; get; } = DateTime.Now;

    }
    /// <summary>
    /// Đăng ký Token key
    /// App to Friend
    /// </summary>
    public class AppFriendAuthKeyRequest {
        [Required(ErrorMessage = "Xin cấp quyền AppId không được để trống")]
        public string AppId { get; set; }
        public string? FriendAppId { get; set; }
    }


}
