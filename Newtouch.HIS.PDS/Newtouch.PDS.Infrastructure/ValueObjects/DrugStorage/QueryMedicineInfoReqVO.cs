using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Điều kiện truy vấn phát thuốc nội bộ
    /// </summary>
    public class QueryMedicineInfoReqVO
    {
        public string begindate { get; set; }
        public string enddate { get; set; }
        public int jxbh { get; set; }
        public string yptssx { get; set; }
        public string Pdh { get; set; }
        public string shbm { get; set; }
        public string yplb { get; set; }
        public string djlx { get; set; }
    }
}
