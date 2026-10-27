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
        /// Lấy danh sách khoa của cơ sở
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysDepartmentVEntity> GetList(string orgId);

        IList<SysDepartmentVEntity> GetList(string orgId, string zt, string keyword);

        /// <summary>
        /// TheoCodeLấy tên
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetNameByCode(string code, string orgId);
    }
}
