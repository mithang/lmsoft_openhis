using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysStaffDmnService
    {
        /// <summary>
        /// Lấy nhân viên hệ thống Danh sách phân trang(Không bao gồm tổ chức con)
        /// </summary>
        /// <param name="topOrganizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysStaffVO> GetPaginationList(Pagination pagination, string OrganizeId, string keyword = null);

        /// <summary>
        /// Thêm,Cập nhật Nhân viên hệ thống
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <param name="asLoginUser">Làm người dùng đăng nhập hệ thống(Chỉ khi tạo)</param>
        void SubmitForm(SysStaffEntity entity, string keyValue, bool asLoginUser,out string staffId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="dutyIdList"></param>
        void UpdateStaffDuty(string staffId, string[] dutyIdList);

        void UpdateStaffWard(string staffId, string[] wardList,string OrganizeId);

        SysStaffEntity GetSysStaff(string msEmaile, string organizeId);

        SysUserStaffEntity GetUserStaff(string staffId);

        SysStaffEntity GetUserStaffByUserId(string userId, string organizeId);
        void UpdateStaffConsult(string staffId, string zsCode, string OrganizeId);

    }
}
