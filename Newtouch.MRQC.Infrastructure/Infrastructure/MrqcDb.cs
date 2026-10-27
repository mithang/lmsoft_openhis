using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;

namespace Newtouch.MRQC.Infrastructure
{
    /// <summary>
    /// MRQC database access. Uses the vendor net6.0 SqlSugar multi-DB context
    /// (same one the running BaseAPI uses); MrQcDb maps to Newtouch_MRQC.
    /// Replaces the old EF6 DefaultDatabaseFactory + FrameworkBase repository base.
    /// </summary>
    public static class MrqcDb
    {
        public static SqlSugarProvider GetConnection()
        {
            return SqlSugarDbContext.Db.GetConnection("MrQcDb");
        }
    }
}
