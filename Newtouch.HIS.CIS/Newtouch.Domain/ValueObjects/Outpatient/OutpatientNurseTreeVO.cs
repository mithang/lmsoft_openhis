
namespace Newtouch.Domain.ValueObjects
{
    public class OutpatientNurseTreeVO
    {
        /// <summary>
        /// Khám bệnhID
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// Họ tên bệnh nhân
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Giới tính bệnh nhân
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// Tuổi bệnh nhân
        /// </summary>
        public string nlshow { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Tên khoa đăng ký khám
        /// </summary>
        public string ghksmc { get; set; }
    }
}
