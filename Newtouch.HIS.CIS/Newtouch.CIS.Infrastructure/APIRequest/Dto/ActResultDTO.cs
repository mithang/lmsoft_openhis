namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// Thực hiện kết nếu
    /// </summary>
    public class ActResultDTO
    {
        /// <summary>
        /// Hàm khởi tạo số Khởi tạo
        /// </summary>
        public ActResultDTO()
        {
            IsSucceed = true;
        }

        /// <summary>
        /// là Không thành công
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// Lỗi Mã
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Lỗi Mô tả
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}