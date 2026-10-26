using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface ISysPharmacyDepartmentBaseDmnService
    {
        SysPharmacyDepartmentVO GetFormJson(string orgId, int? yfbmId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yjbmjb"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVO> GetList(string orgId, byte? yjbmjb);

        /// <summary>
        /// lấy còn hiệu lực của nhà thuốc và kho thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yjbmjb"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVO> GetEffectiveList(string orgId, byte? yjbmjb);

        /// <summary>
        /// lấy bộ phận nhà thuốc thông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVO> GetPagintionList(Pagination pagination, string organizeId, string keyword = null);

        /// <summary>
        /// sửa và thêm bộ phận nhà thuốc
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void submitForm(SysPharmacyDepartmentVO entity, int? keyValue);

        /// <summary>
        /// lấy UserId hiện tại đã liên kết của cần yfbmCode  (không bao gồm tổ chức con)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<string> GetCurPharmacyList(string userId, string orgId);

        /// <summary>
        /// lấy tổ chức dưới của nhà thuốc/kho thuốc  (không bao gồm tổ chức con)
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVO> GetPharmacyListByOrg(string OrganizeId);
    }
}
