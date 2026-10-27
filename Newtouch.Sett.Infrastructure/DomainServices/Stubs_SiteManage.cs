using Newtouch.Sett.Domain;
using System;
using System.Collections.Generic;

namespace Newtouch.Sett.DomainServices
{
    public class GRSCostEarningDmnService : IGRSCostEarningDmnService
    {
        public object GetCosttable(object p0, object p1, object p2)
        {
            return null;
        }

        // Empty (not null): the controller reads data.srxx.siteId; an empty
        // VO renders the blank form instead of NRE-ing. No fake data.
        public SiteCostEarningVO GetEarningInfo(string srxxId, string orgId)
        {
            return new SiteCostEarningVO
            {
                srxx = new jgssEarningVO(),
                srxxList = new List<jgssEarningGridVO>(),
                cbxxList = new List<jgssCostVO>(),
                fjxx = new List<jgssAttachmentVO>()
            };
        }

        public object GetMoneyDetailList(object p0, object p1, object p2, object p3, object p4)
        {
            return null;
        }

        public object GetMoneyDetailListV2(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            return null;
        }

        public object GetjgszInfoList(object p0, object p1, object p2, object p3, object p4, object p5)
        {
            return null;
        }

        public void UploadFile(string fileName, out string path)
        {
            path = "";
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public object deletess(object p0)
        {
            return null;
        }

        public object getLastcbxx(object p0, object p1, object p2)
        {
            return null;
        }

        public void submitEarningInfo(SiteCostEarningVO vo, string keyvalue, string orgId)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class SysOrganizeDmnService : ISysOrganizeDmnService
    {
        public IList<SysOrganizeVEntity> GetMedicalOrganizeListByUserId(string userId)
        {
            return new List<SysOrganizeVEntity>();
        }

        public string GetNameByOrgId(string orgId)
        {
            return null;
        }

        public List<SysOrganizeVEntity> GetValidListByParentOrg(string parentOrgId)
        {
            return new List<SysOrganizeVEntity>();
        }

        public IList<SysOrganizeVEntity> GetValidTopOrgList()
        {
            return new List<SysOrganizeVEntity>();
        }
}

    public class jgss_AttachmentInfoRepo : Ijgss_AttachmentInfoRepo
    {
        public jgss_AttachmentInfoEntity FindEntity(Func<jgss_AttachmentInfoEntity, bool> predicate)
        {
            return null;
        }

    }

    public class jgss_EarningInfoRepo : Ijgss_EarningInfoRepo
    {
        public jgss_EarningInfoEntity FindEntity(Func<jgss_EarningInfoEntity, bool> predicate)
        {
            return null;
        }

        public object updateshzt(object p0, object p1, object p2)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }
}
