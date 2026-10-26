using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Models
{
    public class BookingReqBase
    {
        [Required(ErrorMessage = "应用Id（AppId）必传")]
        public string AppId { get; set; }
        /// <summary>
        /// tổ chứcId（đã xác định được bệnh viện cụ thể）
        /// </summary>
        [Required(ErrorMessage = "机构Id（OrganizeId）必传")]
        public string OrganizeId { get; set; }
        /// <summary>
        /// số hiệu yêu cầu
        /// </summary>
        [Required(ErrorMessage = "方法名（methodcode）必传")]
        public string methodcode { get; set; }
        public string? optype { get; set; }
        /// <summary>
        /// người dùng hiện tại
        /// </summary>
        public string? user { get; set; }
        /// <summary>
        /// thời gian gửi yêu cầu，định dạng"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        [Required(ErrorMessage = "时间戳必传")]
        public DateTime Timestamp { get; set; }

    }

    public class BookingRequest: BookingReqBase
    {
        /// <summary>
        /// tham số đầu vào
        /// </summary>
        public object? paradata { get; set; }
    }

}
