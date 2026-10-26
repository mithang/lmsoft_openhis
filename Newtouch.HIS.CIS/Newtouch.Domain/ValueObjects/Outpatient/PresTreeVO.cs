namespace Newtouch.Domain.ValueObjects
{
    public class MedicalRecordTreeVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string blmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int jzzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int lsblly { get; set; }

        /// <summary>
        /// Tên khoa đăng ký khám
        /// </summary>
        public string ghksmc { get; set; }

        /// <summary>
        /// Bác sĩ khám cuối cùng
        /// </summary>
        public string jzysmc { get; set; }
    }
}
