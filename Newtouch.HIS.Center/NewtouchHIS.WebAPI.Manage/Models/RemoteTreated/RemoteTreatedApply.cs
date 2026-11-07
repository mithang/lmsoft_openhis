
namespace NewtouchHIS.WebAPI.Manage.Models
{
    /// <summary>
    /// Đăng ký Khám chữa bệnh từ xa
    /// </summary>
    public class TreatedApplyInfoRequest
    {
        /// <summary>
        /// Đăng ký Số giao dịch 
        /// Do bên thứ ba tạo Số duy nhất
        /// </summary>
        public string? sqlsh { get; set; }
        /// <summary>
        /// HIS Đăng ký khám chữa bệnh Id
        /// </summary>
        public string? ApplyId { get; set; }
        
    }

    public class TreatedApplyReponse
    {

        /// <summary>
        /// HIS Đăng ký khám chữa bệnh Id
        /// </summary>
        public string? ApplyId { get; set; }
        public string? sqr { get; set; }
        /// <summary>
        /// Đăng ký Số giao dịch 
        /// Do bên thứ ba tạo Số duy nhất
        /// </summary>
        public string? sqlsh { get; set; }
        /// <summary>
        /// Đăng ký Tổ chức
        /// </summary>
        public string? ApplyOrg { get; set; }
        /// <summary>
        /// Tổ chức Tên
        /// </summary>
        public string? ApplyOrgName { get; set; }
        /// <summary>
        /// Đăng ký Mã số nhân viên
        /// </summary>
        public string? sqrgh { get; set; }

    }
}
