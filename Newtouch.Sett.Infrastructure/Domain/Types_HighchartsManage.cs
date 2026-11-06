using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    // SHELL-A (Sett Default dashboard): source-true fields from vendor
    // Newtouch.Domain/ValueObjects/OutpatientVisitNumVO.cs — the generated stub
    // had no properties, so the chart SQL mapped to empty objects.
    public class OutpatientVisitNumVO
    {
        public int? num { get; set; }
    }

    public class PatientMonthVisitPerThe
    {
        public object discharge { get; set; }
        public object visitper { get; set; }
    }

    public class PatientVisitPerThe
    {
        public object discharge { get; set; }
        public object visitper { get; set; }
    }

    // SHELL-A: source-true shape from vendor
    // Newtouch.Domain/BusinessObjects/VisitNumBO.cs (typed lists + zyrs). The
    // object-typed stub still accepted the Highcharts assignments, so this
    // only narrows the types to the vendor shape; no caller changes.
    public class VisitNumBO
    {
        public List<OutpatientVisitNumVO> OutpatientList { get; set; }
        public List<InpatientVisitNumVO> InpatientList { get; set; }
        public int zyrs { get; set; }
    }

}