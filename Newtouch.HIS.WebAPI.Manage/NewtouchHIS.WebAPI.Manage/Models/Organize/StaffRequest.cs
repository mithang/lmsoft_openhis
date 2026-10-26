namespace NewtouchHIS.WebAPI.Manage.Models.Organize
{
    /// <summary>
    /// thông tin nhân viên
    /// </summary>
    public class StaffVO
    {
        /// <summary>
        /// số hiệu nhân viên
        /// </summary>
        public string? ysgh { get; set; }

        /// <summary>
        /// tên nhân viên
        /// </summary>
        public string? xm { get; set; }


    }
    public class SysDoctorVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string organizeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ysxm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// số hiệu bác sĩ
        /// </summary>
        public string ysgh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DutyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dutyName { get; set; }
    }
    /// <summary>
    /// lớp truy vấn thông tin nhân viên
    /// </summary>
    public class StaffRequest
    {
        public string? ks { get; set; }
        /// <summary>
        /// số hiệu bác sĩ
        /// </summary>
        public string? ysgh { get; set; }
        /// <summary>
        /// họ tên bác sĩ
        /// </summary>
        public string? ysxm { get; set; }
    }
}
