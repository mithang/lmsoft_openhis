using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysDepartmentWardRelationRepo : IRepositoryBase<SysDepartmentWardRelationEntity>
    {
        /// <summary>
        /// Tìm thông tin liên kết khu bệnh của khoa
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        IList<SysDepartmentWardRelationEntity> GetDeptWardList(string deptId);
    }
}
