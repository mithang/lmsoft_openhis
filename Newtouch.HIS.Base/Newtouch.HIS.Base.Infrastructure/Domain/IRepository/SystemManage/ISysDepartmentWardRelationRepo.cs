using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysDepartmentWardRelationRepo : IRepositoryBase<SysDepartmentWardRelationEntity>
    {
        /// <summary>
        /// Tìm thông tin gắn kết khoa - khu bệnh
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        IList<SysDepartmentWardRelationEntity> GetDeptWardList(string deptId);
    }
}
