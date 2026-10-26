namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// APIphản hồi
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// mã trả về nghiệp vụ
        /// </summary>
        public ResponseResultCode code { get; set; }

        /// <summary>
        /// mô tả mã trả về nghiệp vụ
        /// </summary>
        public string? msg { get; set; }

        /// <summary>
        /// dữ liệu phản hồi（ResponseResultCode.SUCCESSdữ liệu trả về khi đó）
        /// </summary>
        public object? data { get; set; }

    }
    /// <summary>
    /// lớp chuyển tiếp chỉ dùng để gọihisnội bộApi
    /// </summary>
    public class ResponseBaseOld : ResponseBase
    {

        public string sub_code { get; set; }
        public string sub_msg { get; set; }
    }


    public enum ResponseResultCode
    {
        /// <summary>
        /// giá trị mặc định
        /// </summary>
        Default = -1,
        /// <summary>
        /// thành công
        /// </summary>
        SUCCESS = 10000,

        /// <summary>
        /// lỗi nội bộ máy chủ（chương trình xảy ra ngoại lệ）
        /// </summary>
        ERROR = 20000,
        /// <summary>
        /// Tokenkhông hợp lệ
        /// </summary>
        INVALIDTOKEN = 40000,
        /// <summary>
        /// xác thực thất bại
        /// </summary>
        FAILOfAuth = 40001,
        /// <summary>
        /// yêu cầu trùng lặp
        /// </summary>
        FAILOfRequestRepeat = 40002,
        /// <summary>
        /// xử lý nghiệp vụ thất bại:	nguyên nhân thất bại cụ thể xem mã lỗi giao diện trả về    
        /// </summary>
        FAIL = 40004,
        /// <summary>
        /// ngoại lệ khởi tạo cấu hình
        /// </summary>
        FAILOfConfigInit=400040,
        /// <summary>
        /// mật khẩu sai
        /// </summary>
        FAILOfPwdError = 400041,
        /// <summary>
        /// kiểm tra giá trị trống
        /// </summary>
        FAILOfEmpty = 410001,
        /// <summary>
        /// kiểm tra giá trị trùng lặp
        /// </summary>
        FAILOfExists = 410002,

    }
}
