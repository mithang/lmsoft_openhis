using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;

namespace Newtouch.Herp.Infrastructure
{
    /// <summary>
    /// Herp database access via the vendor net6.0 SqlSugar multi-DB context
    /// (same one the running BaseAPI uses). Replaces EF6 DefaultDatabaseFactory
    /// + FrameworkBase repository base. Connection ids must exist in
    /// BusinessDB.DBConf in appsettings (HerpDb -&gt; NewtouchHIS_herp,
    /// BaseDb -&gt; NewtouchHIS_Base).
    /// </summary>
    public static class HerpDb
    {
        public static SqlSugarProvider GetConnection()
        {
            return SqlSugarDbContext.Db.GetConnection("HerpDb");
        }

        public static SqlSugarProvider GetBaseConnection()
        {
            return SqlSugarDbContext.Db.GetConnection("BaseDb");
        }
    }
}
