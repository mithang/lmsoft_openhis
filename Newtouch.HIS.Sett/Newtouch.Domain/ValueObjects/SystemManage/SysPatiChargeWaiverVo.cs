
using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class SysPatiChargeWaiverVo
    {
        public int brsfjmbh { get; set; }
        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// Loại lớn
        /// </summary>
        public string dl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal jmbl { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

    }
}
