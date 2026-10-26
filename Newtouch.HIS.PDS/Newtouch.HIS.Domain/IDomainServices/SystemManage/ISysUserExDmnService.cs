using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure.Model;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// người dùng liên quan DmnService
    /// </summary>
    public interface ISysUserExDmnService
    {
        /// <summary>
        /// lấy người dùng có thể thao tác của nhà thuốc (kho thuốc)danh sách
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVEntity> GetUserPharmacyDepartmentList(string orgId, string userId);

        /// <summary>
        /// kiểm tra tên người dùng mật khẩu
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        SysUserVEntity CheckLogin(string username, string password);

        /// <summary>
        /// theo UserId lấy liên kết của hệ thống nhân viên danh sách
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysStaffVEntity> GetStaffListByUserId(string userId);

        /// <summary>
        /// cập nhật người dùng vai trò
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="roleList"></param>
        void UpdateUserRole(string keyValue, string[] roleList);

        /// <summary>
        /// theo OrganizeId lấy hệ thống nhân viên VO (và có tương ứng của SysUser)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysUserStaffVO> GetSatffVOListByOrg(string orgId);

        /// <summary>
        /// lấy tổ chức của nhân viên danh sách
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
        /// Lấy loại ngôn ngữ của người dùng
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetLanguageTypeByUserId(string userId);

        /// <summary>
        /// theo cấp cha OrgId (cây đệ quy) lấy tất cả chức vụ được chỉ định của StaffId
        /// </summary>
        /// <param name="parentOrgId"></param>
        /// <param name="dutyCode"></param>
        /// <returns></returns>
        List<string> GetStaffIdListByParentOrg(string parentOrgId, string dutyCode);

        /// <summary>
        /// theo viên công Id lấy tất cả các vị trí
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="StaffId"></param>
        /// <returns></returns>
        IList<string> GetDutyListByStaffId(string orgId, string StaffId);

        /// <summary>
        /// đặt lại mật khẩu
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="keyValue"></param>
        void RevisePassword(string userPassword, string userId);

        /// <summary>
        /// theo UserId lấy hệ thống người dùng có thể thao tác của bộ phận nhà thuốc Code List
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<string> GetYfbmCodeListByUserId(string userId, string orgId);

        /// <summary>
        /// theo UserId lấy hệ thống người dùng có thể thao tác của bộ phận nhà thuốc List
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<LoginUserCurrentYfbmModel> GetYfbmListByUserId(string userId, string orgId);

        IList<SysMSGQueryVO> MSGQuery(string yfbmcode,string orgid,int gqyj,string kcyj);
    }
}
