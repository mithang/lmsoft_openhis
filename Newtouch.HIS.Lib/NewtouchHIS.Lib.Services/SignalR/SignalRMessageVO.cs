namespace NewtouchHIS.Lib.Services.SignalR
{
    public class SignalRMessageVO
    {
        /// <summary>
        /// người khởi xướng
        /// </summary>
        public string? send_user { get; set; }
        /// <summary>
        /// người nhận
        /// </summary>

        public string get_user { get; set; }
        /// <summary>
        /// tiêu đề
        /// </summary>

        public string title { get; set; }
        /// <summary>
        /// nội dung
        /// </summary>

        public string content { get; set; }
        /// <summary>
        /// thời gian gửi
        /// </summary>

        public DateTime? send_date { get; set; }
        /// <summary>
        /// thời gian tạo
        /// </summary>

        public DateTime? created_date { get; set; }
    }


}
