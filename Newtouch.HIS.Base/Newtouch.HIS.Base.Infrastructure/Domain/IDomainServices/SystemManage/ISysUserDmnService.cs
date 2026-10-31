using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Liên quan người dùngDmnService
    /// </summary>
    public interface ISysUserDmnService
    {
        /// <summary>
        /// Gửi tạo mới,Cập nhật Người dùng hệ thống
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="roleList"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysUserEntity userEntity, SysUserLogOnEntity userLogOnEntity, string keyValue);

        /// <summary>
        /// Lấy hệ thống(Đăng nhập)Người dùng
        /// </summary>
        /// <param name="topOrganizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysUserVO> GetPagintionList(Pagination pagination, string OrganizeId, string keyword = null);

        /// <summary>
        /// Theo hệ thống(Đăng nhập)Người dùngId Lấy Nhân viên hệ thống
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
        /// Lấy thuộc về vai trò của Danh sách người dùng
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
        /// TheoUserIdLấy danh sách nhân viên hệ thống liên kết
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysStaffEntity> GetStaffListByUserId(string userId);

        /// <summary>
        /// TheoOrganizeIdLấy Nhân viên hệ thốngVO(và có tương ứngSysUser)
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

        string GetYbdmByGh(string rygh);
    }
}
