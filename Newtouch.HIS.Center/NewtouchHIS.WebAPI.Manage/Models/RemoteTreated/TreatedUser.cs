using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Models
{
    /// <summary>
    /// 参 và Cuộc họp者Đăng kýCuộc họp
    /// </summary>
    public class TreatedUserRequest
    {
        [Required(ErrorMessage = "Mã nhân viên không được để trống")]
        public string usercode { get; set; }
        public string username { get; set; }
        /// <summary>
        /// 远程Đăng ký khám chữa bệnh流水号
        /// </summary>
        [Required(ErrorMessage = "Mã đăng ký khám chữa bệnh không được để trống")]
        public string applyId { get; set; }
        /// <summary>
        /// 是否Cuộc họp重置
        /// Cuộc họp超时时，重新Đăng kýCuộc họp
        /// </summary>
        public bool roomReset { get; set; } = false;
        public string? roomid { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string? mzh { get; set; }
    }
}
