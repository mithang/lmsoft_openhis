using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public interface IEchartsDmnService
    {
        object PerformanceMonthIndicator(object p0, object p1, object p2);
        object SelectPerformanceIndicator(object p0, object p1);
    }

    public interface IReportCommonDmnService
    {
        object GetDlMc(object p0);
        object GetJyjlData(object p0, object p1, object p2, object p3);
        object GetJyjlDatatotxt(object p0, object p1, object p2);
        object GetMjzdbghData(object p0, object p1, object p2, object p3);
        object GetMjzjcsfData(object p0, object p1, object p2, object p3);
        object GetMjzjcsfDatatxt(object p0, object p1, object p2);
        object GetMonthReportRealTimeData(object p0, object p1, object p2);
        List<SFDLReportVO> GetSFDLCodeSelectJson(object p0);
        object GetYbMjzdbghtxt(object p0, object p1, object p2);
        object Getsfxm(object p0, object p1, object p2, object p3, object p4);
        object SubmitMonthReportData(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9);
        object turnInFee(object p0, object p1, object p2, object p3);
        IReportCommonDmnService_yBSBMX_DCdbfsDto yBSBMX_DCdbfs(object p0, object p1, object p2, object p3);
    }

    public interface IRptrptMzRjbRepo
    {
        RptrptMzRjbEntity GetLastMzrjEntity(object p0, object p1);
        object GetLastMzrjEntityList(object p0, object p1, object p2);
    }
}
