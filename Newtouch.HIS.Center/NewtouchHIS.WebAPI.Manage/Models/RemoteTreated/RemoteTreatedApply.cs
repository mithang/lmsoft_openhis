
namespace NewtouchHIS.WebAPI.Manage.Models
{
    /// <summary>
    /// Đăng kýKhám chữa bệnh từ xa
    /// </summary>
    public class TreatedApplyInfoRequest
    {
        /// <summary>
        /// Đăng kýSố giao dịch 
        /// Do bên thứ ba tạo Số duy nhất
        /// </summary>
        public string? sqlsh { get; set; }
        /// <summary>
        /// HIS Đăng ký khám chữa bệnhId
        /// </summary>
        public string? ApplyId { get; set; }
        
    }

    public class TreatedApplyReponse
    {

        /// <summary>
        /// HIS Đăng ký khám chữa bệnhId
        /// </summary>
        public string? ApplyId { get; set; }
        public string? sqr { get; set; }
        /// <summary>
        /// Đăng kýSố giao dịch 
        /// Do bên thứ ba tạo Số duy nhất
        /// </summary>
        public string? sqlsh { get; set; }
        /// <summary>
        /// Đăng kýTổ chức
        /// </summary>
        public string? ApplyOrg { get; set; }
        /// <summary>
        /// Tổ chứcTên
        /// </summary>
        public string? ApplyOrgName { get; set; }
        /// <summary>
        /// Đăng kýMã số nhân viên
        /// </summary>
        public string? sqrgh { get; set; }

    }
}
