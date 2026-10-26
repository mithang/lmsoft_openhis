using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.Entity;

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
        IList<SysDepartmentVEntity> GetList(string orgId, string keyword = null);

        /// <summary>
        /// TheoCodeLấy khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysDepartmentVEntity GetByCode(string code, string orgId);

        /// <summary>
        /// TheoNameLấyCode
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetCodeByName_FirstOrDefault(string name, string orgId);

        /// <summary>
        /// TheoCodeLấy tên
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetNameByCode(string code, string orgId);

    }
}
