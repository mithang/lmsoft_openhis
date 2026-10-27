using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Models
{
    /// <summary>
    /// Tham và Cuộc họpngườiĐăng kýCuộc họp
    /// </summary>
    public class TreatedUserRequest
    {
        [Required(ErrorMessage = "Mã nhân viên không được để trống")]
        public string usercode { get; set; }
        public string username { get; set; }
        /// <summary>
        /// Từ xaĐăng ký khám chữa bệnhSố giao dịch
        /// </summary>
        [Required(ErrorMessage = "Mã đăng ký khám chữa bệnh không được để trống")]
        public string applyId { get; set; }
        /// <summary>
        /// Có hay khôngCuộc họpĐặt lại
        /// Cuộc họpKhi quá hạn,LạiĐăng kýCuộc họp
        /// </summary>
        public bool roomReset { get; set; } = false;
        public string? roomid { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string? mzh { get; set; }
    }
}
