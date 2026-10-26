using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    public interface ISysDepartmentRepo : IRepositoryBase<SysDepartmentVEntity>
    {
        /// <summary>
        /// Lấy danh sách khoa
        /// </summary>
        /// <param name="orgId">Cơ sở y tế</param>
        /// <param name="zt">Cờ hiệu lực.0 Không hiệu lực 1 Hiệu lực null,Empty Tất cả</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDepartmentVEntity> GetList(string orgId, string zt = null, string keyword = null);

        /// <summary>
        /// Theo Code Lấy tên khoa
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetNameByCode(string code, string orgId);

        /// <summary>
        /// Theo Code Lấy thực thể khoa
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysDepartmentVEntity GetEntityByCode(string code, string orgId);

        /// <summary>
        /// Theo Name Lấy Code
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetCodeByNameFirstOrDefault(string name, string orgId);
    }
}