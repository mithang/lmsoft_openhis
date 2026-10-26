using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysStaffRepo : IRepositoryBase<SysStaffEntity>
    {
        /// <summary>
        /// Lấy tất cả nhân viên hệ thống thuộc cơ sở (không bao gồm cơ sở con)
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        IList<SysStaffEntity> GetsatffListByOrg(string OrganizeId);

        /// <summary>
        /// Lấy danh sách Id người quản lý theo UserId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IList<string> GetCurStaffIdListByUserId(string UserId);
        IList<SysStaffEntity> GetStaffList(string orgId, string keyword);
    }
}
