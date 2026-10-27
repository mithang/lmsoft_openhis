namespace NewtouchHIS.Base.Domain.ValueObjects
{
    public class SysDutyStaffVO
    {
        /// <summary>
        /// Nhân viênHọ tên
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// Phiên âm nhân viên
        /// </summary>
        public string StaffPY { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string StaffGh { get; set; }

        /// <summary>
        /// Mã khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Phiên âm khoa
        /// </summary>
        public string kspy { get; set; }

        /// <summary>
        /// Mã vị trí nhân viên
        /// </summary>
        public string DutyCode { get; set; }
    }

}
