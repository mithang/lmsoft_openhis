using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Menu hệ thống,Nút menu DmnService
    /// </summary>
    public interface ISysModuleDmnService
    {
        /// <summary>
        /// Theo vai trò Lấy Của cấu hình Quyền Menu
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IList<SysModuleEntity> GetMenuListByTopOrg(string topOrgId, IList<string> roleIdList, bool isRoot = false, bool isAdministrator = false);
        
        /// <summary>
        /// Lấy menu hệ thống grid
        /// </summary>
        /// <returns></returns>
        IList<SysModuleVO> GetMenuListByTopOrg(string topOrgId);
        
        /// <summary>
        /// Lấy cơ sở hiện tại Đã mở Menu của
        /// </summary>
        /// <returns></returns>
        IList<SysModuleEntity> GetOpenMenuListByTopOrg(string topOrgId);

        /// <summary>
        /// Gửi tạo mới,Cập nhật Menu
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysModuleEntity moduleEntity, string keyValue, string topOrgId = null);
        
        /// <summary>
        /// (Menu,Nút menu)Lấy danh sách tổ chức đã ủy quyền ứng dụng
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        IList<SysOrganizeVEntity> GetAuthedOrgListByModuleId(string moduleId, int itemType);

        /// <summary>
        /// (Menu,Nút menu)Ủy quyền menu cho cơ sở được chỉ định
        /// </summary>
        /// <param name="moduleId"></param>
        void UpdateAuthOrganizeList(string moduleId, string orgList, int itemType);

        /// <summary>
        /// (Menu,Nút menu)Ủy quyền menu cho tất cả các cơ sở
        /// </summary>
        /// <param name="moduleId"></param>
        void AuthAllOrganize(string moduleId, int itemType);

        /// <summary>
        /// (Menu,Nút menu)Hủy toàn bộ ủy quyền(Tổ chức)
        /// </summary>
        /// <param name="moduleId"></param>
        void AuthCancelAllOrganize(string moduleId, int itemType);



        /// <summary>
        /// Xem người dùng có thể truy cập hệ thống này hay không(Xác định theo menu quyền được mở cho người đó)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="topOrgId"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        bool CheckLoginable(string topOrgId, string account);

    }
}
