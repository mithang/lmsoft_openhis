
namespace NewtouchHIS.WebAPI.Manage.Models
{
    /// <summary>
    /// Đăng ký远程诊疗
    /// </summary>
    public class TreatedApplyInfoRequest
    {
        /// <summary>
        /// Đăng ký流水号 
        /// 由第三方生成 唯一号
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
        /// Đăng ký流水号 
        /// 由第三方生成 唯一号
        /// </summary>
        public string? sqlsh { get; set; }
        /// <summary>
        /// Đăng kýTổ chức
        /// </summary>
        public string? ApplyOrg { get; set; }
        /// <summary>
        /// Tổ chức名称
        /// </summary>
        public string? ApplyOrgName { get; set; }
        /// <summary>
        /// Đăng ký人工号
        /// </summary>
        public string? sqrgh { get; set; }

    }
}
