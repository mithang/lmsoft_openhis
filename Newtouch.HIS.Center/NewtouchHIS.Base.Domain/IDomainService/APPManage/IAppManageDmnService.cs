using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    public interface IAppManageDmnService : IScopedDependency
    {
        /// <summary>
        /// Lấy danh sách ứng dụng đã đăng ký
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<AuthAppVO>> GetRegAppList();
        /// <summary>
        /// Lấy thông tin đăng ký ứng dụng
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AuthAppVO> GetAppInfo(AppFriendAuthKeyRequest request);
        /// <summary>
        /// Tạo theo thông tin ủy quyền ứng dụngkey
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> AppKeyGen(AppFriendAuthKeyRequest request);
        /// <summary>
        /// Thông tin ủy quyền bạn bè của ứng dụng
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AppFriendAuthKey> GetAppFriend(AppFriendAuthKeyRequest request);
        #region Thao tác thực thể
        Task<SysAuthAppEntity> AddAuthApp(AuthAppVO authApp);
        Task<bool> DelAuthApp(AuthAppVO authapp);
        #endregion
    }
}
