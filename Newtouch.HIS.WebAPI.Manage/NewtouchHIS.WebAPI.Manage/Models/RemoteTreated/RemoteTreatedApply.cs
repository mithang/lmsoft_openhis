
namespace NewtouchHIS.WebAPI.Manage.Models
{
    /// <summary>
    /// đăng ký khám chữa bệnh từ xa
    /// </summary>
    public class TreatedApplyInfoRequest
    {
        /// <summary>
        /// số thứ tự đăng ký 
        /// do bên thứ ba tạo ra số duy nhất
        /// </summary>
        public string? sqlsh { get; set; }
        /// <summary>
        /// HIS đăng ký khám chữa bệnhId
        /// </summary>
        public string? ApplyId { get; set; }
        
    }

    public class TreatedApplyReponse
    {

        /// <summary>
        /// HIS đăng ký khám chữa bệnhId
        /// </summary>
        public string? ApplyId { get; set; }
        public string? sqr { get; set; }
        /// <summary>
        /// số thứ tự đăng ký 
        /// do bên thứ ba tạo ra số duy nhất
        /// </summary>
        public string? sqlsh { get; set; }
        /// <summary>
        /// cơ quan đăng ký
        /// </summary>
        public string? ApplyOrg { get; set; }
        /// <summary>
        /// tên cơ quan
        /// </summary>
        public string? ApplyOrgName { get; set; }
        /// <summary>
        /// số hiệu nhân viên đăng ký
        /// </summary>
        public string? sqrgh { get; set; }

    }
}
