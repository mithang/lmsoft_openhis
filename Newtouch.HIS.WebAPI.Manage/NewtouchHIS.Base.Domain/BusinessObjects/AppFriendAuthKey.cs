using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain
{
    /// <summary>
    /// tạo ủy quyềnkey
    /// </summary>
    public class AppFriendAuthKey
    {
        /// <summary>
        /// Desc:ủy quyềnID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? AuthId { get; set; }

        /// <summary>
        /// Desc:ứng dụng chungId
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? AppId { get; set; }

        /// <summary>
        /// Desc:tên ứng dụng
        /// Default:
        /// Nullable:False
        public string? AppName { get; set; }
        public string? FriendId { get; set; }
        public string? FriendAppId { get; set; }
        public string? AuthLevs { get; set; }
        public DateTime? TimeSpanNow { set; get; } = DateTime.Now;

    }
    /// <summary>
    /// đăng kýToken key
    /// App to Friend
    /// </summary>
    public class AppFriendAuthKeyRequest {
        [Required(ErrorMessage = "申请授权AppId不可为空")]
        public string AppId { get; set; }
        public string? FriendAppId { get; set; }
    }


}
