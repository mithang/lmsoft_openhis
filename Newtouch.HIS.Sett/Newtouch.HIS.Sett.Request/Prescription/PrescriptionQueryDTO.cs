using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// Đ ơn thuốc
    /// </summary>
    public class PrescriptionQueryDTO
    {
        /// <summary>
        /// Loại đ ơn thuốc Tư ơng ứng enum enumPrescriptionType
        /// </summary>
        [Required]
        public int cflx { get; set; }

        /// <summary>
        /// Số đ ơn thuốc
        /// </summary>
        [Required]
        public string cfh { get; set; }

    }
}
