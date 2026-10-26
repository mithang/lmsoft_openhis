using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysUserVDmnService : IScopedDependency
    {
        /// <summary>
        /// xác thực đăng nhập
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<BusResult<SysUserVEntity>> UserLogin(LoginBO login);
        /// <summary>
        /// xác thực danh tính đăng nhập thành công
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="staffOrganizeId"></param>
        /// <returns></returns>
        Task<OperatorModel?> BuildLoginStatusOpr(string userId, string staffOrganizeId = null);
    }
}
