namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// Kết quả thực hiện
    /// </summary>
    public class ActResultDTO
    {
        /// <summary>
        /// Khởi tạo hàm khởi tạo
        /// </summary>
        public ActResultDTO()
        {
            IsSucceed = true;
        }

        /// <summary>
        /// Có thành công hay không
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Mô tả lỗi
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}