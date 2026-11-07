namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// Thực hiệnkếtnếu
    /// </summary>
    public class ActResultDTO
    {
        /// <summary>
        /// Hàm khởi tạosố Khởi tạo
        /// </summary>
        public ActResultDTO()
        {
            IsSucceed = true;
        }

        /// <summary>
        /// là Khôngthành công
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