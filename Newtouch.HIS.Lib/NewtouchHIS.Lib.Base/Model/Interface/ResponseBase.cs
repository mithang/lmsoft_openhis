namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// APIphản hồi
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// Mã trả về nghiệp vụ
        /// </summary>
        public ResponseResultCode code { get; set; }

        /// <summary>
        /// Mô tả mã trả về nghiệp vụ
        /// </summary>
        public string? msg { get; set; }

        /// <summary>
        /// Dữ liệu phản hồi (ResponseResultCode.SUCCESSdữ liệu trả về khi )
        /// </summary>
        public object? data { get; set; }

    }
    /// <summary>
    /// Lớp chuyển tiếp chỉ dùng để gọi nội bộ hisApi
    /// </summary>
    public class ResponseBaseOld : ResponseBase
    {

        public string sub_code { get; set; }
        public string sub_msg { get; set; }
    }


    public enum ResponseResultCode
    {
        /// <summary>
        /// Giá trị mặc định
        /// </summary>
        Default = -1,
        /// <summary>
        /// thành công
        /// </summary>
        SUCCESS = 10000,

        /// <summary>
        /// Lỗi nội bộ máy chủ (chương trình xảy ra ngoại lệ)
        /// </summary>
        ERROR = 20000,
        /// <summary>
        /// TokenKhông hợp lệ
        /// </summary>
        INVALIDTOKEN = 40000,
        /// <summary>
        /// Xác thực thất bại
        /// </summary>
        FAILOfAuth = 40001,
        /// <summary>
        /// Yêu cầu trùng lặp
        /// </summary>
        FAILOfRequestRepeat = 40002,
        /// <summary>
        /// Xử lý nghiệp vụ thất bại:	Nguyên nhân thất bại cụ thể xem mã lỗi do giao diện trả về    
        /// </summary>
        FAIL = 40004,
        /// <summary>
        /// Khởi tạo cấu hình ngoại lệ
        /// </summary>
        FAILOfConfigInit=400040,
        /// <summary>
        /// Mật khẩu sai
        /// </summary>
        FAILOfPwdError = 400041,
        /// <summary>
        /// Kiểm tra giá trị trống
        /// </summary>
        FAILOfEmpty = 410001,
        /// <summary>
        /// Kiểm tra giá trị trùng lặp
        /// </summary>
        FAILOfExists = 410002,

    }
}
