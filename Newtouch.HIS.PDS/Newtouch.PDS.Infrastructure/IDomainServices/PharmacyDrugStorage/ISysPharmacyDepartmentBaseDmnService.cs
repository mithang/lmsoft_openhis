using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Model;
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
        /// Lấy nhà thuốc và kho thuốc hiệu lực
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yjbmjb"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVO> GetEffectiveList(string orgId, byte? yjbmjb);

        /// <summary>
        /// Lấy thông tin bộ phận nhà thuốc
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVO> GetPagintionList(Pagination pagination, string organizeId, string keyword = null);

        /// <summary>
        /// Sửa và thêm bộ phận nhà thuốc
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void submitForm(SysPharmacyDepartmentVO entity, int? keyValue);

        /// <summary>
        /// LấyUserIdđã liên kết hiện tạiyfbmCode (Không gồm tổ chức con)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<string> GetCurPharmacyList(string userId, string orgId);

        /// <summary>
        /// Lấy nhà thuốc thuộc tổ chức/Kho thuốc (Không gồm tổ chức con)
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVO> GetPharmacyListByOrg(string OrganizeId);
    }
}
