using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// Thông tin phòng khám（nhỏmàn hình）
    /// </summary>
    public class OutpatientConsultInfoVO
    {
        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Tên phòng khám
        /// </summary>
        public string zsmc { get; set; }
        /// <summary>
        /// Bác sĩHọ tên
        /// </summary>
        public string ysxm { get; set; }
        /// <summary>
        /// Bác sĩGiới thiệu
        /// </summary>
        public string ysjs { get; set; }
        /// <summary>
        /// Khám bệnhtrong
        /// </summary>
        public string jzz { get; set; }
        /// <summary>
        /// Khám bệnhtrongthứ tựsố
        /// </summary>
        public Int16? jzzxh { get; set; }
        /// <summary>
        /// chờKhám bệnh
        /// </summary>
        public string djz { get; set; }
        /// <summary>
        /// chờKhám bệnhthứ tựsố
        /// </summary>
        public Int16? djzxh { get; set; }
    }
}
