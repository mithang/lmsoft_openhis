using System;
using Newtouch.CIS.Compat;
using Newtouch.Domain.IRepository;
using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// System-config repository interface (originally FrameworkBase
    /// MultiOrg SystemManage, binary-only). Surface derived from CIS usage:
    /// only GetValueByCode / GetBoolValueByCode are called.
    /// </summary>
    public interface ISysConfigRepo
    {
        string GetValueByCode(string code, string organizeId);
        bool GetBoolValueByCode(string code, string organizeId, bool defaultValue = false);
        int GetIntValueByCode(string code, string organizeId, int defaultValue);
    }

    /// <summary>
    /// Staff repository interface (originally FrameworkBase MultiOrg
    /// SystemManage, binary-only). Surface derived from CIS usage: only
    /// GetValidStaffByGh is called (the Base-domain ISysStaffRepo with
    /// GetsatffListByOrg etc. is a different interface and is not used
    /// by CIS code).
    /// </summary>
    public interface ISysStaffRepo
    {
        Newtouch.Domain.Entity.SysStaffVEntity GetValidStaffByGh(string gh, string organizeId);
    }
}

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// Code-mapping ("TT") service interface (implementation lives outside
    /// CIS source). Only GetTTItem is called by CIS code.
    /// </summary>
    public interface ITTCataloguesComparisonDmnService
    {
        Newtouch.Domain.ValueObjects.FirstSecondThird GetTTItem(string orgId, string type, string code, string txtype);
    }
}

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// Three-part code-mapping result (originally FrameworkBase binary).
    /// Callers use First (code) and Second (name) as strings.
    /// </summary>
    public class FirstSecondThird
    {
        public string First { get; set; }
        public string Second { get; set; }
        public string Third { get; set; }
    }
}

namespace Newtouch.Repository
{
    /// <summary>
    /// Base-database access shared by the cross-module repositories below.
    /// Sys_Staff / Sys_GlobalConfig live in NewtouchHIS_Base, not in CIS.
    /// </summary>
    internal static class BaseDb
    {
        public static SqlSugarProvider GetConnection()
        {
            return SqlSugarDbContext.Db.GetConnection("BaseDb");
        }
    }

    /// <summary>
    /// ISysConfigRepo implementation against the org-scoped CIS Sys_Config
    /// table (CisDb = Newtouch_CIS), filtered by Code + OrganizeId.
    /// VERIFIED 2026-10-26 against live data: NewtouchHIS_Base.Sys_GlobalConfig
    /// has 0 rows in the demo DB, so the previous implementation could never
    /// return a value; the codes callers request (wordsNeedExecuteSwitch,
    /// bespeakTime, IsRehabAuthtoNurse, ...) live in CIS Sys_Config with
    /// per-org rows. The previous wrong-table read caused NREs in
    /// OrderExecutionController..ctor (wnesV.ToLower() on null) and anywhere
    /// else a config value is required. SysGlobalConfigEntity is reused as
    /// the DTO (only its Value column is read).
    /// </summary>
    public class SysConfigRepo : ISysConfigRepo
    {
        public string GetValueByCode(string code, string organizeId)
        {
            var row = Newtouch.CIS.Infrastructure.CisDb.GetConnection().Ado.SqlQuerySingle<Newtouch.HIS.Domain.Entity.SysGlobalConfigEntity>(
                "SELECT TOP 1 Value FROM Sys_Config WHERE Code = @code AND OrganizeId = @org AND zt = '1' ORDER BY px",
                new SugarParameter("@code", code),
                new SugarParameter("@org", organizeId));
            return row?.Value;
        }

        public int GetIntValueByCode(string code, string organizeId, int defaultValue)
        {
            var v = GetValueByCode(code, organizeId);
            return int.TryParse(v, out int n) ? n : defaultValue;
        }
        public bool GetBoolValueByCode(string code, string organizeId, bool defaultValue = false)
        {
            var value = GetValueByCode(code, organizeId);
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }
            if (value == "1" || value.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (value == "0" || value.Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            return defaultValue;
        }
    }

    /// <summary>
    /// ISysStaffRepo implementation against NewtouchHIS_Base.Sys_Staff.
    /// </summary>
    public class SysStaffRepo : ISysStaffRepo
    {
        public Newtouch.Domain.Entity.SysStaffVEntity GetValidStaffByGh(string gh, string organizeId)
        {
            return BaseDb.GetConnection().Ado.SqlQuerySingle<Newtouch.Domain.Entity.SysStaffVEntity>(
                "SELECT * FROM V_S_Sys_Staff WHERE gh = @gh AND OrganizeId = @orgId AND zt = '1'",
                new SugarParameter("@gh", gh),
                new SugarParameter("@orgId", organizeId));
        }
    }
}

namespace Newtouch.DomainServices
{
    /// <summary>
    /// STUB implementation of the TT code-mapping service. The real
    /// implementation (and its backing table) is not in CIS/Base source.
    /// Returns an empty mapping so callers that only need the object shape
    /// keep working; the yibao-review paths that consume these mappings
    /// cannot submit without the vendor mapping data + yibao service.
    /// </summary>
    public class TTCataloguesComparisonDmnService : DmnServiceBase, Newtouch.Domain.IDomainServices.ITTCataloguesComparisonDmnService
    {
        public Newtouch.Domain.ValueObjects.FirstSecondThird GetTTItem(string orgId, string type, string code, string txtype)
        {
            return new Newtouch.Domain.ValueObjects.FirstSecondThird();
        }
    }
}
