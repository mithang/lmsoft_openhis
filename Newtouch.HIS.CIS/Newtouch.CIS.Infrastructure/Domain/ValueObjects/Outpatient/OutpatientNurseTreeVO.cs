
namespace Newtouch.Domain.ValueObjects
{
    public class OutpatientNurseTreeVO
    {
        /// <summary>
        /// Khám bệnhID
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Bệnh nhânGiới tính
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// Bệnh nhânTuổi
        /// </summary>
        public string nlshow { get; set; }
        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// đăng ký khámTên khoa
        /// </summary>
        public string ghksmc { get; set; }
    }
}
