using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public class NonTreatmentItemBillingEntity
    {
        public object OrganizeId { get; set; }
        public object blh { get; set; }
        public object cxjfbId { get; set; }
        public DateTime? cxrq { get; set; }
        public object cxry { get; set; }
        public object dj { get; set; }
        public object dlCode { get; set; }
        public object je { get; set; }
        public object jfbId { get; set; }
        public object jzrq { get; set; }
        public object patId { get; set; }
        public object px { get; set; }
        public object sfxmCode { get; set; }
        public object sl { get; set; }
        public object smksCode { get; set; }
        public object smry { get; set; }
        public object tjr { get; set; }
        public object xm { get; set; }
        public string zt { get; set; }
        public object zyh { get; set; }
        public void Create() { }
    }

    public class NonTreatmentItemBillingInfoVO
    {
        public object blh { get; set; }
        public decimal? dj { get; set; }
        public object dlCode { get; set; }
        public object jfbId { get; set; }
        public object jzrq { get; set; }
        public object patId { get; set; }
        public object sfxmCode { get; set; }
        public object smksCode { get; set; }
        public object smry { get; set; }
        public decimal? tje { get; set; }
        public object tjr { get; set; }
        public decimal? tsl { get; set; }
        public object xm { get; set; }
        public object zyh { get; set; }
    }

    public class TreeSelectModel
    {
        public object id { get; set; }
        public object parentId { get; set; }
        public object text { get; set; }
    }

}