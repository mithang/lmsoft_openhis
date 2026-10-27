using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Models
{
    public class BookingReqBase
    {
        [Required(ErrorMessage = "Id ứng dụng (AppId) là bắt buộc")]
        public string AppId { get; set; }
        /// <summary>
        /// Tổ chứcId(Đã xác định tới bệnh viện cụ thể)
        /// </summary>
        [Required(ErrorMessage = "Tổ chứcId(OrganizeId) là bắt buộc")]
        public string OrganizeId { get; set; }
        /// <summary>
        /// Số hiệu yêu cầu
        /// </summary>
        [Required(ErrorMessage = "Tên phương thức(methodcode) là bắt buộc")]
        public string methodcode { get; set; }
        public string? optype { get; set; }
        /// <summary>
        /// Người dùng hiện tại
        /// </summary>
        public string? user { get; set; }
        /// <summary>
        /// Thời gian gửi yêu cầu,Định dạng"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        [Required(ErrorMessage = "Dấu thời gian là bắt buộc")]
        public DateTime Timestamp { get; set; }

    }

    public class BookingRequest: BookingReqBase
    {
        /// <summary>
        /// Tham số đầu vào
        /// </summary>
        public object? paradata { get; set; }
    }

}
