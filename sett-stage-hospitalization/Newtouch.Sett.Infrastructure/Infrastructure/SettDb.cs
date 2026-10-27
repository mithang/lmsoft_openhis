using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;

namespace Newtouch.Sett.Infrastructure
{
    /// <summary>
    /// Sett database access via the vendor net6.0 SqlSugar multi-DB context
    /// (same one the running BaseAPI uses). Default = NewtouchHIS_Sett.
    /// Replaces EF6 DefaultDatabaseFactory + FrameworkBase repository base.
    /// </summary>
    public static class SettDb
    {
        public static SqlSugarProvider GetConnection()
        {
            return SqlSugarDbContext.Db.GetConnection("SettDb");
        }

        public static SqlSugarProvider GetBaseConnection()
        {
            return SqlSugarDbContext.Db.GetConnection("BaseDb");
        }

        public static SqlSugarProvider GetUnionConnection()
        {
            return SqlSugarDbContext.Db.GetConnection("UnionDb");
        }
    }
}
