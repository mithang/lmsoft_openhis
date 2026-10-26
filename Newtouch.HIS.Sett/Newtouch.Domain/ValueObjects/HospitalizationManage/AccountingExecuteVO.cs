using System;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    /// <summary>
    /// Tra cứu kết quả thực hiện Dùng chung ngoại trú/nội trú
    /// </summary>
    public class AccountingExecuteVO
    {
        /// <summary>
        /// Bản ghi thực hiệnId
        /// </summary>
        public string zxjlId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// Tên bệnh nhân
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
        /// 
        /// </summary>
        public string zt { get; set; }

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

        /// <summary>
        /// Số lượng(Được quy đổi từ liều điều trị mỗi lần)
        /// </summary>
        public decimal? sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? je { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zlsmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? zlrq { get; set; }

        public string Creater { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string kflb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfzt { get; set; }
    }
}
