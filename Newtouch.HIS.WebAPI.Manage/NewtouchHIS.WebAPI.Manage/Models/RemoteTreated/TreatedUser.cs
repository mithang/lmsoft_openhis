using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Models
{
    /// <summary>
    /// người tham gia cuộc họp đăng ký cuộc họp
    /// </summary>
    public class TreatedUserRequest
    {
        [Required(ErrorMessage = "职工工号不可为空")]
        public string usercode { get; set; }
        public string username { get; set; }
        /// <summary>
        /// số thứ tự đăng ký khám chữa bệnh từ xa
        /// </summary>
        [Required(ErrorMessage = "诊疗申请流水号不可为空")]
        public string applyId { get; set; }
        /// <summary>
        /// có đặt lại cuộc họp hay không
        /// khi cuộc họp quá thời gian，đăng ký lại cuộc họp
        /// </summary>
        public bool roomReset { get; set; } = false;
        public string? roomid { get; set; }
        /// <summary>
        /// số khám ngoại trú
        /// </summary>
        public string? mzh { get; set; }
    }
}
