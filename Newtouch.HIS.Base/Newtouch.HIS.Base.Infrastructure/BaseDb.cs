using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;

namespace Newtouch.HIS.Base.Infrastructure
{
    /// <summary>
    /// Base database access. Uses the vendor net6.0 SqlSugar multi-DB context
    /// (same one the running BaseAPI uses); BaseDb maps to NewtouchHIS_Base.
    /// Replaces the old EF6 BaseDatabaseFactory + FrameworkBase repository base.
    /// </summary>
    public static class BaseDb
    {
        public static SqlSugarProvider GetConnection()
        {
            return SqlSugarDbContext.Db.GetConnection("BaseDb");
        }
    }
}
