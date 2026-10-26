using System;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    /// <summary>
    /// Tra cứu y lệnh phẫu thuậtGrid View
    /// </summary>
    public class ArrangeQueryGridVO
    {
        public string yzlb { get; set; }
        public string Id { get; set; }
        public int yzlx { get; set; }
        public string zyh { get; set; }
        public string xm { get; set; }
        public string kssj { get; set; }
        public string ysmc { get; set; }
        public string yznr { get; set; }
        public int? zh { get; set; }
        public DateTime? tzsj { get; set; }
        public string tzr { get; set; }
        public string zxr { get; set; }
        public int yzzt { get; set; }
        public DateTime? shsj { get; set; }
        public DateTime? zxsj { get; set; }
        public string ztmc { get; set; }

        /// <summary>
        /// Họ tên bác sĩ phẫu thuật
        /// </summary>
        public string surgeonName { get; set; }

        /// <summary>
        /// Địa chỉ phẫu thuật
        /// </summary>
        public string ssAddr { get; set; }

        /// <summary>
        /// Ngày phẫu thuật
        /// </summary>
        public DateTime? aprq { get; set; }
    }
}
