namespace NewtouchHIS.WebAPI.Manage.Models.Organize
{
    /// <summary>
    /// Thông tin khoa
    /// </summary>
    public class DepartmentVO
    {
        /// <summary>
        /// KhoaMã
        /// </summary>
        public string? ks { get; set; }
        /// <summary>
        /// Tên khoa
        /// </summary>
        public string? ksmc { get; set; }
    }
    public class DepartmentRequest: DepartmentVO
    {

    }

    public class DepartmentResponse: DepartmentVO
    {

    }
}
