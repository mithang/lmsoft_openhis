using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    /// <summary>
    /// Menu hệ thống (Giao diện thêm/xóa/sửa)
    /// </summary>
    public interface IRoleAuthorizeApp : IScopedDependency
    {
        /// <summary>
        /// Lấy danh sách ủy quyền vai trò
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<SysRoleAuthorizeEntity>> GetValidList(string roleId);
    }
}
