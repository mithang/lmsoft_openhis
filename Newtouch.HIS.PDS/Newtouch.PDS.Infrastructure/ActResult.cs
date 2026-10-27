using System.ComponentModel;

namespace Newtouch.PDS.Requset
{
    /// <summary>
    /// Kết quả thực hiện
    /// </summary>
    public class ActResult
    {
        /// <summary>
        /// Kết quả thực hiện
        /// </summary>
        public ActResult()
        {
            IsSucceed = true;
            ResultCode = 0;
            ResultMsg = string.Empty;
        }

        /// <summary>
        /// Có thành công không true:Tất cả thành công/Thành công một phần  false:Tất cả thất bại/Kiểm tra thất bại
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// Mã trạng thái Tham chiếu enumResultCode
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// Mô tả lỗi
        /// </summary>
        public string ResultMsg { get; set; }

        /// <summary>
        /// Thông tin trả về
        /// </summary>
        public dynamic Data { get; set; }
    }

    /// <summary>
    /// Enum trạng thái trả về
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// Thực hiện thành công
        /// </summary>
        [Description("Thực hiện thành công")]
        None = 0,

        /// <summary>
        /// Thực hiện thành công
        /// </summary>
        [Description("Thành công một phần")]
        PartSuccess = 1,

        /// <summary>
        /// Thực hiện thành công
        /// </summary>
        [Description("Thành công một phần")]
        AllFailure = 2,

        /// <summary>
        /// Kiểm tra thất bại
        /// </summary>
        [Description("Kiểm tra thất bại")]
        ValidationFailure = 3,

        /// <summary>
        /// Ngoại lệ xử lý nội bộ
        /// </summary>
        [Description("Ngoại lệ xử lý nội bộ")]
        InternalProcessingAbnormality = 4,

        /// <summary>
        /// Lỗi khác
        /// </summary>
        [Description("Lỗi khác")]
        Other = 5,
    }
}
