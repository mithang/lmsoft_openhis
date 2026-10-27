using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    public interface IExceReportPrintDmnService
    {
        /// <summary>
        /// LấyKhu bệnhphát thuốcBệnh nhâncây
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        IList<InpWardPatTreeVO> GetPatTree(string orgId, string zyzt, string keyword);

        /// <summary>
        /// Tra cứuY lệnhChi tiết
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="zxsj"></param>
        /// <param name="yzlx"></param>
        /// <returns></returns>
         IList<ExecReportReportRightVO> GetExecDetailGridJson(string orgId, string zyh, DateTime zxsj,string zxdlb,string yzxz);
        IList<ExecReportReportRightVO> QueryExecDetailGridJson(Pagination pagination, string orgId, string zyh, DateTime zxsj, DateTime zxsjend, string zxdlb,string yzxz);

        IList<InpWardPatTreeVO> GetPatCenterTree(string orgId, string zyzt, string keyword);
    }
}
