namespace NewtouchHIS.WebAPI.Manage.Models
{
    public class LisReportRequestBase
    {
        /// <summary>
        /// số thẻ
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// trạng thái phiếu đăng ký EnumReportStu
        /// </summary>
        public string? sqdzt { get; set; }
        /// <summary>
        /// số phiếu đăng ký
        /// </summary>
        public string? sqdh { get; set; }
        /// <summary>
        /// mã hạng mục
        /// </summary>
        public string? xmdm { get; set; }
        /// <summary>
        /// loại nghiệp vụ 1 khám ngoại trú 2 nội trú
        /// </summary>
        public string? ywlx { get; set; }
    }
    /// <summary>
    /// báo cáo ngoại trú
    /// </summary>
    public class LisReportMzRequest:LisReportRequestBase
    {
        /// <summary>
        /// số khám ngoại trú
        /// </summary>
        public string? mzh { get; set; }
    }
    /// <summary>
    /// báo cáo nội trú
    /// </summary>
    public class LisReportZyRequest: LisReportRequestBase
    {
        /// <summary>
        /// số nội trú
        /// </summary>
        public string? zyh { get; set; }
    }
}
