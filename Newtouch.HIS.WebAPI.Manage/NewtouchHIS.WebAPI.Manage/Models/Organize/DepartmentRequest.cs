namespace NewtouchHIS.WebAPI.Manage.Models.Organize
{
    /// <summary>
    /// thông tin khoa
    /// </summary>
    public class DepartmentVO
    {
        /// <summary>
        /// mã khoa
        /// </summary>
        public string? ks { get; set; }
        /// <summary>
        /// tên khoa
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
