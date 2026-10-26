namespace Newtouch.Domain.DTO
{
    /// <summary>
    /// Tích hợp biện chứng luận trị
    /// </summary>
    public class GetAeRequestDto
    {
        /// <summary>
        /// Số ngoại trú Số tiếp nhận khám
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Mã nhân viên bác sĩ:Tức là tên đăng nhập hệ thống của bác sĩ
        /// </summary>
        public string jzys { get; set; }

        /// <summary>
        /// Cơ cấu tổ chứcID
        /// </summary>
        public string organizeId { get; set; }
    }
}
