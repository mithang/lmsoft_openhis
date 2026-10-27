namespace NewtouchHIS.WebAPI.Manage.Models
{
    public class LisReportRequestBase
    {
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// Đăng kýTrạng thái phiếu EnumReportStu
        /// </summary>
        public string? sqdzt { get; set; }
        /// <summary>
        /// Đăng kýSố phiếu
        /// </summary>
        public string? sqdh { get; set; }
        /// <summary>
        /// Mã hạng mục
        /// </summary>
        public string? xmdm { get; set; }
        /// <summary>
        /// Loại nghiệp vụ 1 Ngoại trú 2 Nội trú
        /// </summary>
        public string? ywlx { get; set; }
    }
    /// <summary>
    /// Báo cáo ngoại trú
    /// </summary>
    public class LisReportMzRequest:LisReportRequestBase
    {
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string? mzh { get; set; }
    }
    /// <summary>
    /// Báo cáo nội trú
    /// </summary>
    public class LisReportZyRequest: LisReportRequestBase
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string? zyh { get; set; }
    }
}
