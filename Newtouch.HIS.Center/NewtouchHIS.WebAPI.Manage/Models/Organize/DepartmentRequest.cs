namespace NewtouchHIS.WebAPI.Manage.Models.Organize
{
    /// <summary>
    /// 科室信息
    /// </summary>
    public class DepartmentVO
    {
        /// <summary>
        /// 科室Mã
        /// </summary>
        public string? ks { get; set; }
        /// <summary>
        /// 科室名称
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
