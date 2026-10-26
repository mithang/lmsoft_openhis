using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain
{
    public class LoginBO
    {
        /// <summary>
        /// tên người dùng
        /// </summary>
        [Required(ErrorMessage ="用户名不可为空")]
        public string Username { get; set; }
        /// <summary>
        /// mật khẩu
        /// </summary>
        [Required(ErrorMessage = "密码不可为空")]
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
