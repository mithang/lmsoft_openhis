using System;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    public class AccoutingPlanDetailVO
    {

        public string zxId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Số ngoại trú/nội trú
        /// </summary>
        public string mzzyh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zll { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }

        public decimal sl { get; set; }
        /// <summary>
        /// Thời gian thực hiện
        /// </summary>
        public DateTime? zxsj { get; set; }
        public string kflb { get; set; }
        public string zls { get; set; }
        public string zlsgh { get; set; }
        /// <summary>
        /// Hiệu lực
        /// </summary>
        public string zt { get; set; }
        public decimal je { get; set; }
        /// <summary>
        /// Tính chất tự chi trả
        /// </summary>
        public string zfxz { get; set; }
        /// <summary>
        /// Ngày điều trị
        /// </summary>
        public DateTime? zlrq { get; set; }

        /// <summary>
        /// Thời gian tạo(Thời gian hệ thống thực hiện)
        /// </summary>
        public DateTime? CreateTime { get; set; }

    }
}
