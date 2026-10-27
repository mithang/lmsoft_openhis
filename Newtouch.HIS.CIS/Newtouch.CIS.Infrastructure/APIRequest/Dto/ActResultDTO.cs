namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// Thực hiệnkếtnếu
    /// </summary>
    public class ActResultDTO
    {
        /// <summary>
        /// Hàm khởi tạosốKhởi tạo
        /// </summary>
        public ActResultDTO()
        {
            IsSucceed = true;
        }

        /// <summary>
        /// làKhôngthành công
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// LỗiMã
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// LỗiMô tả
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}