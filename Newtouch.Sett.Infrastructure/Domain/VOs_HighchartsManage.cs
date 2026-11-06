using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    // Source-true: Newtouch.Domain/ValueObjects/InpatientVisitNumVO.cs.
    // (OutpatientVisitNumVO + VisitNumBO already exist in Types_HighchartsManage.cs
    // via gen-types; do NOT duplicate them here.)
    public class InpatientVisitNumVO
    {
        public int? num { get; set; }
    }

    // SHELL-A: source-true from vendor
    // Newtouch.Domain/BusinessObjects/VisitNumBO.cs (MonthVisitNumBO) and
    // Newtouch.Domain/ValueObjects/*.cs — needed by Home/Default's week chart.
    public class MonthVisitNumBO
    {
        public List<OutpatientWeekVisitNumVO> OutpatientList { get; set; }
        public List<OutpatientWeekVisitNumVO> InpatientList { get; set; }
    }

    public class OutpatientWeekVisitNumVO
    {
        public string wbegin { get; set; }
        public string wend { get; set; }
        public int num { get; set; }
    }

    public class LastWeekInfo
    {
        public string wbegin { get; set; }
        public string wend { get; set; }
    }

    public class InpatientVisitBasicVO
    {
        public string wbegin { get; set; }
        public string wend { get; set; }
        public string zyh { get; set; }
        public string zybz { get; set; }
        public DateTime ryrq { get; set; }
        public DateTime? cyrq { get; set; }
    }
}
