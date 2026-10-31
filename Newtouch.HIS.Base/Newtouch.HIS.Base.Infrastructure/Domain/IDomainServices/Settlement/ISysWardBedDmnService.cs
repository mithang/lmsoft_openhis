using Newtouch.HIS.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.Settlement
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysWardBedDmnService
    {
        /// <summary>
        /// Lấy tất cả danh sách,Sửaform
        /// </summary>
        List<SysWardBedVO> GetWardBedList(int? cwId, string orgId = null, string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        IList<SysWardBedVO> GetPagintionList(string orgId, Pagination pagination, string keyword = null);

    }
}
