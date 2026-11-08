namespace Newtouch.Domain.DTO
{
    /// <summary>
    /// tập thành biện chứng luận điều trị
    /// </summary>
    public class GetAeRequestDto
    {
        /// <summary>
        /// Số khám ngoại trú nhận khám Số hiệu
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Bác sĩ Mã nhân viên:tức Bác sĩ Đăng nhập Hệ thống của Người dùng tên
        /// </summary>
        public string jzys { get; set; }

        /// <summary>
        /// tổ chức kết cấu trúc ID
        /// </summary>
        public string organizeId { get; set; }
    }
}
