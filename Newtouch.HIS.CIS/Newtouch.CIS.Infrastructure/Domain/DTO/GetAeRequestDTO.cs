namespace Newtouch.Domain.DTO
{
    /// <summary>
    /// tậpthànhbiệnchứngluậnđiều trị
    /// </summary>
    public class GetAeRequestDto
    {
        /// <summary>
        /// Số khám ngoại trú nhậnkhám Số hiệu
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Bác sĩ Mã nhân viên:tức Bác sĩ Đăng nhập Hệ thốngcủa Người dùngtên
        /// </summary>
        public string jzys { get; set; }

        /// <summary>
        /// tổ chứckếtcấu trúc ID
        /// </summary>
        public string organizeId { get; set; }
    }
}
