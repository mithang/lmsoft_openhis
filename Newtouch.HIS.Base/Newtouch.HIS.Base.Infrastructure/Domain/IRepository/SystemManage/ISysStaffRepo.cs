using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysStaffRepo : IRepositoryBase<SysStaffEntity>
    {
        /// <summary>
        /// Lấy tất cả nhân viên hệ thống thuộc tổ chức（Không bao gồm tổ chức con）
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        IList<SysStaffEntity> GetsatffListByOrg(string OrganizeId);

        /// <summary>
        /// Lấy nhân viên quản lýIdDanh sách TheoUserId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IList<string> GetCurStaffIdListByUserId(string UserId);
        IList<SysStaffEntity> GetStaffList(string orgId, string keyword);
    }
}
