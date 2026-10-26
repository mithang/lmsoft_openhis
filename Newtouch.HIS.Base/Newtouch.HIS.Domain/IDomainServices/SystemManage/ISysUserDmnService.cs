using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// DmnService liên quan đến người dùng
    /// </summary>
    public interface ISysUserDmnService
    {
        /// <summary>
        /// Gửi tạo mới, cập nhật người dùng hệ thống
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="roleList"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysUserEntity userEntity, SysUserLogOnEntity userLogOnEntity, string keyValue);

        /// <summary>
        /// Lấy người dùng hệ thống (đăng nhập)
        /// </summary>
        /// <param name="topOrganizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysUserVO> GetPagintionList(Pagination pagination, string OrganizeId, string keyword = null);

        /// <summary>
        /// Lấy nhân viên hệ thống theo Id người dùng hệ thống (đăng nhập)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        SysStaffEntity GetStaffByUserId(string userId);

        /// <summary>
        /// Lấy thông tin nhân viên hệ thống
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        SysUserVO GetSysUserByUserId(string userId);

        /// <summary>
        /// Lấy danh sách người dùng thuộc vai trò
        /// </summary>
        /// <param name="roleId"></param>
        IList<RoleUserVO> GetPagintionUserListByRoleId(Pagination pagination, string gh, string name, string roleId, string topOrganizeId);

        /// <summary>
        /// Tạo người dùng mới
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userLogOnEntity"></param>
        void CreateUser(SysUserEntity userEntity, SysUserLogOnEntity userLogOnEntity);

        /// <summary>
        /// Lấy danh sách nhân viên hệ thống liên kết theo UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysStaffEntity> GetStaffListByUserId(string userId);

        /// <summary>
        /// Lấy VO nhân viên hệ thống theo OrganizeId (và có SysUser tương ứng)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysUserStaffVO> GetSatffVOListByOrg(string orgId);

        /// <summary>
        /// Lấy loại ngôn ngữ của người dùng
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetLanguageTypeByUserId(string userId);

        /// <summary>
        /// Lấy tất cả các vị trí theo Id nhân viên
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

        string GetYbdmByGh(string rygh);
    }
}
