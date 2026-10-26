namespace NewtouchHIS.WebAPI.Manage.Models
{
    public class MeettingTokenRequest
    {
        /// <summary>
        /// số phòng âm thanh – hình ảnh，số phòng của các cuộc họp khác nhau phải duy nhất，độ dài không vượt quá 50 vị trí，khuyến nghị sử dụng khóa chính tự tăng hoặc UUID
        /// </summary>
        public string roomid { get; set; }
        /// <summary>
        /// tên người dùng vào phòng đó，dùng để phân biệt người dùng khác nhau
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// thiết bị sử dụng âm thanh – hình ảnh，khuyến nghị lấy giá trị là Mobile（thiết bị di động），Browser（trình duyệt máy tính）
        /// </summary>
        public string device { get; set; }
    }

    public class MeettingTokenResponse
    {
        /// <summary>
        /// là tài khoản âm thanh – hình ảnh để Secret tiến hành bằng khóa bí mật AES kết quả sau khi mã hóa đối xứng，cần dùng khi gọi dịch vụ âm thanh – hình ảnh，vui lòng giải mã rồi sử dụng
        /// </summary>
        public string rtcuserid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roomid { get; set; }
        public string? username { get; set; }

        /// <summary>
        /// nếu status là Success thì yêu cầu thành công，nếu status là Failure thì yêu cầu thất bại，errorCode hiển thị nguyên nhân thất bại trong。
        /// </summary>
        public string status { get; set; }
        public string errorCode { get; set; }
    }

    public class MeettingResponse
    {
        /// <summary>
        /// là tài khoản âm thanh – hình ảnh để Secret tiến hành bằng khóa bí mật AES kết quả sau khi mã hóa đối xứng，cần dùng khi gọi dịch vụ âm thanh – hình ảnh，vui lòng giải mã rồi sử dụng
        /// </summary>
        public string rtcuserid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roomid { get; set; }
        public string? username { get; set; }
        public string? roompath { get; set; }
        public string? organization { get; set; }
        public string? device { get; set; }
    }
}
          