namespace NewtouchHIS.Lib.Services.SignalR
{
    public class SignalRMessageVO
    {
        /// <summary>
        /// Người khởi xướng
        /// </summary>
        public string? send_user { get; set; }
        /// <summary>
        /// Người nhận
        /// </summary>

        public string get_user { get; set; }
        /// <summary>
        /// Tiêu đề
        /// </summary>

        public string title { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>

        public string content { get; set; }
        /// <summary>
        /// Thời gian gửi
        /// </summary>

        public DateTime? send_date { get; set; }
        /// <summary>
        /// Thời gian tạo
        /// </summary>

        public DateTime? created_date { get; set; }
    }


}
