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
        /// lấy tổ chức còn hiệu lực viên công danh sách
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysStaffVEntity> GetStaffListByOrganizeId(string orgId);
    }
}
