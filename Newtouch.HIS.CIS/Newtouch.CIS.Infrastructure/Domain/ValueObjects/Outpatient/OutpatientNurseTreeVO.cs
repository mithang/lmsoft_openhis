
namespace Newtouch.Domain.ValueObjects
{
    public class OutpatientNurseTreeVO
    {
        /// <summary>
        /// Khám bệnh ID
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Bệnh nhân Giới tính
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// Bệnh nhân Tuổi
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
        /// đăng ký khám Tên khoa
        /// </summary>
        public string ghksmc { get; set; }
    }
}
