using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public class Inparameter
    {
        public DateTime? ksrq { get; set; }
        public DateTime? jsrq { get; set; }
        public string keywordo { get; set; }
        public string keywordt { get; set; }
    }

    public class GhJzInfo
    {
        public int ghnm { get; set; }
        public string mzh { get; set; }
        public string blh { get; set; }
        public string xm { get; set; }
        public string zjh { get; set; }
        public string xb { get; set; }
        public string brxz { get; set; }
        public string ghksmc { get; set; }
        public string ghks { get; set; }
        public string ghysmc { get; set; }
        public string ghys { get; set; }
        public DateTime? ghrq { get; set; }
        public string ghzt { get; set; }
        public string jzzt { get; set; }
        public DateTime? jzrq { get; set; }
        public string jzksmc { get; set; }
        public string jzks { get; set; }
        public string jzysmc { get; set; }
        public string jzys { get; set; }
        public string fph { get; set; }
        public string syth { get; set; }
        public string thys { get; set; }
        public string thrq { get; set; }
    }

    public class OutPatientRegInfoVO
    {
        public int ghnm { get; set; }
        public string mzh { get; set; }
        public string blh { get; set; }
        public string xm { get; set; }
        public string kh { get; set; }
        public string CardType { get; set; }
        public int? patid { get; set; }
        public string ks { get; set; }
        public string ys { get; set; }
        public DateTime? ghrq { get; set; }
        public string ghzt { get; set; }
        public string jzbz { get; set; }
        public string brxz { get; set; }
        public string xb { get; set; }
        public string zjh { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public string OrganizeId { get; set; }
        public string ksmc { get; set; }
        public string ysmc { get; set; }
        public string brxzmc { get; set; }
        public string py { get; set; }
    }
    public class OutpatientPrescriptionEntity
    {
        public int cfnm { get; set; }
        public int cflx { get; set; }
        public string cfh { get; set; }
        public string cfly { get; set; }
    }
}
