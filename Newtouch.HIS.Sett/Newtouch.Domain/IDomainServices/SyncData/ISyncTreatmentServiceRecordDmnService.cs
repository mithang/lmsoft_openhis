using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Đồng bộ hồ sơ điều trị
    /// </summary>
    public interface ISyncTreatmentServiceRecordDmnService
    {
        /// <summary>
        /// Lấy hồ sơ điều trị đồng bộ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="clzt"></param>
        /// <param name="mzh"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<SyncTreatmentServiceRecordVO> GetList(string orgId, int? clzt = 1, bool? wtjlbz = false, string mzh = "", string zyh = "", string zlsgh = "");

    }
}
