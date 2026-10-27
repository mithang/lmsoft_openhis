using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Models
{
    public class BookingReqBase
    {
        [Required(ErrorMessage = "Id ứng dụng (AppId) là bắt buộc")]
        public string AppId { get; set; }
        /// <summary>
        /// Tổ chứcId（已经定位到了具体医院）
        /// </summary>
        [Required(ErrorMessage = "Tổ chứcId（OrganizeId） là bắt buộc")]
        public string OrganizeId { get; set; }
        /// <summary>
        /// 请求编号
        /// </summary>
        [Required(ErrorMessage = "Tên phương thức（methodcode） là bắt buộc")]
        public string methodcode { get; set; }
        public string? optype { get; set; }
        /// <summary>
        /// 当前用户
        /// </summary>
        public string? user { get; set; }
        /// <summary>
        /// 发送请求的时间，格式"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        [Required(ErrorMessage = "Dấu thời gian là bắt buộc")]
        public DateTime Timestamp { get; set; }

    }

    public class BookingRequest: BookingReqBase
    {
        /// <summary>
        /// 入参
        /// </summary>
        public object? paradata { get; set; }
    }

}
