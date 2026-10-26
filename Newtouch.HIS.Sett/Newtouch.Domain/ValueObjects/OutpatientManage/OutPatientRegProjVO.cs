
using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Hạng mục đăng ký khám
    /// </summary>
    [NotMapped]
    public class OutPatientRegProjVO: OutpatientRegistItemEntity
    {
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string rymc { get; set; }
        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Tên hạng mục thu phí
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// Loại lớn Bảo hiểm nông nghiệp
        /// </summary>
        public string nbdl { get; set; }
    }
}
