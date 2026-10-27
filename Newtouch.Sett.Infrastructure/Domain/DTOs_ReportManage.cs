using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public class IReportCommonDmnService_GetSFDLCodeSelectJsonDto
    {
    }

    public class IReportCommonDmnService_yBSBMX_DCdbfsDto
    {
        public List<JMMJZ_Query> jmmjzvo { get; set; }
        public List<JMZY_Query> jmzyvo { get; set; }
        public List<HZBK_Query> hzbkvo { get; set; }
        public List<MZYD_Query> ydmzvo { get; set; }
        public List<ZYYD_Query> ydzyvo { get; set; }
        public List<ZGDBMZ_Qury> zgdbmzvo { get; set; }
        public List<ZGMJZTS_Query> zgmjztsvo { get; set; }
        public List<ZGMJZ_Query> zgmjzvo { get; set; }
        public List<ZGZY_Query> zgzyvo { get; set; }
    }

    public class IRptrptMzRjbRepo_GetLastMzrjEntityDto
    {
        public object Id { get; set; }
        public object jssj { get; set; }
    }

    public class ISysUserDmnService_GetStaffByDutyCodeDto
    {
            public object StaffName { get; set; }
        public object Where { get; set; }
        public object ks { get; set; }
}

}