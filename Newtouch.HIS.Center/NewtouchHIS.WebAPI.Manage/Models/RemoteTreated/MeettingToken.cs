namespace NewtouchHIS.WebAPI.Manage.Models
{
    public class MeettingTokenRequest
    {
        /// <summary>
        /// Số phòng âm thanh/hình ảnh,Khác nhauCuộc họpsố phòng cần duy nhất,Độ dài không vượt quá 50 ký tự,Khuyên dùng khóa chính tự tăngKhóa chínhhoặc UUID
        /// </summary>
        public string roomid { get; set; }
        /// <summary>
        /// Là tên người dùng vào phòng nàyHọ tên,để phân biệt người dùng khác nhau
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// Là thiết bị dùng cho âm thanh/hình ảnh,Khuyên lấy giá trị Mobile(Thiết bị di động),Browser(Trình duyệt máy tính)
        /// </summary>
        public string device { get; set; }
    }

    public class MeettingTokenResponse
    {
        /// <summary>
        /// Là kết quả mã hóa đối xứng AES của tài khoản âm thanh/hình ảnh dùng Secret làm khóa, khi gọi dịch vụ âm thanh/hình ảnh cần dùng tới, vui lòng giải mã rồi dùng
        /// </summary>
        public string rtcuserid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roomid { get; set; }
        public string? username { get; set; }

        /// <summary>
        /// Nếu status là Success thì yêu cầu thành công,Nếu status là Failure thì yêu cầu thất bại,errorCode hiển thị nguyên nhân thất bại.
        /// </summary>
        public string status { get; set; }
        public string errorCode { get; set; }
    }

    public class MeettingResponse
    {
        /// <summary>
        /// Là kết quả mã hóa đối xứng AES của tài khoản âm thanh/hình ảnh dùng Secret làm khóa, khi gọi dịch vụ âm thanh/hình ảnh cần dùng tới, vui lòng giải mã rồi dùng
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
          