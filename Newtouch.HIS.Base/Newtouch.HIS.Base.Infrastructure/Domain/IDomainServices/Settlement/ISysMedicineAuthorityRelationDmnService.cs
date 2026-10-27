using Newtouch.HIS.Domain.Entity.Settlement;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.Settlement
{
    public interface ISysMedicineAuthorityRelationDmnService
    {
        IList<SysMedicineAuthorityRelationVO> GetGridQx(Pagination pagination, string gh, string orgId, string keyword = null);

        IList<SysMedicineAuthorityRelationVO> GetListBygh(string gh, string orgId, string keyword = null);
        void UpdateAuthority(string gh, string organizeId, string keyword, string[] AuthorityList);
        IList<SysStaffVO> GetStaffPaginationList(Pagination pagination, string OrganizeId, string keyword = null);
    }
}
