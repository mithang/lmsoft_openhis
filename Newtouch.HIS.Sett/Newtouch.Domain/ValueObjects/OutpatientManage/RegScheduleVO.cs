
using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    [NotMapped]
    public class RegScheduleVO: OutpatientRegistScheduleEntity
    {
        /// <summary>
        /// Tên hạng mục thu phí
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Tên bác sĩ
        /// </summary>
        public string rymc { get; set; }
        /// <summary>
        /// Mã nhân viên bác sĩ
        /// </summary>
        public string gh { get; set; }
        /// <summary>
        /// Pinyin khoa
        /// </summary>
        public string kspy { get; set; }
        /// <summary>
        /// Pinyin nhân viên
        /// </summary>
        public string rypy { get; set; }
        /// <summary>
        /// Pinyin hạng mục thu phí
        /// </summary>
        public string sfxmpy { get; set; }
        /// <summary>
        /// xt_ghzbBệnh chuyên khoa đăng ký khám
        /// </summary>
        public new string ghzb { get; set; }
        /// <summary>
        /// Tên bệnh chuyên khoa đăng ký khám
        /// </summary>
        public string ghzbmc { get; set; }
        /// <summary>
        /// Pinyin bệnh chuyên khoa đăng ký khám
        /// </summary>
        public string ghzbpy { get; set; }

        /// <summary>
        /// Tên hạng mục khám chữa bệnh
        /// </summary>
        public string zlxmmc { get; set; }

    }

	
}
