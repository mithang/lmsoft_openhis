using System.ComponentModel;

namespace Newtouch.Herp.Domain.DTO.OutputDto
{
    /// <summary>
    /// Kết quả thực thi
    /// </summary>
    public class ActResult
    {
        /// <summary>
        /// Kết quả thực thi
        /// </summary>
        public ActResult()
        {
            IsSucceed = true;
            ResultCode = 0;
            ResultMsg = string.Empty;
        }

        /// <summary>
        /// Có thành công không true:Tất cả thành công/Thành công một phần  false:Tất cả thất bại/Xác thực thất bại
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// Mã trạng thái Tham chiếu enum ResultCode
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
        /// Thực thi thành công
        /// </summary>
        [Description("Thực thi thành công")]
        None = 0,

        /// <summary>
        /// Thực thi thành công
        /// </summary>
        [Description("Thành công một phần")]
        PartSuccess = 1,

        /// <summary>
        /// Thực thi thành công
        /// </summary>
        [Description("Thành công một phần")]
        AllFailure = 2,

        /// <summary>
        /// Xác thực thất bại
        /// </summary>
        [Description("Xác thực thất bại")]
        ValidationFailure = 3,

        /// <summary>
        /// Lỗi xử lý nội bộ
        /// </summary>
        [Description("Lỗi xử lý nội bộ")]
        InternalProcessingAbnormality = 4,

        /// <summary>
        /// Lỗi khác
        /// </summary>
        [Description("Lỗi khác")]
        Other = 5,
    }
}
