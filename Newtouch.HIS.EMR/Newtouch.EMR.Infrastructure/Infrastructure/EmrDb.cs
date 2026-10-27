using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;

namespace Newtouch.EMR.Infrastructure
{
    /// <summary>
    /// EMR database access. Uses the vendor net6.0 SqlSugar multi-DB context
    /// (same one the running BaseAPI uses); EmrDb maps to Newtouch_EMR.
    /// The web app's appsettings maps the "EmrDb" connection name; this
    /// classlib intentionally ships no appsettings of its own.
    /// Replaces the old EF6 DefaultDatabaseFactory + FrameworkBase repository base.
    /// </summary>
    public static class EmrDb
    {
        public static SqlSugarProvider GetConnection()
        {
            return SqlSugarDbContext.Db.GetConnection("EmrDb");
        }
    }
}
