using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure.Model;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Liên quan người dùngDmnService
    /// </summary>
    public interface ISysUserExDmnService
    {
        /// <summary>
        /// Lấy nhà thuốc người dùng có thể thao tác（Kho thuốc）Danh sách
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVEntity> GetUserPharmacyDepartmentList(string orgId, string userId);

        /// <summary>
        /// Kiểm tra tên người dùng mật khẩu
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        SysUserVEntity CheckLogin(string username, string password);

        /// <summary>
        /// TheoUserIdLấy danh sách nhân sự hệ thống liên kết
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysStaffVEntity> GetStaffListByUserId(string userId);

        /// <summary>
        /// Cập nhật vai trò người dùng
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="roleList"></param>
        void UpdateUserRole(string keyValue, string[] roleList);

        /// <summary>
        /// TheoOrganizeIdLấy Nhân sự hệ thốngVO（và có tương ứngSysUser）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysUserStaffVO> GetSatffVOListByOrg(string orgId);

        /// <summary>
        /// Lấy danh sách nhân sự của tổ chức
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysStaffVEntity> GetStaffListByOrg(string orgId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<string> GetYfbmCodeListByUserId(string userId);

        /// <summary>
        /// Lấy loại ngôn ngữ người dùng
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetLanguageTypeByUserId(string userId);

        /// <summary>
        /// Theo cấp chaOrgId（Cây đệ quy）Lấy Tất cả Chỉ định Chức vụ của StaffId
        /// </summary>
        /// <param name="parentOrgId"></param>
        /// <param name="dutyCode"></param>
        /// <returns></returns>
        List<string> GetStaffIdListByParentOrg(string parentOrgId, string dutyCode);

        /// <summary>
        /// Theo nhân viênIdLấy tất cả vị trí
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="StaffId"></param>
        /// <returns></returns>
        IList<string> GetDutyListByStaffId(string orgId, string StaffId);

        /// <summary>
        /// Đặt lại mật khẩu
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="keyValue"></param>
        void RevisePassword(string userPassword, string userId);

        /// <summary>
        /// TheoUserIdLấy bộ phận nhà thuốc mà người dùng hệ thống có thể thao tácCode List
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<string> GetYfbmCodeListByUserId(string userId, string orgId);

        /// <summary>
        /// TheoUserIdLấy bộ phận nhà thuốc mà người dùng hệ thống có thể thao tác List
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<LoginUserCurrentYfbmModel> GetYfbmListByUserId(string userId, string orgId);

        IList<SysMSGQueryVO> MSGQuery(string yfbmcode,string orgid,int gqyj,string kcyj);
    }
}
