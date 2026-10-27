namespace NewtouchHIS.WebAPI.Manage.Models
{
    public class LisReportRequestBase
    {
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// Đăng ký单状态 EnumReportStu
        /// </summary>
        public string? sqdzt { get; set; }
        /// <summary>
        /// Đăng ký单号
        /// </summary>
        public string? sqdh { get; set; }
        /// <summary>
        /// 项目代码
        /// </summary>
        public string? xmdm { get; set; }
        /// <summary>
        /// Loại nghiệp vụ 1 门诊 2 住院
        /// </summary>
        public string? ywlx { get; set; }
    }
    /// <summary>
    /// 门诊报告
    /// </summary>
    public class LisReportMzRequest:LisReportRequestBase
    {
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string? mzh { get; set; }
    }
    /// <summary>
    /// 住院报告
    /// </summary>
    public class LisReportZyRequest: LisReportRequestBase
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string? zyh { get; set; }
    }
}
