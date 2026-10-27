using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;

namespace Newtouch.MR.ManageSystem.Infrastructure
{
    /// <summary>
    /// MRMS database access. Uses the vendor net6.0 SqlSugar multi-DB context
    /// (same one the running BaseAPI uses). Replaces EF6 DefaultDatabaseFactory
    /// + FrameworkBase repository base.
    /// </summary>
    public static class MrmsDb
    {
        /// <summary>MRMS module database (Newtouch_MRMS).</summary>
        public static SqlSugarProvider GetConnection()
        {
            return SqlSugarDbContext.Db.GetConnection("MrmsDb");
        }

        /// <summary>Base database (NewtouchHIS_Base) for shared dictionaries.</summary>
        public static SqlSugarProvider GetBaseConnection()
        {
            return SqlSugarDbContext.Db.GetConnection("BaseDb");
        }
    }
}
