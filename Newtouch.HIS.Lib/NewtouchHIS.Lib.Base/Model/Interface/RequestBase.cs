using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Lib.Base.Model
{
    public class RequestBase
    {
        [Required(ErrorMessage = "Id ứng dụng (AppId) bắt buộc phải truyền")]
        /// <summary>
        /// ứng dụngId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// tổ chứcId
        /// </summary>
        public string? OrganizeId { get; set; }

        [Required(ErrorMessage = "Dấu thời gian bắt buộc phải truyền")]
        /// <summary>
        /// Thời gian gửi yêu cầu, định dạng"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        public DateTime Timestamp { get; set; }

    }

    public class BusRequest : RequestBase
    {
        /// <summary>
        /// Loại mã thông báo
        /// </summary>
        public string? TokenType { get; set; }

        /// <summary>
        /// Mã thông báo truy cập
        /// </summary>
        public string? Token { get; set; }
    }
}
