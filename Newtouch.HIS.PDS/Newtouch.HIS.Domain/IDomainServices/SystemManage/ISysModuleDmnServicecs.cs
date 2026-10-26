using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.Entity;
using SysOrganizeVEntity = Newtouch.HIS.Domain.Entity.SysOrganizeVEntity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysModuleDmnService
    {
        /// <summary>
        /// theo vai trò lấy cấu hình của quyền menu
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IList<SysModuleEntity> GetMenuListByTopOrg(string topOrgId, IList<string> roleIdList, bool isRoot = false, bool isAdministrator = false);

        /// <summary>
        /// lấy hệ thống menu
        /// </summary>
        /// <returns></returns>
        IList<SysModuleVO> GetMenuListByTopOrg(string topOrgId);

        /// <summary>
        /// lấy hệ thống menu nút
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        IList<SysModuleButtonVO> GetMenuButtonListByTopOrg(string topOrgId, string moduleId);

        /// <summary>
        /// lấy tổ chức hiện tại đã mở của menu
        /// </summary>
        /// <returns></returns>
        IList<SysModuleEntity> GetOpenMenuListByTopOrg(string topOrgId);

        /// <summary>
        /// lấy tổ chức hiện tại đã mở của menu
        /// </summary>
        /// <returns></returns>
        IList<SysModuleButtonEntity> GetOpenMenuButtonListByTopOrg(string topOrgId);

        /// <summary>
        /// gửi tạo mới, cập nhật menu
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysModuleEntity moduleEntity, string keyValue, string topOrgId = null);

        /// <summary>
        /// gửi tạo mới, cập nhật quyền nút
        /// </summary>
        /// <param name="moduleButtonEntity"></param>
        /// <param name="keyValue"></param>
        void ModuleButtonSubmitForm(SysModuleButtonEntity moduleButtonEntity, string keyValue, string topOrgId = null);


        /// <summary>
        ///  (menu, nút menu)lấy ứng dụng đã ủy quyền của tổ chức danh sách
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        IList<SysOrganizeVEntity> GetAuthedOrgListByModuleId(string moduleId, int itemType);

        /// <summary>
        /// (menu, nút menu) ủy quyền menu cho tổ chức được chỉ định
        /// </summary>
        /// <param name="moduleId"></param>
        void UpdateAuthOrganizeList(string moduleId, string orgList, int itemType);

        /// <summary>
        /// (menu, nút menu) ủy quyền menu cho tất cả các tổ chức
        /// </summary>
        /// <param name="moduleId"></param>
        void AuthAllOrganize(string moduleId, int itemType);

        /// <summary>
        ///  (menu, nút menu)thu hồi tất cả ủy quyền (tổ chức)
        /// </summary>
        /// <param name="moduleId"></param>
        void AuthCancelAllOrganize(string moduleId, int itemType);

    }
}
