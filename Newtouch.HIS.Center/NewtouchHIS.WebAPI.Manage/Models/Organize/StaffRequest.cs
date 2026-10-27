namespace NewtouchHIS.WebAPI.Manage.Models.Organize
{
    /// <summary>
    /// Thông tin công nhân viên chức
    /// </summary>
    public class StaffVO
    {
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string? ysgh { get; set; }

        /// <summary>
        /// Tên nhân viên
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
        /// Mã nhân viên bác sĩ
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
    /// Lớp truy vấn thông tin nhân viên
    /// </summary>
    public class StaffRequest
    {
        public string? ks { get; set; }
        /// <summary>
        /// Mã nhân viên bác sĩ
        /// </summary>
        public string? ysgh { get; set; }
        /// <summary>
        /// Bác sĩHọ tên
        /// </summary>
        public string? ysxm { get; set; }
    }
}
