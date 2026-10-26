using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Menu hệ thống, nút menu DmnService
    /// </summary>
    public interface ISysModuleDmnService
    {

        /// <summary>
        /// Lấy menu quyền đã cấu hình theo vai trò
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IList<SysModuleEntity> GetMenuListByTopOrg(string orgId, IList<string> roleIdList, bool isRoot = false, bool isAdministrator = false);

        /// <summary>
        /// Lấy menu hệ thống  grid
        /// </summary>
        /// <returns></returns>
        IList<SysModuleVO> GetMenuListByTopOrg(string topOrgId);

        /// <summary>
        /// Lấy nút menu hệ thống   grid
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        IList<SysModuleButtonVO> GetMenuButtonListByTopOrg(string topOrgId, string moduleId);

        /// <summary>
        /// Lấy menu đã mở của cơ sở hiện tại
        /// </summary>
        /// <returns></returns>
        IList<SysModuleEntity> GetOpenMenuListByTopOrg(string topOrgId);

        /// <summary>
        /// Lấy menu đã mở của cơ sở hiện tại
        /// </summary>
        /// <returns></returns>
        IList<SysModuleButtonEntity> GetOpenMenuButtonListByTopOrg(string topOrgId);

        /// <summary>
        /// Gửi tạo mới, cập nhật menu
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysModuleEntity moduleEntity, string keyValue);

        /// <summary>
        /// Gửi tạo mới, cập nhật nút quyền
        /// </summary>
        /// <param name="moduleButtonEntity"></param>
        /// <param name="keyValue"></param>
        void ModuleButtonSubmitForm(SysModuleButtonEntity moduleButtonEntity, string keyValue, string topOrgId = null);
        
        /// <summary>
        /// (Menu, nút menu) Lấy danh sách tổ chức đã được ủy quyền ứng dụng
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        IList<SysOrganizeEntity> GetAuthedOrgListByModuleId(string moduleId, int itemType);

        /// <summary>
        /// (Menu, nút menu) Ủy quyền menu cho cơ sở được chỉ định
        /// </summary>
        /// <param name="moduleId"></param>
        void UpdateAuthOrganizeList(string moduleId, string orgList, int itemType);

        /// <summary>
        /// (Menu, nút menu) Ủy quyền menu cho tất cả các cơ sở
        /// </summary>
        /// <param name="moduleId"></param>
        void AuthAllOrganize(string moduleId, int itemType);

        /// <summary>
        /// (Menu, nút menu) Thu hồi toàn bộ ủy quyền (tổ chức)
        /// </summary>
        /// <param name="moduleId"></param>
        void AuthCancelAllOrganize(string moduleId, int itemType);

        /// <summary>
        /// Xem người dùng có thể truy cập hệ thống này hay không (xác định theo menu quyền đã mở cho người đó)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="topOrgId"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        bool CheckLoginable(string topOrgId, string account);

        /// <summary>
        /// Lấy menu hệ thống grid
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
         IList<SysModuleEntity> GetMenuListByOrg(string orgId);
    }
}
