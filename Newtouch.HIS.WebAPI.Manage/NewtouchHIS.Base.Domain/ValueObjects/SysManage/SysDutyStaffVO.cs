namespace NewtouchHIS.Base.Domain.ValueObjects
{
    public class SysDutyStaffVO
    {
        /// <summary>
        /// họ tên nhân viên
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// phiên âm nhân viên
        /// </summary>
        public string StaffPY { get; set; }

        /// <summary>
        /// số hiệu nhân viên
        /// </summary>
        public string StaffGh { get; set; }

        /// <summary>
        /// mã khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// phiên âm khoa
        /// </summary>
        public string kspy { get; set; }

        /// <summary>
        /// mã vị trí nhân viên
        /// </summary>
        public string DutyCode { get; set; }
    }

}
