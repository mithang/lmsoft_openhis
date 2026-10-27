using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysUserVDmnService : IScopedDependency
    {
        /// <summary>
        /// Kiểm chứng đăng nhập
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<BusResult<SysUserVEntity>> UserLogin(LoginBO login);
        /// <summary>
        /// Xác thực danh tính sau đăng nhập thành công
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="staffOrganizeId"></param>
        /// <returns></returns>
        Task<OperatorModel?> BuildLoginStatusOpr(string userId, string staffOrganizeId = null);
    }
}
