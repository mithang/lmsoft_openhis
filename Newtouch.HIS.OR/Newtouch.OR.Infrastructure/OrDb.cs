using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;

namespace Newtouch.OR.ManageSystem.Infrastructure
{
    /// <summary>
    /// OR database access. Uses the vendor net6.0 SqlSugar multi-DB context
    /// (same one the running BaseAPI uses); OrDb maps to Newtouch_OR.
    /// Replaces the old EF6 DefaultDatabaseFactory + FrameworkBase repository base.
    /// Cross-database reads use fully-qualified [NewtouchHIS_Base].. names in SQL.
    /// </summary>
    public static class OrDb
    {
        public static SqlSugarProvider GetConnection()
        {
            return SqlSugarDbContext.Db.GetConnection("OrDb");
        }
    }
}
