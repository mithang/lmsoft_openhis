using System;

namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    public class SysCIRemindContentVO
    {
        public int sfxjsnrbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzjsnr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zyjsnr { get; set; }

        /// <summary>
        /// 0 Không nhắc 1 Nhắc nhở thông thường 2 Cảnh báo 3 Cấm sử dụng
        /// </summary>
        public byte mzjsjb { get; set; }

        /// <summary>
        /// 0 Không nhắc 1 Nhắc nhở thông thường 2 Cảnh báo 3 Cấm sử dụng
        /// </summary>
        public byte zyjsjb { get; set; }

        /// <summary>
        /// 
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

        public string sfxm { get; set; }

        public string sfxmmc { get; set; }
    }
}
