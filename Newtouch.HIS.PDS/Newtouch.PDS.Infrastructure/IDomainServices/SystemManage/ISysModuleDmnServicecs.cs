using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using SysOrganizeVEntity = Newtouch.HIS.Domain.Entity.SysOrganizeVEntity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysModuleDmnService
    {
        /// <summary>
        /// Theo vai trò Lấy đã cấu hình Quyền Thực đơn
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IList<SysModuleEntity> GetMenuListByTopOrg(string topOrgId, IList<string> roleIdList, bool isRoot = false, bool isAdministrator = false);

        /// <summary>
        /// Lấy thực đơn hệ thống
        /// </summary>
        /// <returns></returns>
        IList<SysModuleVO> GetMenuListByTopOrg(string topOrgId);

        /// <summary>
        /// Lấy thực đơn hệ thống Nút
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        IList<SysModuleButtonVO> GetMenuButtonListByTopOrg(string topOrgId, string moduleId);

        /// <summary>
        /// Lấy cơ sở hiện tại Đã mở thực đơn của
        /// </summary>
        /// <returns></returns>
        IList<SysModuleEntity> GetOpenMenuListByTopOrg(string topOrgId);

        /// <summary>
        /// Lấy cơ sở hiện tại Đã mở thực đơn của
        /// </summary>
        /// <returns></returns>
        IList<SysModuleButtonEntity> GetOpenMenuButtonListByTopOrg(string topOrgId);

        /// <summary>
        /// Gửi mới,Cập nhật Thực đơn
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysModuleEntity moduleEntity, string keyValue, string topOrgId = null);

        /// <summary>
        /// Gửi mới,Cập nhật Nút quyền
        /// </summary>
        /// <param name="moduleButtonEntity"></param>
        /// <param name="keyValue"></param>
        void ModuleButtonSubmitForm(SysModuleButtonEntity moduleButtonEntity, string keyValue, string topOrgId = null);


        /// <summary>
        /// (Thực đơn,Nút thực đơn)Lấy danh sách tổ chức ứng dụng đã ủy quyền
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        IList<SysOrganizeVEntity> GetAuthedOrgListByModuleId(string moduleId, int itemType);

        /// <summary>
        /// (Thực đơn,Nút thực đơn)Ủy quyền thực đơn cho cơ sở chỉ định
        /// </summary>
        /// <param name="moduleId"></param>
        void UpdateAuthOrganizeList(string moduleId, string orgList, int itemType);

        /// <summary>
        /// (Thực đơn,Nút thực đơn)Ủy quyền thực đơn cho tất cả cơ sở
        /// </summary>
        /// <param name="moduleId"></param>
        void AuthAllOrganize(string moduleId, int itemType);

        /// <summary>
        /// (Thực đơn,Nút thực đơn)Thu hồi tất cả ủy quyền(Tổ chức)
        /// </summary>
        /// <param name="moduleId"></param>
        void AuthCancelAllOrganize(string moduleId, int itemType);

    }
}
