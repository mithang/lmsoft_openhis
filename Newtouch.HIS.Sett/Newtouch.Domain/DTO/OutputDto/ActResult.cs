using System.ComponentModel;

namespace Newtouch.HIS.Domain.DTO.OutputDto
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
        /// Có thành công hay không true:Tất cả thành công/Một phần thành công  false:Tất cả thất bại/Xác minh thất bại
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
        /// Trả về thông tin
        /// </summary>
        public dynamic Data { get; set; }
    }

    /// <summary>
    /// Trả về enum trạng thái
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// Thực hiện thành công
        /// </summary>
        [Description("执行成功")]
        None = 0,

        /// <summary>
        /// Thực hiện thành công
        /// </summary>
        [Description("部分成功")]
        PartSuccess = 1,

        /// <summary>
        /// Thực hiện thành công
        /// </summary>
        [Description("部分成功")]
        AllFailure = 2,

        /// <summary>
        /// Xác minh thất bại
        /// </summary>
        [Description("验证失败")]
        ValidationFailure = 3,

        /// <summary>
        /// Ngoại lệ xử lý nội bộ
        /// </summary>
        [Description("内部处理异常")]
        InternalProcessingAbnormality = 4,

        /// <summary>
        /// Lỗi khác
        /// </summary>
        [Description("其他错误")]
        Other = 5,
    }
}
