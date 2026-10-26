namespace Newtouch.HIS.Proxy.guian.DTO.S02
{
    /// <summary>
    /// 客户端 truyền 入 ngư ời dùng 名 mật khẩu tiến hành thân phận 认证quay lại票据mã, mật khẩu cầnMD5 mã hóa 32 vị 大写 response dto
    /// </summary>
    public class S02ResponseDTO
    {
        /// <summary>
        /// 票据 đ ơn số 
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
        /// dòng 政tên
        /// </summary>
        public string areaName { get; set; }

        /// <summary>
        /// Cơ quan cấp bậc 
        /// </summary>
        public string orgLevel { get; set; }

    }
}
