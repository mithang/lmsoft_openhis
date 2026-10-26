using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysDepartmentRepo : IRepositoryBase<SysDepartmentVEntity>
    {
        /// <summary>
        /// lấy cơ quan khoa danh sách
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysDepartmentVEntity> GetList(string orgId);

        /// <summary>
        /// theo Code lấy tên
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetNameByCode(string code, string orgId);
    }
}
