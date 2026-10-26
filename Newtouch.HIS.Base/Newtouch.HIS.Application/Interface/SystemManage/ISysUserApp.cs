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
        /// Tạo một quản trị viên hệ thống admin cho tổ chức
        /// </summary>
        /// <param name="userPassword"></param>
        void CreateDefaultAdminToOrg(string topOrgId, string userPassword);

        /// <summary>
        /// Đăng nhập SSO của Microsoft (email)
        /// </summary>
        /// <param name="userId">ID người dùng</param>
        /// <param name="organizeId">ID tổ chức</param>
        /// <returns></returns>
        SysUserEntity MsSsoLogin(string userId, string organizeId);
    }

}
