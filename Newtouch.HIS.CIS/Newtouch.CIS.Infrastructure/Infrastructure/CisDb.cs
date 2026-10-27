using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;

namespace Newtouch.CIS.Infrastructure
{
    /// <summary>
    /// CIS database access. Uses the vendor net6.0 SqlSugar multi-DB context
    /// (same one the running BaseAPI uses); CisDb maps to Newtouch_CIS.
    /// Replaces the old EF6 DefaultDatabaseFactory + FrameworkBase repository base.
    /// </summary>
    public static class CisDb
    {
        public static SqlSugarProvider GetConnection()
        {
            return SqlSugarDbContext.Db.GetConnection("CisDb");
        }
    }
}
