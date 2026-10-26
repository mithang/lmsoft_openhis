using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Lib.Base.Model
{
    public class RequestBase
    {
        [Required(ErrorMessage = "应用Id（AppId）必传")]
        /// <summary>
        /// ứng dụngId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// tổ chứcId
        /// </summary>
        public string? OrganizeId { get; set; }

        [Required(ErrorMessage = "时间戳必传")]
        /// <summary>
        /// thời gian gửi yêu cầu，định dạng"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        public DateTime Timestamp { get; set; }

    }

    public class BusRequest : RequestBase
    {
        /// <summary>
        /// loại mã thông báo
        /// </summary>
        public string? TokenType { get; set; }

        /// <summary>
        /// mã thông báo truy cập
        /// </summary>
        public string? Token { get; set; }
    }
}
