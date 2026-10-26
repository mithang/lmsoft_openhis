namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// thực hiện kết quả
    /// </summary>
    public class ActResultDTO
    {
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        public ActResultDTO()
        {
            IsSucceed = true;
        }

        /// <summary>
        /// Có hay khôngThành công
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// Lỗi mã 
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Mô tả lỗi
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}