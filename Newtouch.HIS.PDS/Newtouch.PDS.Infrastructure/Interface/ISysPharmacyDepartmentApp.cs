using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.PDS.Requset.PharmacyDepartment;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPharmacyDepartmentApp
    {

        /// <summary>
        /// Lấy kho thuốc theo mã nhà thuốcList（Nhà thuốc có thể lĩnh từ kho thuốc đích）
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVEntity> GetTheUpperYkbmCodeList(string keyword, string yfbmCode, string organizeId);

        /// <summary>
        /// Lấy nhà thuốc theo mã kho thuốcList（Kho thuốc có thể phát thuốc cho nhà thuốc đích）
        /// </summary>
        /// <param name="ykbmCode"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVEntity> GetTheLowerYfbmCodeList(string ykbmCode);

        /// <summary>
        /// Lấy khoa theo mã nhà thuốcList（Nhà thuốc có thể phát thuốc cho khoa đích）
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        IList<SysDepartmentVEntity> GetTheLowerKsCodeList(string yfbmCode);

        /// <summary>
        /// Bộ phận nhà thuốc được ủy quyền thuốc
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string EmpowermentPharmacyDepartment(EmpowermentPharmacyDepartmentRequestDto req);

        /// <summary>
        /// Bộ phận nhà thuốc được ủy quyền thuốc vàEmpowermentPharmacyDepartmentKhác nhau，Phương thức này sẽ hủy trước ủy quyền tất cả nhà thuốc của thuốc này，trong việc cấp lại quyền
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string EmpowermentPharmacyDepartmentAndRemoveOld(EmpowermentPharmacyDepartmentAndRemoveOldRequestDto req);
    }
}
