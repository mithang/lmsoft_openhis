using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Quản lý nội trú>Tra cứu quyết toán nội trú 
    /// </summary>
    public class HospSettQueryGridVO
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

        public string xm { get; set; }

        public DateTime? jsksrq { get; set; }

        public DateTime? jsjsrq { get; set; }

        public decimal zje { get; set; }

        public string CreateTime { get; set; }

        public string CreatorCode { get; set; }

        public string jszt { get; set; }

        public string jsxz { get; set; }

        public string blh { get; set; }

        public string zybz { get; set; }
        public string nlshow { get; set; }
        public string ks { get; set; }
        public string zzys { get; set; }
        public string ryrq { get; set; }
        public string cyrq { get; set; }
        public string sfsj { get; set; }
        public decimal xj { get; set; }
        public decimal jz { get; set; }
        public string ryzd { get; set; }
        public string cyzd { get; set; }
        public string jslx { get; set; }
        public string czy { get; set; }
    }

    public class HospSettQueryReq
    {
        public string keyword { get; set; }

        public DateTime? jsksrq { get; set; }

        public DateTime? jsjsrq { get; set; }

        public string zybz { get; set; }
    }
}
