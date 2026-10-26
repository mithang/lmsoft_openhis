namespace Newtouch.HIS.Proxy.guian.DTO.S02
{
    /// <summary>
    /// máy khách truyền vào ngư ời dùng tên mật khẩu tiến hành thân phận xác thựcquay lạichứng từmã, mật khẩu cầnMD5 mã hóa 32 vị chữ hoa response dto
    /// </summary>
    public class S02ResponseDTO
    {
        /// <summary>
        /// chứng từ đ ơn số 
        /// </summary>
        public string billCode { get; set; }

        /// <summary>
        /// ngư ời dùng thuộc tính 
        /// </summary>
        public UserProp userProp { get; set; }
    }

    /// <summary>
    /// ngư ời dùng thuộc tính 
    /// </summary>
    public class UserProp
    {
        /// <summary>
        /// ngư ời dùng ID
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// ngư ời dùng tên
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// ngư ời dùng tài khoản 
        /// </summary>
        public string userAccount { get; set; }

        /// <summary>
        /// Cơ quanID
        /// </summary>
        public string orgId { get; set; }

        /// <summary>
        /// Cơ quanTên
        /// </summary>
        public string orgName { get; set; }

        /// <summary>
        /// is Manager
        /// </summary>
        public string isManager { get; set; }

        /// <summary>
        /// mã hành chính 
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// dòng hành chínhtên
        /// </summary>
        public string areaName { get; set; }

        /// <summary>
        /// Cơ quan cấp bậc 
        /// </summary>
        public string orgLevel { get; set; }

    }
}
