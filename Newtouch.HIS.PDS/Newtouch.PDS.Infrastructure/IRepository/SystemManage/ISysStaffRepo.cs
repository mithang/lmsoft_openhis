using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysStaffRepo : IRepositoryBase<SysStaffVEntity>
    {
        /// <summary>
        /// Lấy tổ chức Danh sách nhân viên hiệu lực
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysStaffVEntity> GetStaffListByOrganizeId(string orgId);

        IList<SysStaffVEntity> GetValidStaffListByOrganizeId(string orgId);
    }
}
