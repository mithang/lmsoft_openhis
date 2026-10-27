using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain
{
    public class LoginBO
    {
        /// <summary>
        /// Tên người dùng
        /// </summary>
        [Required(ErrorMessage ="Tên người dùng không được để trống")]
        public string Username { get; set; }
        /// <summary>
        /// Mật khẩu
        /// </summary>
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class UALoginBO
    {
        public string? access_token { get; set; }
    }
}
