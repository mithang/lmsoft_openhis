using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// Khoa dược hệ thống
    /// </summary>
    public interface ISysPharmacyDepartmentApp
    {
        /// <summary>
        /// Lấy tổ chức được ủy quyền cho thuốc đó
        /// </summary>
        /// <param name="ypId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysPharmacyDepartmentVEntity> EmpowermentPharmacyDepartmentQuery(string ypId, string organizeId);

        /// <summary>
        /// Ủy quyền khoa dược
        /// </summary>
        /// <param name="ypId"></param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="epds"></param>
        /// <returns></returns>
        string SubmitEmpowermentPharmacyDepartment(int? ypId, string ypCode, string organizeId, string userCode,List<string> epds);
    }
}