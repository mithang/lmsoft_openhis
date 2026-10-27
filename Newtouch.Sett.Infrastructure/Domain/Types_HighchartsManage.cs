using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public class OutpatientVisitNumVO
    {
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

    public class VisitNumBO
    {
        public object InpatientList { get; set; }
        public object OutpatientList { get; set; }
    }

}