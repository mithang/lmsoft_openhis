using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.Settlement
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysWardBedDmnService
    {
        /// <summary>
        /// Lấy tất cả danh sách, sửa đổi form
        /// </summary>
        List<SysWardBedVO> GetWardBedList(int? cwId, string orgId = null, string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        IList<SysWardBedVO> GetPagintionList(string orgId, Pagination pagination, string keyword = null);

    }
}
