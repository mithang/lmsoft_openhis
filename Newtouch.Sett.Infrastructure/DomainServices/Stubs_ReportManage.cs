using Newtouch.Sett.Domain;
using System;
using System.Collections.Generic;

namespace Newtouch.Sett.DomainServices
{
    public class EchartsDmnService : IEchartsDmnService
    {
        public object PerformanceMonthIndicator(object p0, object p1, object p2)
        {
            return null;
        }

        public object SelectPerformanceIndicator(object p0, object p1)
        {
            return null;
        }

    }

    public class ReportCommonDmnService : IReportCommonDmnService
    {
        public object GetDlMc(object p0)
        {
            return null;
        }

        public object GetJyjlData(object p0, object p1, object p2, object p3)
        {
            return null;
        }

        public object GetJyjlDatatotxt(object p0, object p1, object p2)
        {
            return null;
        }

        public object GetMjzdbghData(object p0, object p1, object p2, object p3)
        {
            return null;
        }

        public object GetMjzjcsfData(object p0, object p1, object p2, object p3)
        {
            return null;
        }

        public object GetMjzjcsfDatatxt(object p0, object p1, object p2)
        {
            return null;
        }

        public object GetMonthReportRealTimeData(object p0, object p1, object p2)
        {
            return null;
        }

        public List<SFDLReportVO> GetSFDLCodeSelectJson(object p0)
        {
            return new List<SFDLReportVO>();
        }

        public object GetYbMjzdbghtxt(object p0, object p1, object p2)
        {
            return null;
        }

        public object Getsfxm(object p0, object p1, object p2, object p3, object p4)
        {
            return null;
        }

        public object SubmitMonthReportData(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public object turnInFee(object p0, object p1, object p2, object p3)
        {
            return null;
        }

        public IReportCommonDmnService_yBSBMX_DCdbfsDto yBSBMX_DCdbfs(object p0, object p1, object p2, object p3)
        {
            // Demo DB has no insurance-declaration rows; empty lists let the
            // DBF export action return its empty-result JSON instead of NRE.
            return new IReportCommonDmnService_yBSBMX_DCdbfsDto
            {
                jmmjzvo = new List<JMMJZ_Query>(), jmzyvo = new List<JMZY_Query>(),
                hzbkvo = new List<HZBK_Query>(), ydmzvo = new List<MZYD_Query>(),
                ydzyvo = new List<ZYYD_Query>(), zgdbmzvo = new List<ZGDBMZ_Qury>(),
                zgmjztsvo = new List<ZGMJZTS_Query>(), zgmjzvo = new List<ZGMJZ_Query>(),
                zgzyvo = new List<ZGZY_Query>()
            };
        }

    }

    public class RptrptMzRjbRepo : IRptrptMzRjbRepo
    {
        public RptrptMzRjbEntity GetLastMzrjEntity(object p0, object p1)
        {
            return new RptrptMzRjbEntity();
        }

        public object GetLastMzrjEntityList(object p0, object p1, object p2)
        {
            return null;
        }

    }
}
