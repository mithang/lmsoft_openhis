namespace Newtouch.Sett.API.Models
{
    /// <summary>
    /// Request của POST api/Patient/OutPatientRegistrationQuery — đúng body mà
    /// CIS PatientListController.SelectNotYetTreateList gửi qua
    /// SiteSettAPIHelper (reqObj anonymous): zzhz, ksCode, ysgh, mjzbz,
    /// jiuzhenbz, keyword và pagination lồng trong. CIS KHÔNG gửi OrganizeId
    /// nên field này là optional: trống thì service dùng Top_OrganizeId cấu hình.
    /// rows/page/sidx/sord phẳng là fallback nếu caller gửi phân trang ở
    /// top-level thay vì lồng trong pagination.
    /// </summary>
    public class OutPatientRegistrationQueryRequest
    {
        public string OrganizeId { get; set; }
        public string zzhz { get; set; }
        public string ksCode { get; set; }
        public string ysgh { get; set; }
        public string mjzbz { get; set; }
        public string jiuzhenbz { get; set; }
        public string keyword { get; set; }
        public QueryPagination pagination { get; set; }
        public int? rows { get; set; }
        public int? page { get; set; }
        public string sidx { get; set; }
        public string sord { get; set; }
    }

    public class QueryPagination
    {
        public int? rows { get; set; }
        public int? page { get; set; }
        public string sidx { get; set; }
        public string sord { get; set; }
        public int? records { get; set; }
    }
}
