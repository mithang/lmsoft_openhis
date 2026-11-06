using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using SqlSugar;
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

        // NX6-002: port that — goc Base SysOrganizeRepo.GetValidListByParentOrg
        // (CTE de quy tren Sys_Organize, BaseDb). Truoc day stub tra [] nen
        // Organize/GetChildTreeSelectJson + moi cay co so (SysChargeItem...) deu
        // rong live; nay tra cay co so that.
        public List<SysOrganizeVEntity> GetValidListByParentOrg(string parentOrgId)
        {
            if (string.IsNullOrWhiteSpace(parentOrgId))
            {
                return new List<SysOrganizeVEntity>();
            }
            var sql = @"WITH cteTree AS (SELECT * FROM Sys_Organize WHERE Id = @parentOrgId and zt = '1'
            UNION ALL
            SELECT Sys_Organize.* FROM cteTree INNER JOIN Sys_Organize ON cteTree.Id = Sys_Organize.ParentId and Sys_Organize.zt = '1')
        SELECT * FROM cteTree";
            return SettDb.GetBaseConnection().SqlQueryable<SysOrganizeVEntity>(sql)
                .AddParameters(new SugarParameter("@parentOrgId", parentOrgId)).ToList();
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
