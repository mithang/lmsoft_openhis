using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public class DBFField
    {
    }

    public class DBFWriter
    {
        public object CharEncoding { get; set; }
        public object Fields { get; set; }
        public void WriteRecord() { }
    }

    public class HZBK_Query
    {
    }

    public class JMMJZ_Query
    {
    }

    public class JMZY_Query
    {
    }

    public class MZYD_Query
    {
    }

    public class MonthReportMajorCateStatisticsVO
    {
    }

    public class RptrptMzRjbEntity
    {
        public string Id { get; set; }
        public DateTime kssj { get; set; }
        public DateTime jssj { get; set; }
    }

    public class YBSBMX
    {
    }

    public class ZGDBMZ_Qury
    {
    }

    public class ZGMJZTS_Query
    {
    }

    public class ZGMJZ_Query
    {
    }

    public class ZGZY_Query
    {
    }

    public class ZYYD_Query
    {
    }

    public enum NativeDbType
    {
        Char = 0,
        Numeric = 1,
    }

}