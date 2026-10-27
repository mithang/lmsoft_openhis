namespace Newtouch.Domain.DTO
{
    /// <summary>
    /// tậpthànhbiệnchứngluậnđiều trị
    /// </summary>
    public class GetAeRequestDto
    {
        /// <summary>
        /// Số khám ngoại trú nhậnkhámSố hiệu
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Bác sĩMã nhân viên：tứcBác sĩĐăng nhậpHệ thốngcủaNgười dùngtên
        /// </summary>
        public string jzys { get; set; }

        /// <summary>
        /// tổ chứckếtcấu trúcID
        /// </summary>
        public string organizeId { get; set; }
    }
}
