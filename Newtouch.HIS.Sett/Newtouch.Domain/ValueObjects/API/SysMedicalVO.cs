using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.API
{
    public class SysMedicalVO
    {
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int sfxmId { get; set; }
        /// <summary>
        /// Mã hạng mục y tế
        /// </summary>
        public string sfxmCode { get; set; }
        /// <summary>
        /// Tên hạng mục y tế
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal dj { get; set; }
        /// <summary>
        /// Mã phân loại hạng mục
        /// </summary>
        public string sfdlCode { get; set; }
        /// <summary>
        /// Tên phân loại hạng mục
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// Mã y tế BHYT
        /// </summary>
        public string ybdm { get; set; }
        /// <summary>
        /// Tính chất tự chi trả(Cấp BHYT)  enum EnumZiFuXingZhi
        /// </summary>
        public string zfxz { get; set; }
    }
}
