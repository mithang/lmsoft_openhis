using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    // Source-true subset: Newtouch.Domain/IDomainServices/SystemManage/
    // IGRSCostEarningDmnService.cs (UploadFile: IFormFile replaced by fileName —
    // merged Infra has no ASP.NET types; stub throws, so the file is never read)
    public interface IGRSCostEarningDmnService
    {
        object GetCosttable(object p0, object p1, object p2);
        SiteCostEarningVO GetEarningInfo(string srxxId, string orgId);
        object GetMoneyDetailList(object p0, object p1, object p2, object p3, object p4);
        object GetMoneyDetailListV2(object p0, object p1, object p2, object p3, object p4, object p5);
        object GetjgszInfoList(object p0, object p1, object p2, object p3, object p4, object p5);
        void UploadFile(string fileName, out string path);
        object deletess(object p0);
        object getLastcbxx(object p0, object p1, object p2);
        void submitEarningInfo(SiteCostEarningVO vo, string keyvalue, string orgId);
    }

    // Source-true: Newtouch.Domain/IDomainServices/SystemManage/
    // ISysOrganizeDmnService.cs
    public interface ISysOrganizeDmnService
    {
        IList<SysOrganizeVEntity> GetMedicalOrganizeListByUserId(string userId);
        string GetNameByOrgId(string orgId);
        List<SysOrganizeVEntity> GetValidListByParentOrg(string parentOrgId);
            IList<SysOrganizeVEntity> GetValidTopOrgList();
}

    public interface Ijgss_AttachmentInfoRepo
    {
        jgss_AttachmentInfoEntity FindEntity(Func<jgss_AttachmentInfoEntity, bool> predicate);
    }

    public interface Ijgss_EarningInfoRepo
    {
        jgss_EarningInfoEntity FindEntity(Func<jgss_EarningInfoEntity, bool> predicate);
        object updateshzt(object p0, object p1, object p2);
    }
}
