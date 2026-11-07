using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysUserApp
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        SysUserEntity CheckLogin(string username, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="keyValue"></param>
        void RevisePassword(string userPassword, string keyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEntity"></param>
        void UpdateUserZT(SysUserEntity userEntity);

        /// <summary>
        /// Tạo một quản trị viên hệ thống cho tổ chức admin
        /// </summary>
        /// <param name="userPassword"></param>
        void CreateDefaultAdminToOrg(string topOrgId, string userPassword);

        /// <summary>
        /// Microsoft SSO Đăng nhập(Email)
        /// </summary>
        /// <param name="userId">Người dùng ID</param>
        /// <param name="organizeId">Cơ sở ID</param>
        /// <returns></returns>
        SysUserEntity MsSsoLogin(string userId, string organizeId);
    }

}
